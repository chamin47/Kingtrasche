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

	private void Start()
	{
		base.Init();
		Bind<TMP_Text>(typeof(Texts));
		Bind<Button>(typeof(Buttons));
		Bind<GameObject>(typeof(GameObjects));

		Get<Button>((int)Buttons.StartButton).gameObject.BindEvent(OnButtonClicked);
	}

	private void OnButtonClicked(PointerEventData eventData)
	{
		Managers.Scene.LoadScene(Scene.Stage);
	}
}
