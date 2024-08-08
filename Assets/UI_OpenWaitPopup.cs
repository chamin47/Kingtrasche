using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_OpenWaitPopup : UI_Popup
{
	#region enum
	enum GameObjects
	{

	}

	enum Buttons
	{
		CheckBtn
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

		Get<Button>((int)Buttons.CheckBtn).gameObject.BindEvent(OnclickCheckButton);

		return true;
	}

	private void OnclickCheckButton(PointerEventData eventData)
	{
		Managers.UI.ClosePopupUI();
	}
}
