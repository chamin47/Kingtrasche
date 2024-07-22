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

	private int currentStage;

	private void Awake()
	{
		Init();
	}

	private void Start()
	{
		currentStage = PlayerPrefs.GetInt("StageNumber");
		Debug.Log($"currentStage : {currentStage}");
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
		if (currentStage == 4)
		{
			Managers.Scene.LoadScene(Scene.Boss);
			PlayerPrefs.SetInt("StageNumber", ++currentStage);
		}
		else if (currentStage == 9)
		{
			Managers.Scene.LoadScene(Scene.Boss2);
			PlayerPrefs.SetInt("StageNumber", ++currentStage);
		}
		else if (currentStage == 14)
		{
			// 15 스테이지 보스씬으로 이동시키기
			PlayerPrefs.SetInt("StageNumber", ++currentStage);
		}
		else
		{
			PlayerPrefs.SetInt("StageNumber", ++currentStage);
			Managers.Scene.LoadScene(Scene.Game);
		}		
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
