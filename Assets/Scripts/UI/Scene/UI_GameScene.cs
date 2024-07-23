using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_GameScene : UI_Scene
{
    #region enum
    enum GameObjects
    {
    }

    enum Buttons
    {
        PauseButton
    }

    enum Texts
    {
        CurrentMeatText
    }

    enum Images
    {

    }
    #endregion

    private static int currentScore;
    private static TMP_Text currentMeat;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        Bind<TMP_Text>(typeof(Texts));
        Bind<Button>(typeof(Buttons));
        Bind<Image>(typeof(Images));

        Get<Button>((int)Buttons.PauseButton).gameObject.BindEvent(OnClickPauseButton);
        currentMeat = GetText((int)Texts.CurrentMeatText);

        return true;
    }

    private void OnClickPauseButton(PointerEventData eventData)
    {
        Managers.UI.ShowPopupUI<UI_PausePopup>();
        Time.timeScale = 0;
    }

    public static void AddScore()
    {
        currentScore += 10;
        currentMeat.text = currentScore.ToString();
    }
}
