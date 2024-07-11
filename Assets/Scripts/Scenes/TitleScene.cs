using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : BaseScene
{
	protected override void Init()
	{
		base.Init();

		SceneType = Define.Scene.Title;

		// Managers.UI.ShowSceneUI<UI_Inven>();
	}

	public override void Clear()
	{
		Debug.Log("TiltleScene Clear!");
	}
}
