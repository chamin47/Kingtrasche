using UnityEngine;

public class GameScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Scene.RunningScene;
        Time.timeScale = 1.0f;
        Managers.UI.ShowSceneUI<UI_GameScene>();
        Managers.Player.SpawnPlayer();
    }

    public override void Clear()
    {
        Debug.Log("GameScene Clear!");
    }
}
