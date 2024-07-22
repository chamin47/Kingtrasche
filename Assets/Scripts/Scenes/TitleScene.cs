using System.Collections;
using System.Collections.Generic;
using UGS;
using UnityEngine;

public class TitleScene : BaseScene
{
	protected override void Init()
	{
		base.Init();

		SceneType = Scene.Title;

		Time.timeScale = 1.0f;
		Managers.UI.ShowSceneUI<UI_TitleScene>();
	}

	public override void Clear()
	{
		Debug.Log("TiltleScene Clear!");
	}
}
