using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScene : BaseScene
{
	protected override void Init()
	{
		base.Init();

		SceneType = Scene.Boss;
		Time.timeScale = 1.0f;
		Managers.UI.ShowSceneUI<UI_BossScene>();
	}

	public override void Clear()
	{
		Debug.Log("BossScene Clear!");
	}
}
