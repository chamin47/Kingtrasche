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
        BGMOn,
        BGMOff,
        SoundEffectOn,
        SoundEffectOff,
        MakePeople
    }
    #endregion

    private void Awake()
    {
        Init();
        EffectSoundImageUpdate();
        BgmImageUpdate();
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
        Managers.Sound.Play("switch10", Sound.Effect);
        Managers.Sound.ONOffEffect();
        EffectSoundImageUpdate();
    }

    private void EffectSoundImageUpdate()
    {
        Image on = Get<Image>((int)Images.SoundEffectOn);
        Image off = Get<Image>((int)Images.SoundEffectOff);
        if (Managers.Sound._isEffectOn == false)
        {
            on.gameObject.SetActive(false);
            off.gameObject.SetActive(true);

        }
        else if (Managers.Sound._isEffectOn == true)
        {
            on.gameObject.SetActive(true);
            off.gameObject.SetActive(false);
        }
    }

    private void BackgroundSoundOnOff(PointerEventData eventData)
    {
        Managers.Sound.Play("switch10", Sound.Effect);
        Managers.Sound.OnOffBgm();
        BgmImageUpdate();
    }

    private void BgmImageUpdate()
    {
        Image on = Get<Image>((int)Images.BGMOn);
        Image off = Get<Image>((int)Images.BGMOff);

        if (Managers.Sound._isBgmOn == false)
        {
            on.gameObject.SetActive(false);
            off.gameObject.SetActive(true);

        }
        else if (Managers.Sound._isBgmOn == true)
        {
            on.gameObject.SetActive(true);
            off.gameObject.SetActive(false);
        }
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
