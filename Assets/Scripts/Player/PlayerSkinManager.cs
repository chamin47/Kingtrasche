using UnityEngine;

public class PlayerSkinManager : MonoBehaviour
{
    public Renderer playerRenderer;
    public Animator animator;
    public string baseSkin = "";

    public string skinName = "AfghanHoundBlack";

    private void Start()
    {
        ApplySkin(skinName);
    }

    public void ApplySkin(string skinName)
    {
        ChangeRenderer(skinName);
        ChangeAnimations(skinName);
    }

    private void ChangeRenderer(string skinName)
    {
        Texture skinTexture = Resources.Load<Texture>($"DogSkinAndAnimationClip/{skinName}/Standing1");
        playerRenderer.material.mainTexture = skinTexture;
    }

    private void ChangeAnimations(string skinName)
    {
        AnimationClip idle = Resources.Load<AnimationClip>($"DogSkinAndAnimationClip/{skinName}/Idle");
        AnimationClip dying = Resources.Load<AnimationClip>($"DogSkinAndAnimationClip/{skinName}/Dying");
        AnimationClip jump = Resources.Load<AnimationClip>($"DogSkinAndAnimationClip/{skinName}/Jump");
        AnimationClip running = Resources.Load<AnimationClip>($"DogSkinAndAnimationClip/{skinName}/Running");
        AnimationClip sleeping = Resources.Load<AnimationClip>($"DogSkinAndAnimationClip/{skinName}/Sleeping");

        AnimatorOverrideController overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        overrideController["DogIdle"] = idle;
        overrideController["DogDying"] = dying;
        overrideController["DogJump"] = jump;
        overrideController["DogRunnig"] = running;
        overrideController["DogSleeping"] = sleeping;

        animator.runtimeAnimatorController = overrideController;
    }
}
