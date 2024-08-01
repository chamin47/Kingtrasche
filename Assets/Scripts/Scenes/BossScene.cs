using UnityEngine;

public class BossScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Scene.BossScene1;
        Time.timeScale = 1.0f;
        Managers.UI.ShowSceneUI<UI_BossScene>();
        Managers.Sound.Play("Boss Fight__LOOP", Sound.Bgm);
        //Managers.Player.SpawnPlayer();
    }

    public override void Clear()
    {
        Debug.Log("BossScene Clear!");
    }
}
