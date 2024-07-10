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

    public float moveSpeed = 7f;
    public float jumpForce = 13f;
    private int jumpCount = 0;

    private Vector2 inputVector;
    private Vector3 moveVector;

    private bool isRightDirection; // �ٶ󺸴� ���� �⺻ ������
    public Vector2 returnDirection => isRightDirection ? Vector2.left : Vector2.right; //���� ��ȯ ������Ƽ

    public Transform groundCheck; //�÷��̾� ����ġ üũ
    public LayerMask groundLayer;
    private bool isGrounded;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        playerShooting = GetComponent<PlayerShooting>();

        var playerActionMap = inputActionAsset.FindActionMap("PlayerActions");
        moveAction = playerActionMap.FindAction("Move");
        jumpAction = playerActionMap.FindAction("Jump");
        skillAction = playerActionMap.FindAction("Skill");
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
        ExceptKey();
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
        transform.Translate(moveVector.normalized * Time.deltaTime * moveSpeed);

        FlipPlayerDirection();
    }

    public void OnMove(InputAction.CallbackContext value)
    {
        //rigid.velocity = new Vector2(moveInput.x * moveSpeed, rigid.velocity.y);
        inputVector = value.ReadValue<Vector2>();
        moveVector = new Vector3(inputVector.x, 0f, 0f);
    }

    public void OnJump(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            if (isGrounded) //�ٴ��̰ų�
            {
                //rigid.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
                jumpCount = 1;
            }
            else if (jumpCount < 2) // jumpCount�� 2ȸ�̸��϶��� ���� ����
            {
                rigid.velocity = new Vector2(rigid.velocity.x, 0f); // y �� �ӵ� �ʱ�ȭ -> ������ ������ ����
                //rigid.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
                jumpCount = 2;
            }
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
            playerShooting.isFiring = true;
            return;
        }
        else // �� �� ���̸�
        {
            moveAction.Disable();
            skillAction.Disable();
            jumpAction.Disable();
        }
    }

}
