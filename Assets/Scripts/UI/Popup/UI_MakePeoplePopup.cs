using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_MakePeoplePopup : UI_Popup
{
	enum GameObjects
	{
	}
	
	enum Buttons
	{
		ClosePopupButton
	}

	public override bool Init()
	{
		if (base.Init() == false)
			return false;
		Bind<Button>(typeof(Buttons));

		GetButton((int)Buttons.ClosePopupButton).gameObject.BindEvent(OnClickCloseButton);


		return true;
	}

	public void OnClickCloseButton(PointerEventData eventData)
	{
		Managers.UI.ClosePopupUI(this);
	}
}
