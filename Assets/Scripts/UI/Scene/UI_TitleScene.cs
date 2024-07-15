using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;
using UnityEngine.EventSystems;

public class UI_TitleScene : UI_Scene
{
   enum GameObjects
	{
		
	}

	enum Buttons
	{
		StartButton,
		OptionButton,
		MissionButton,
	}

	enum Texts
	{
		StartText,
		OptionText,
		MissionText,
	}

	public override bool Init()
	{
		if (base.Init() == false)
			return false;

		Bind<TMP_Text>(typeof(Texts));
		Bind<Button>(typeof(Buttons));
		Bind<GameObject>(typeof(GameObjects));

		Get<Button>((int)Buttons.StartButton).gameObject.BindEvent(OnStartButtonClicked);
		Get<Button>((int)Buttons.OptionButton).gameObject.BindEvent(OnOptionButtonClicked);
		return true;
	}

	private void Awake()
	{
		Init();
	}

	private void OnStartButtonClicked(PointerEventData eventData)
	{
		Managers.Scene.LoadScene(Scene.StageSelect);
	}

	private void OnOptionButtonClicked(PointerEventData eventData)
	{
		Managers.UI.ShowPopupUI<UI_SettingPopup>();
	}
}
