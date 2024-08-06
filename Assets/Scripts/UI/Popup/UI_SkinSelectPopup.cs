using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_SkinSelectPopup : UI_Popup
{
    enum Buttons
    {
        OKBtn,
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

        Get<Button>((int)Buttons.OKBtn).gameObject.BindEvent(OnOKBtnClicked);

        return true;
    }

    private void OnOKBtnClicked(PointerEventData eventData)
    {
        Managers.Sound.Play("switch10", Sound.Effect);
        Managers.UI.ClosePopupUI(this);
    }
}
