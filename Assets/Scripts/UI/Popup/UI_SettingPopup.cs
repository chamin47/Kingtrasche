using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_SettingPopup : UI_Popup
{
	#region enum
	enum GameObjects
	{
	}

	enum Buttons
	{
		BGMButton,
		SoundEffectButton,
		MakePeopleButton,
		ClosePopupButton
	}

	enum Texts
	{
		BGMText,
		SoundEffectText,
		MakePeopleText,
		OptionText
	}

	enum Images
	{
		BGMOnOff,
		SoundEffectOnOff,
		MakePeople
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

		Get<Button>((int)Buttons.SoundEffectButton).gameObject.BindEvent(EffectSoundOnOff);
		Get<Button>((int)Buttons.BGMButton).gameObject.BindEvent(BackgroundSoundOnOff);
		Get<Button>((int)(Buttons.MakePeopleButton)).gameObject.BindEvent(MakePeople);
		Get<Button>((int)Buttons.ClosePopupButton).gameObject.BindEvent(OnClickCloseButton);

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

	private void EffectSoundOnOff(PointerEventData eventData)
	{

	}

	private void BackgroundSoundOnOff(PointerEventData eventData)
	{

	}

	private void MakePeople(PointerEventData eventData)
	{
		Managers.UI.ShowPopupUI<UI_MakePeoplePopup>();
	}

	public void OnClickCloseButton(PointerEventData eventData)
	{
		Managers.UI.ClosePopupUI(this);
	}
}
