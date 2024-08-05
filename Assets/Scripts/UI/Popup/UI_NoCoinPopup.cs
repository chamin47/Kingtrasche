using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_NoCoinPopup : UI_Popup
{
    enum Buttons
    {
        GoBuyBtn,
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

        Get<Button>((int)Buttons.GoBuyBtn).gameObject.BindEvent(OnGoBuyBtnClicked);
        Get<Button>((int)Buttons.BackBtn).gameObject.BindEvent(OnBackBtnClicked);

        return true;
    }

    private void OnGoBuyBtnClicked(PointerEventData eventData)
    {
        Managers.Sound.Play("switch10", Sound.Effect);
    }

    private void OnBackBtnClicked(PointerEventData eventData)
    {
        Managers.Sound.Play("switch10", Sound.Effect);
        Managers.UI.ClosePopupUI(this);
    }

}
