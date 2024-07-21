using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_StageClearPopup : UI_Popup
{
	#region enum
	enum GameObjects
	{
	}

	enum Buttons
	{
		NextStageButton,
		RetryButton,
		BackStageButton
	}

	enum Texts
	{
		StageClearText,
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

		Get<Button>((int)Buttons.NextStageButton).gameObject.BindEvent(OnClickNextStageButton);
		Get<Button>((int)Buttons.RetryButton).gameObject.BindEvent(OnClickRetryButton);
		Get<Button>((int)Buttons.BackStageButton).gameObject.BindEvent(OnClickBackStageButton);

		return true;
	}

	private void OnClickNextStageButton(PointerEventData eventData)
	{

	}

	private void OnClickRetryButton(PointerEventData eventData)
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		Time.timeScale = 1.0f;
		// ���� �ϳ� �����ϴ� ���� �ʿ�
	}

	private void OnClickBackStageButton(PointerEventData eventData)
	{
		Managers.Scene.LoadScene(Scene.StageSelect);
	}
}
