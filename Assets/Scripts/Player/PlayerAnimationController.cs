using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAnimationController : MonoBehaviour
{
    public Animator animator;

    private static readonly int _Idle = Animator.StringToHash("Idle");
    private static readonly int _Running = Animator.StringToHash("Running");
    private static readonly int _Jump = Animator.StringToHash("Jump");
    private static readonly int _Dying = Animator.StringToHash("Dying");
    private static readonly int _Bite = Animator.StringToHash("Bite");
    private static readonly int _Sleeping = Animator.StringToHash("Sleeping");

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
        //StartCoroutine("StepSound");
    }

    public void StopRunningAnim()
    {
        animator.SetBool(_Running, false);
        //StopCoroutine("StepSound");
    }

    public void JumpAnim()
    {
        animator.SetTrigger(_Jump);
    }

    public void DyingAnim()
    {
        animator.SetTrigger(_Dying);
    }

    public void AfterAnimationNExtAction()
    {
        gameObject.SetActive(false);
        if (SceneManager.GetActiveScene().name == "InfinityRunningScene")
        {
            Managers.Game.InfinityGameOver();
        }
        else
        {
            Managers.Game.GameOver();
        }
    }

    public void BiteAnim()
    {
        animator.SetTrigger(_Bite);
    }

    public void SleepingAnim()
    {
        animator.SetTrigger(_Sleeping);
    }

    IEnumerator StepSound()
    {
        while (true)
        {
            Managers.Sound.Play("sfx_step_grass_l", Sound.Effect);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
