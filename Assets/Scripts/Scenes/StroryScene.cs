using UnityEngine;

public class StoryScene : BaseScene
{
	protected override void Init()
	{
		base.Init();

		SceneType = Scene.StoryScene;
		Time.timeScale = 1.0f;
		Managers.UI.ShowSceneUI<UI_StoryScene>();
	}

	public override void Clear()
	{
		Debug.Log("StoryScene Clear!");
	}
}
