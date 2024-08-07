using GameBalance;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class UI_ChapterScene : UI_Scene
{
	#region
	enum GameObjects
	{
	}
	enum Texts
	{
		Chapter1Txt,
		Chapter2Txt,
		Chapter3Txt
	}
	enum Buttons
	{
		BackButton,
		Chapter1Button,
		Chapter2Button,
		Chapter3Button
	}
	enum Images
	{
		Background
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

		Bind<Button>(typeof(Buttons));
		Bind<TMP_Text>(typeof(Texts));
		Bind<Image>(typeof(Images));


		Get<Button>((int)Buttons.Chapter1Button).gameObject.BindEvent(OnClickChapter1Button);
		Get<Button>((int)Buttons.Chapter2Button).gameObject.BindEvent(OnClickChapter2Button);
		Get<Button>((int)Buttons.Chapter3Button).gameObject.BindEvent(OnClickChapter3Button);
		Get<Button>((int)Buttons.BackButton).gameObject.BindEvent(OnClickBackButton);



		return true;
	}

	private void OnClickBackButton(PointerEventData eventData)
	{
		Managers.Sound.Play("switch10", Sound.Effect);
		Managers.Scene.LoadScene(Scene.LobbyScene);
	}

	private void OnClickChapter1Button(PointerEventData eventData)
	{
		Managers.Sound.Play("switch10", Sound.Effect);
		Managers.UI.ShowPopupUI<UI_Chapter1Popup>();
	}

	private void OnClickChapter2Button(PointerEventData eventData)
	{
		Managers.Sound.Play("switch10", Sound.Effect);
		Managers.UI.ShowPopupUI<UI_Chapter2Popup>();
	}

	private void OnClickChapter3Button(PointerEventData eventData)
	{
		Managers.Sound.Play("switch10", Sound.Effect);
		Managers.UI.ShowPopupUI<UI_Chapter3Popup>();
	}
}
