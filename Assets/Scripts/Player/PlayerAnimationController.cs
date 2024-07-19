using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public Animator animator;

    private static readonly int _Idle = Animator.StringToHash("Idle");
    private static readonly int _Running = Animator.StringToHash("Running");
    private static readonly int _Jump = Animator.StringToHash("Jump");
    private static readonly int _Dying = Animator.StringToHash("Dying");
    private static readonly int _Bite = Animator.StringToHash("Bite");

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void StartIdleAnim()
    {
        animator.SetBool(_Idle, true);
    }

    public void StopIdleAnim()
    {
        animator.SetBool(_Idle, false);
    }

    public void StartRunningAnim()
    {
        animator.SetBool(_Running, true);
    }

    public void StopRunningAnim()
    {
        animator.SetBool(_Running, false);
    }

    public void JumpAnim()
    {
        animator.SetTrigger(_Jump);
    }

    public void DyingAnim()
    {
        animator.SetTrigger(_Dying);
    }

    public void BiteAnim()
    {
        animator.SetTrigger(_Bite);
    }
}
