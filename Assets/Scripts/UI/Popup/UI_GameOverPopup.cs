using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UI_GameOverPopup : UI_Popup
{
	#region enum
	enum GameObjects
	{
	}

	enum Buttons
	{
		RetryButton,
		BackStageButton,
	}

	enum Texts
	{
		RetryText,
		BackStageText,
		GameOverText,
		DogText
	}

	enum Images
	{
		DogImage
	}

	#endregion

	private void Awake()
	{
		Init();
	}

	public override bool Init()
	{
		if (base.Init() == false)
			return false;
		Bind<TMP_Text>(typeof(Texts));
		Bind<Button>(typeof(Buttons));
		Bind<Image>(typeof(Images));

		Get<Button>((int)Buttons.RetryButton).gameObject.BindEvent(OnClickRetryButton);
		Get<Button>((int)Buttons.BackStageButton).gameObject.BindEvent(OnClickBackStageButton);


		Refresh();
		return true;
	}

	public void SetInfo()
	{
		Refresh();
	}

	private void Refresh()
	{

	}

	private void OnClickRetryButton(PointerEventData eventData)
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		Time.timeScale = 1.0f;
		// 목줄 하나 감소하는 로직 필요
	}

	private void OnClickBackStageButton(PointerEventData eventData)
	{
		Managers.Scene.LoadScene(Scene.StageSelect);
	}
}
