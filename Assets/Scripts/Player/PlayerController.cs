using GameBalance;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public InputActionAsset inputActionAsset;
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction skillAction;

    private Rigidbody2D rigid;
    private PlayerShooting playerShooting;
    private PlayerAnimationController animation;

    public float moveSpeed = 3f;
    public float jumpForce = 13f;
    private int jumpCount = 0;
    public int life = 3;

    private Vector2 inputVector;
    private Vector3 moveVector;

    private bool isRightDirection; // �ٶ󺸴� ���� �⺻ ������
    public Vector2 returnDirection => isRightDirection ? Vector2.left : Vector2.right; //���� ��ȯ ������Ƽ

    public Transform groundCheck; //�÷��̾� ����ġ üũ
    public LayerMask groundLayer;
    private bool isGrounded;

    private bool isJumpig = false;
    private bool isStunned = false; // ���� ���� ����
    private float stunDuration = 0f; // ���� ���� �ð�

    private int stunTouchCount = 0; // ���� ���� ��ġ Ƚ��
    private float stunTouchResetTime = 1.0f; // �Է� �ʱ�ȭ �ð�
    private float stunTouchTimer = 0f; // �Է� �ʱ�ȭ Ÿ�̸�

    private Color originalColor;
    private SpriteRenderer spriteRenderer;

    public delegate void Healthchanged();
    public event Healthchanged OnHealthChanged;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        playerShooting = GetComponent<PlayerShooting>();
        animation = GetComponent<PlayerAnimationController>();

        var playerActionMap = inputActionAsset.FindActionMap("PlayerActions");
        moveAction = playerActionMap.FindAction("Move");
        jumpAction = playerActionMap.FindAction("Jump");
        skillAction = playerActionMap.FindAction("Skill");

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        originalColor = spriteRenderer.color;

        InitPlayerData();
    }

    private void OnEnable()
    {
        moveAction.Enable();
        jumpAction.Enable();
        skillAction.Enable();

        moveAction.performed += OnMove;
        moveAction.canceled += OnMove;
        jumpAction.performed += OnJump;
        skillAction.performed += OnSkill;

        // �� Ȯ���ɶ� Ȱ��ȭ
        //ExceptKey();

        //if (SceneManager.GetActiveScene().name == "HAY Scene")
        //{
        //    moveAction.Disable();
        //    skillAction.Disable();
        //    playerShooting.isFiring = false;
        //} //���� ����
    }

    private void OnDisable()
    {
        moveAction.Disable();
        jumpAction.Disable();
        skillAction.Disable();

        moveAction.performed -= OnMove;
        moveAction.canceled -= OnMove;
        jumpAction.performed -= OnJump;
        skillAction.performed -= OnSkill;
    }

    void Start()
    {
    }
    public void InitPlayerData()
    {
        int playerid = 0;
        PlayerData data = PlayerData.PlayerDataMap[playerid];
        this.moveSpeed = data.moveSpeed;
        this.jumpForce = data.jumpForce;
    }

    private void FixedUpdate()
    {
        if (isStunned)
            return;

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer); // ���� ���� ����ִ��� üũ
        if (isGrounded)
        {
            jumpCount = 0; // �ٴڿ� ������ jumpCount ����
        }
    }

    void Update()
    {
        // �� Ȯ���ɶ� Ȱ��ȭ
        //if (SceneManager.GetActiveScene().name == "HAY Scene") //���׾�
        //{
        //    transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        //}
        //else if (SceneManager.GetActiveScene().name == "CDM Scene") //���þ�
        //{
        //    transform.Translate(moveVector.normalized * Time.deltaTime * moveSpeed);
        //}

        if (isStunned)
        {
            stunTouchTimer += Time.deltaTime;
            if (stunTouchTimer >= stunTouchResetTime)
            {
                stunTouchCount = 0;
                stunTouchTimer = 0;
            }

            // ���� ������ ���� ��ġ �Է��� ó���մϴ�.
            HandleTouchInput();

            // ������ ��ġ�� �������� ������ ����
            return;
        }

        transform.Translate(moveVector.normalized * Time.deltaTime * moveSpeed); //Test

        FlipPlayerDirection();
    }

    public void OnMove(InputAction.CallbackContext value)
    {
        if (isStunned)
            return;

        inputVector = value.ReadValue<Vector2>();
        moveVector = new Vector3(inputVector.x, 0f, 0f);

        if (moveVector.x != 0)
        {
            animation.StartRunningAnim();
            animation.StopIdleAnim();
        }
        else
        {
            animation.StartIdleAnim();
            animation.StopRunningAnim();
        }
    }

    public void OnJump(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            if (isGrounded) //�ٴ��̰ų�
            {
                rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
                jumpCount = 1;
            }
            else if (jumpCount < 2) // jumpCount�� 2ȸ�̸��϶��� ���� ����
            {
                rigid.velocity = new Vector2(rigid.velocity.x, 0f); // y �� �ӵ� �ʱ�ȭ -> ������ ������ ����
                rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
                jumpCount = 2;
            }
            animation.JumpAnim();

            RunningTutorialManager.Instance.IncreaseJumpCount();
        }
    }

    public void TakeDamage(int Damage)
    {
        life -= Damage;

        OnHealthChanged?.Invoke();
        if (life <= 0)
        {
            life = 0;
            DirectDying();
        }
    }

    public void DirectDying()
    {
        animation.DyingAnim();
    }

    public void OnSkill(InputAction.CallbackContext value)
    {
        if (isStunned)
            return;

        if (value.performed)
        {
            Debug.Log("��ų");
        }
    }

    private void HandleTouchInput()
    {
        // ��ġ �Է� ó��
        if (Input.touchCount > 0)
        {
            foreach (UnityEngine.Touch touch in Input.touches)
            {
                if (touch.phase == UnityEngine.TouchPhase.Began)
                {
                    OnTouch();
                }
            }
        }

        // ���콺 Ŭ�� �Է� ó�� (���� �� �׽�Ʈ��)
        if (Input.GetMouseButtonDown(0))
        {
            OnTouch();
        }
    }

    private void OnTouch()
    {
        if (isStunned)
        {
            stunTouchCount++;
            stunTouchTimer = 0;
            if (stunTouchCount >= 6)
            {
                EndStun();
            }
        }
    }

    private void FlipPlayerDirection()
    {
        if (moveVector.x < 0 && !isRightDirection)
        {
            Flip();
        }
        else if (moveVector.x > 0 && isRightDirection)
        {
            Flip();
        }
    }

    private void Flip()
    {
        Vector3 scale = transform.localScale;

        isRightDirection = !isRightDirection;
        scale.x *= -1; // x�� ����
        transform.localScale = scale;
    }

    private void ExceptKey()
    {
        if (SceneManager.GetActiveScene().name == "HAY Scene") // ���׾�
        {
            moveAction.Disable();
            skillAction.Disable();
            playerShooting.isFiring = false;
        }
        else if (SceneManager.GetActiveScene().name == "CDM Scene") // ���þ�
        {
            return;
        }
        else // �� �� ���̸�
        {
            //moveAction.Disable();
            //skillAction.Disable();
            //jumpAction.Disable();
            return;
        }
    }

    public void ApplyStun(float duration)
    {
        isStunned = true;
        stunDuration = duration;
        stunTouchCount = 0;
        stunTouchTimer = 0;
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.5f);
    }

    private void EndStun()
    {
        isStunned = false;
        stunDuration = 0;
        stunTouchCount = 0;
        spriteRenderer.color = originalColor;
    }
}
