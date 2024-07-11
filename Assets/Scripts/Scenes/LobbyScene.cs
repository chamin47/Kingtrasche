using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyScene : BaseScene
{
	protected override void Init()
	{
		base.Init();

		SceneType = Define.Scene.Lobby;

		//TitleUI
		Managers.UI.ShowSceneUI<UI_LobbyScene>();

		// Managers.Sound.Play(Define.Sound.Bgm, "Bgm_Lobby");
	}

	public override void Clear()
	{

	}

}
