using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
        Get<Button>((int)Buttons.MakePeopleButton).gameObject.BindEvent(MakePeople);
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
        Managers.Sound.Play("switch10", Sound.Effect);
        Managers.UI.ShowPopupUI<UI_MakePeoplePopup>();
    }

    public void OnClickCloseButton(PointerEventData eventData)
    {
        Managers.Sound.Play("switch10", Sound.Effect);
        Managers.UI.ClosePopupUI(this);
    }
}
