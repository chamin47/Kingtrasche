using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_DailyBoxPopup : UI_Popup
{
	#region enum
	enum GameObjects
	{
	}
	enum Buttons
	{
		BoxDrawButton,
		ViewAdButton,
		BackButton
	}
	enum Texts
	{
		BoxDrawTxt,
		ViewAdTxt
	}
	enum Images
	{
		DailyBoxImg
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


		Get<Button>((int)Buttons.BackButton).gameObject.BindEvent(OnClickBackButton);

		return true;
	}

	private void OnClickBoxDrawButton(PointerEventData eventData)
	{

	}

	private void OnClickViewAdButton(PointerEventData eventData)
	{

	}

	private void OnClickBackButton(PointerEventData eventData)
	{
		Managers.UI.ClosePopupUI();
	}
}
