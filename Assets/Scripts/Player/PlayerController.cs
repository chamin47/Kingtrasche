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
    private InputAction touchJumpAction;

    public Joystick joystick;
    public GameObject JoystickUI;

    private Rigidbody2D rigid;
    private PlayerShooting playerShooting;
    private PlayerAnimationController animController;

    public float moveSpeed = 0;
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
    public bool isStunned = false; // ���� ���� ����
    public bool isPuzzlOn = false;
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
        animController = GetComponent<PlayerAnimationController>();

        var playerActionMap = inputActionAsset.FindActionMap("PlayerActions");
        moveAction = playerActionMap.FindAction("Move");
        jumpAction = playerActionMap.FindAction("Jump");
        skillAction = playerActionMap.FindAction("Skill");
        touchJumpAction = playerActionMap.FindAction("TouchJump");

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        originalColor = spriteRenderer.color;

        InitPlayerData();
    }

    private void OnEnable()
    {
        moveAction.Enable();
        jumpAction.Enable();
        skillAction.Enable();
        touchJumpAction.Enable();

        moveAction.performed += OnMove;
        moveAction.canceled += OnMove;
        jumpAction.performed += OnJump;
        skillAction.performed += OnSkill;
        touchJumpAction.performed += OnJump;

        // �� Ȯ���ɶ� Ȱ��ȭ
        ExceptKey();
    }

    private void OnDisable()
    {
        moveAction.Disable();
        jumpAction.Disable();
        skillAction.Disable();
        touchJumpAction.Disable();

        moveAction.performed -= OnMove;
        moveAction.canceled -= OnMove;
        jumpAction.performed -= OnJump;
        skillAction.performed -= OnSkill;
        touchJumpAction.performed -= OnJump;
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
        if (SceneManager.GetActiveScene().name == "RunningScene" ||
            SceneManager.GetActiveScene().name == "RunningTutorialScene" ||
            SceneManager.GetActiveScene().name == "InfinityRunningScene") //���׾�
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            animController.StartRunningAnim();
        }
        else if (SceneManager.GetActiveScene().name == "BossScene1" ||
            SceneManager.GetActiveScene().name == "BossScene2" ||
            SceneManager.GetActiveScene().name == "BossScene3") //���þ�
        {
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

            inputVector = new Vector2(joystick.Horizontal, joystick.Vertical);
            moveVector = new Vector3(inputVector.x, 0, 0);
            transform.Translate(moveVector.normalized * Time.deltaTime * moveSpeed);

            if (moveVector.x != 0)
            {
                animController.StartRunningAnim();
                animController.StopIdleAnim();
            }
            else
            {
                animController.StartIdleAnim();
                animController.StopRunningAnim();
            }

        }
        else
        {
            //Test
            inputVector = new Vector2(joystick.Horizontal, joystick.Vertical);
            moveVector = new Vector3(inputVector.x, 0, 0);
            transform.Translate(moveVector.normalized * Time.deltaTime * moveSpeed);
            if (moveVector.x != 0)
            {
                animController.StartRunningAnim();
                animController.StopIdleAnim();
            }
            else
            {
                animController.StartIdleAnim();
                animController.StopRunningAnim();
            }
            //Test
        }

        FlipPlayerDirection();
        HandleJumpInput();
    }

    public void OnMove(InputAction.CallbackContext value)
    {
        if (isStunned)
            return;

        inputVector = value.ReadValue<Vector2>();
        moveVector = new Vector3(inputVector.x, 0f, 0f);

        if (moveVector.x != 0)
        {
            animController.StartRunningAnim();
            animController.StopIdleAnim();
        }
        else
        {
            animController.StartIdleAnim();
            animController.StopRunningAnim();
        }
    }

    public void OnJump(InputAction.CallbackContext value)
    {
        if (isStunned)
            return;

        if (joystick.isPointDown)
        {
            joystick.isPointDown = false;
            return;
        }
        if (value.started)
        {
            Jump();
        }
    }

    private void HandleJumpInput()
    {
        if (joystick.isPointDown) // ���̽�ƽ�� ó�� ��ġ�� ������ ���� X
        {
            joystick.isPointDown = false;
            return;
        }

        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == UnityEngine.TouchPhase.Began)
                {
                    Jump();
                }
            }
        }

        //if (Input.GetMouseButtonDown(0))
        //{
        //    Jump();
        //}
    }

    private void Jump()
    {
        if (isStunned || isPuzzlOn)
            return;

        if (isGrounded) //�ٴ��̰ų�
        {
            rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
            jumpCount = 1;

            Managers.Sound.Play("SFX_Jump_42", Sound.Effect);
        }
        else if (jumpCount < 2) // jumpCount�� 2ȸ�̸��϶��� ���� ����
        {
            rigid.velocity = new Vector2(rigid.velocity.x, 0f); // y �� �ӵ� �ʱ�ȭ -> ������ ������ ����
            rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
            jumpCount = 2;

            Managers.Sound.Play("SFX_Jump_35", Sound.Effect);
        }
        animController.JumpAnim();

        if (SceneManager.GetActiveScene().name == "RunningTutorialScene")
        {
            RunningTutorialManager.Instance.IncreaseJumpCount();
        }
    }

    public void TakeDamage(int Damage)
    {
        Managers.Sound.Play("8", Sound.Effect);
        life -= Damage;

        OnHealthChanged?.Invoke();
        if (life < 2)
        {
            Managers.Sound.Play("heartbeat_fast_0", Sound.Bgm);
        }
        if (life <= 0)
        {
            life = 0;
            Managers.Sound.Clear();
            DirectDying();
        }
    }

    public void DirectDying()
    {
        animController.DyingAnim();
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
        if ((SceneManager.GetActiveScene().name == "RunningScene" ||
            SceneManager.GetActiveScene().name == "RunningTutorialScene" ||
            SceneManager.GetActiveScene().name == "InfinityRunningScene")) // ���׾�
        {
            moveAction.Disable();
            skillAction.Disable();
            playerShooting.isFiring = false;
            JoystickUI.SetActive(false);
        }
        else if (SceneManager.GetActiveScene().name == "BossScene1" ||
            SceneManager.GetActiveScene().name == "BossScene2" ||
            SceneManager.GetActiveScene().name == "BossScene3") // ���þ�
        {
            playerShooting.isFiring = true;
            return;
        }
        else if (SceneManager.GetActiveScene().name == "LobbyScene")
        {
            OnDisable();
            playerShooting.isFiring = false;
            JoystickUI.SetActive(false);
        }
        else // �� �� ���̸�
        {
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
        animController.StartSleepingAnim();
    }

    private void EndStun()
    {
        isStunned = false;
        stunDuration = 0;
        stunTouchCount = 0;
        spriteRenderer.color = originalColor;
        animController.StopSleepingAnim();
    }
}
