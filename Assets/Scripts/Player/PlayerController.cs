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

    private bool isRightDirection; // 바라보는 방향 기본 오른쪽
    public Vector2 returnDirection => isRightDirection ? Vector2.left : Vector2.right; //방향 반환 프로퍼티

    public Transform groundCheck; //플레이어 발위치 체크
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
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer); // 발이 땅에 닿아있는지 체크
        if (isGrounded)
        {
            jumpCount = 0; // 바닥에 닿으면 jumpCount 리셋
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
            if (isGrounded) //바닥이거나
            {
                //rigid.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
                jumpCount = 1;
            }
            else if (jumpCount < 2) // jumpCount가 2회미만일때만 점프 가능
            {
                rigid.velocity = new Vector2(rigid.velocity.x, 0f); // y 축 속도 초기화 -> 일정한 높이의 점프
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
            Debug.Log("스킬");
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
        scale.x *= -1; // x축 반전
        transform.localScale = scale;
    }

    private void ExceptKey()
    {
        if (SceneManager.GetActiveScene().name == "HAY Scene") // 러닝씬
        {
            moveAction.Disable();
            skillAction.Disable();
            playerShooting.isFiring = false;
        }
        else if (SceneManager.GetActiveScene().name == "CDM Scene") // 슈팅씬
        {
            playerShooting.isFiring = true;
            return;
        }
        else // 그 외 씬이면
        {
            moveAction.Disable();
            skillAction.Disable();
            jumpAction.Disable();
        }
    }

}
