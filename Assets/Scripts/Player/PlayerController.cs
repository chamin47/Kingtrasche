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

    private bool isRightDirection; // 바라보는 방향 기본 오른쪽
    public Vector2 returnDirection => isRightDirection ? Vector2.left : Vector2.right; //방향 반환 프로퍼티

    public Transform groundCheck; //플레이어 발위치 체크
    public LayerMask groundLayer;
    private bool isGrounded;

    private bool isJumpig = false;
    private bool isStunned = false; // 스턴 상태 여부
    private float stunDuration = 0f; // 스턴 지속 시간

    private int stunTouchCount = 0; // 스턴 해제 터치 횟수
    private float stunTouchResetTime = 1.0f; // 입력 초기화 시간
    private float stunTouchTimer = 0f; // 입력 초기화 타이머

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

        // 씬 확정될때 활성화
        //ExceptKey();

        //if (SceneManager.GetActiveScene().name == "HAY Scene")
        //{
        //    moveAction.Disable();
        //    skillAction.Disable();
        //    playerShooting.isFiring = false;
        //} //추후 삭제
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

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer); // 발이 땅에 닿아있는지 체크
        if (isGrounded)
        {
            jumpCount = 0; // 바닥에 닿으면 jumpCount 리셋
        }
    }

    void Update()
    {
        // 씬 확정될때 활성화
        //if (SceneManager.GetActiveScene().name == "HAY Scene") //러닝씬
        //{
        //    transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        //}
        //else if (SceneManager.GetActiveScene().name == "CDM Scene") //슈팅씬
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

            // 스턴 해제를 위해 터치 입력을 처리합니다.
            HandleTouchInput();

            // 스턴이 터치로 해제되지 않으면 리턴
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
            if (isGrounded) //바닥이거나
            {
                rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
                jumpCount = 1;
            }
            else if (jumpCount < 2) // jumpCount가 2회미만일때만 점프 가능
            {
                rigid.velocity = new Vector2(rigid.velocity.x, 0f); // y 축 속도 초기화 -> 일정한 높이의 점프
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
            Debug.Log("스킬");
        }
    }

    private void HandleTouchInput()
    {
        // 터치 입력 처리
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

        // 마우스 클릭 입력 처리 (개발 중 테스트용)
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
            return;
        }
        else // 그 외 씬이면
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
