using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_TitleScene : UI_Scene
{
    enum GameObjects
    {

    }

    enum Buttons
    {
		Background,
        OptionButton,
    }

    enum Texts
    {
        StartText,
    }

	private TMP_Text startText;
	private Coroutine blinkCoroutine;

	public override bool Init()
    {
        if (base.Init() == false)
            return false;

        Bind<TMP_Text>(typeof(Texts));
        Bind<Button>(typeof(Buttons));
        Bind<GameObject>(typeof(GameObjects));

        Get<Button>((int)Buttons.Background).gameObject.BindEvent(OnStartButtonClicked);
        Get<Button>((int)Buttons.OptionButton).gameObject.BindEvent(OnOptionButtonClicked);

		startText = Get<TMP_Text>((int)Texts.StartText);

		blinkCoroutine = StartCoroutine(BlinkText());

		return true;
    }

    private void Awake()
    {
        Init();
    }

    private void OnStartButtonClicked(PointerEventData eventData)
    {
        Managers.Sound.Play("switch10", Sound.Effect);
        if (PlayerPrefs.GetInt("TitleFirstTimePlaying") == 0)
        {
            PlayerPrefs.SetInt("TitleFirstTimePlaying", 1);
            PlayerPrefs.SetInt("StageNumber", 1000);
            PlayerPrefs.SetInt("StartFrom", 1);
            PlayerPrefs.Save();

            LoadingManager.LoadScene("StoryScene");
        }
        else
        {
            LoadingManager.LoadScene("LobbyScene");
            //Managers.Scene.LoadScene(Scene.LobbyScene);
        }
    }

    private void OnOptionButtonClicked(PointerEventData eventData)
    {
        Managers.Sound.Play("switch10", Sound.Effect);
        Managers.UI.ShowPopupUI<UI_SettingPopup>();
    }

	private IEnumerator BlinkText()
	{
		while (true)
		{
			startText.alpha = 1; // Show text
			yield return new WaitForSeconds(0.5f); // Wait for 0.5 seconds
			startText.alpha = 0; // Hide text
			yield return new WaitForSeconds(0.5f); // Wait for 0.5 seconds
		}
	}

	private void OnDestroy()
	{
		if (blinkCoroutine != null)
		{
			StopCoroutine(blinkCoroutine);
		}
	}
}
