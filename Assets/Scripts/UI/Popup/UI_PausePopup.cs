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
        BGMOnOff,
        SoundEffectOnOff
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
        BGMOnOff,
        SoundEffectOnOff
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

        Get<Button>((int)Buttons.SoundEffectOnOff).gameObject.BindEvent(EffectSoundOnOff);
        Get<Button>((int)Buttons.BGMOnOff).gameObject.BindEvent(BackgroundSoundOnOff);
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
    }

    private void BackgroundSoundOnOff(PointerEventData eventdata)
    {
        Managers.Sound.Play("switch10", Sound.Effect);
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
        Managers.Scene.LoadScene(Scene.StageSelect);
    }
}
