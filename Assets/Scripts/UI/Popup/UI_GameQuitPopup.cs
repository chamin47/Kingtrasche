using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_GameQuitPopup : UI_Popup
{
	#region enum
	enum GameObjects
	{
	}

	enum Buttons
	{
		GameQuitButton,
		BackTitleButton
	}

	enum Texts
	{
	}

	enum Images
	{
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

		Get<Button>((int)Buttons.GameQuitButton).gameObject.BindEvent(OnClickQuitButton);
		Get<Button>((int)Buttons.BackTitleButton).gameObject.BindEvent(OnClickCancelButton);


		return true;
	}

	private void OnClickQuitButton(PointerEventData eventdata)
	{
		Managers.Sound.Play("switch10", Sound.Effect);
		Application.Quit();
	}

	private void OnClickCancelButton(PointerEventData eventdata)
	{
		Managers.Sound.Play("switch10", Sound.Effect);
		Managers.UI.ClosePopupUI();
	}
}
