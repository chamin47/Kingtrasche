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

    private Vector2 inputVector;
    private Vector3 moveVector;

    private bool isRightDirection; // �ٶ󺸴� ���� �⺻ ������
    public Vector2 returnDirection => isRightDirection ? Vector2.left : Vector2.right; //���� ��ȯ ������Ƽ

    public Transform groundCheck; //�÷��̾� ����ġ üũ
    public LayerMask groundLayer;
    private bool isGrounded;

    private bool isJumpig = false;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        playerShooting = GetComponent<PlayerShooting>();
        animation = GetComponent<PlayerAnimationController>();

        var playerActionMap = inputActionAsset.FindActionMap("PlayerActions");
        moveAction = playerActionMap.FindAction("Move");
        jumpAction = playerActionMap.FindAction("Jump");
        skillAction = playerActionMap.FindAction("Skill");

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

        transform.Translate(moveVector.normalized * Time.deltaTime * moveSpeed); //Test

        FlipPlayerDirection();
    }

    public void OnMove(InputAction.CallbackContext value)
    {
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
        }
    }

    public void OnSkill(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            Debug.Log("��ų");
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

}
