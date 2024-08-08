using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_NoPlayPopup : UI_Popup
{
	enum Buttons
	{
		CheckBtn,
		BackBtn
	}

	private void Awake()
	{
		Init();
	}

	public override bool Init()
	{
		if (base.Init() == false)
		{
			return false;
		}
		Bind<Button>(typeof(Buttons));

		Get<Button>((int)Buttons.CheckBtn).gameObject.BindEvent(OnCheckBtnClicked);
		Get<Button>((int)Buttons.BackBtn).gameObject.BindEvent(OnBackBtnClicked);

		return true;
	}

	private void OnCheckBtnClicked(PointerEventData eventData)
	{
		Managers.Sound.Play("switch10", Sound.Effect);
		Managers.UI.ClosePopupUI();
	}

	private void OnBackBtnClicked(PointerEventData eventData)
	{
		Managers.Sound.Play("switch10", Sound.Effect);
		Managers.UI.ClosePopupUI(this);
	}

}
