using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;
using UnityEngine.EventSystems;

public class UI_StageSelectScene : UI_Scene
{
	enum GameObjects
	{
	}

	enum Buttons
	{
		BackButton
	}

	enum Texts
	{
		
	}

	public override bool Init()
	{
		if (base.Init() == false)
			return false;
		Bind<TMP_Text>(typeof(Texts));
		Bind<Button>(typeof(Buttons));
		Get<Button>((int)Buttons.BackButton).gameObject.BindEvent(OnBackButtonClicked);

		return true;
	}

	private void OnBackButtonClicked(PointerEventData eventData)
	{
		Managers.Scene.LoadScene(Scene.Title);
	}
}
