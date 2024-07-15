using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectScene : BaseScene
{
	protected override void Init()
	{
		base.Init();

		SceneType = Scene.StageSelect;

		Managers.UI.ShowSceneUI<UI_StageSelectScene>();
	}

	public override void Clear()
	{
		Debug.Log("StageSelectScene Clear!");
	}
}
