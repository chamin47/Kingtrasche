using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //public InputActionAsset inputActionAsset;

    public float moveSpeed = 7f;
    public float jumpForce = 7f;
    private Vector2 inputVector;
    private Vector3 moveVector;

    public Transform groundCheck; //플레이어 발위치 체크
    public LayerMask groundLayer;

    private Rigidbody2D rigid;
    private bool isGrounded;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Start()
    {


    }


    void Update()
    {
        transform.Translate(moveVector.normalized * Time.deltaTime * moveSpeed);
    }

    public void OnMove(InputAction.CallbackContext value)
    {
        // velocity->1초당 움직이는 거리
        //rigid.velocity = new Vector2(moveInput.x * moveSpeed, rigid.velocity.y);
        inputVector = value.ReadValue<Vector2>();
        moveVector = new Vector3(inputVector.x, 0f, inputVector.y);

    }

    public void OnJump(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            rigid.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);

        }
    }

    public void OnSkill(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            Debug.Log("스킬");
        }
    }
}
