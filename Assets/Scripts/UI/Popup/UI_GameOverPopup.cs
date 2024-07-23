using TMPro;
using UnityEngine.UI;

public class UI_GameOverPopup : UI_Popup
{
    #region enum
    enum GameObjects
    {
    }

    enum Buttons
    {
        RetryButton,
        BackStageButton,
    }

    enum Texts
    {
        RetryText,
        BackStageText,
        GameOverText,
        DogText
    }

    enum Images
    {
        DogImage
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

        Get<Button>((int)Buttons.RetryButton).gameObject.BindEvent(OnClickRetryButton);
        Get<Button>((int)Buttons.BackStageButton).gameObject.BindEvent(OnClickBackStageButton);


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
}
