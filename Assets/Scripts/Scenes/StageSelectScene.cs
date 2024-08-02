using UnityEngine;

public class StageSelectScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Scene.StageScene;
        Time.timeScale = 1.0f;
        Managers.UI.ShowSceneUI<UI_StageSelectScene>();
        Managers.Sound.Play("BGM_04", Sound.Bgm);
    }

    public override void Clear()
    {
        Debug.Log("StageSelectScene Clear!");
    }
}
