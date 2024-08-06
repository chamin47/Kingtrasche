using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_ModeSelectPopup : UI_Popup
{
	#region enum
	enum GameObjects
	{
	}

	enum Buttons
	{
		StoryModeButton,
		InfinityModeButton,
		BackButton
	}
	#endregion

	public override bool Init()
	{
		if (base.Init() == false) 
			return false;
		Bind<Button>(typeof(Buttons));

		Get<Button>((int)Buttons.StoryModeButton).gameObject.BindEvent(OnclickStoryButton);
		Get<Button>((int)Buttons.InfinityModeButton).gameObject.BindEvent(OnClickInfinityButton);
		Get<Button>((int)Buttons.BackButton).gameObject.BindEvent(OnClickBackButton);

		return true;
	}

	private void OnclickStoryButton(PointerEventData eventData)
	{
		if (PlayerPrefs.GetInt("StoryFirstTimePlaying") == 1)
		{
			PlayerPrefs.SetInt("StoryFirstTimePlaying", 1);
			PlayerPrefs.SetInt("StageNumber", 1001);
			PlayerPrefs.SetInt("StartFrom", 2);
			PlayerPrefs.Save();
			Managers.Scene.LoadScene(Scene.StoryScene);
		}
		else
		{
			Managers.Scene.LoadScene(Scene.StageScene);
		}
	}

	private void OnClickInfinityButton(PointerEventData eventData)
	{
		Managers.Scene.LoadScene(Scene.InfinityRunningScene);
	}

	private void OnClickBackButton(PointerEventData eventData)
	{
		Managers.UI.ClosePopupUI();
	}		
}
