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

		// Managers.UI.ShowSceneUI<UI_Inven>();
	}

	public override void Clear()
	{
		Debug.Log("TiltleScene Clear!");
	}
}
