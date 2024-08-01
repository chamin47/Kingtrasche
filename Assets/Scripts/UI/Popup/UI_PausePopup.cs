using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_PausePopup : UI_Popup
{
    #region enum
    enum GameObjects
    {
    }

    enum Buttons
    {
        ContinueButton,
        RetryButton,
        StopButton,
        BGMOn,
        BGMOff,
        SoundEffectOn,
        SoundEffectOff
    }

    enum Texts
    {
        ContinueText,
        RetryText,
        StopText,
        PauseText,
        BGMText,
        SoundEffectText,
    }

    enum Images
    {
        BGMOn,
        BGMOff,
        SoundEffectOn,
        SoundEffectOff
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

        Get<Button>((int)Buttons.BGMOn).gameObject.BindEvent(BackgroundSoundOnOff);
        Get<Button>((int)Buttons.BGMOff).gameObject.BindEvent(BackgroundSoundOnOff);
        Get<Button>((int)Buttons.SoundEffectOn).gameObject.BindEvent(EffectSoundOnOff);
        Get<Button>((int)Buttons.SoundEffectOff).gameObject.BindEvent(EffectSoundOnOff);
        Get<Button>((int)Buttons.ContinueButton).gameObject.BindEvent(OnClickContinueButton);
        Get<Button>((int)Buttons.RetryButton).gameObject.BindEvent(OnClickRetryButton);
        Get<Button>((int)Buttons.StopButton).gameObject.BindEvent(OnClickStopButton);

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

    private void BackgroundSoundOnOff(PointerEventData eventdata)
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

    private void OnClickContinueButton(PointerEventData eventdata)
    {
        Managers.Sound.Play("switch10", Sound.Effect);
        Managers.UI.ClosePopupUI(this);
        Time.timeScale = 1.0f;
    }

    private void OnClickStopButton(PointerEventData eventdata)
    {
        Managers.Sound.Play("switch10", Sound.Effect);
        UI_GameScene.currentScore = 0;
        Managers.Scene.LoadScene(Scene.StageScene);
    }
}
