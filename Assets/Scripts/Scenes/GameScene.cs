using UnityEngine;

public class GameScene : BaseScene
{
	protected override void Init()
	{
		base.Init();

		SceneType = Scene.RunningScene;
		Time.timeScale = 1.0f;
		Managers.UI.ShowSceneUI<UI_GameScene>();
	}

	public override void Clear()
	{
		Debug.Log("GameScene Clear!");
	}
}
