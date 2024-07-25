using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_TitleScene : UI_Scene
{
    enum GameObjects
    {

    }

    enum Buttons
    {
        StartButton,
        OptionButton,
        MissionButton,
    }

    enum Texts
    {
        StartText,
        OptionText,
        MissionText,
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        Bind<TMP_Text>(typeof(Texts));
        Bind<Button>(typeof(Buttons));
        Bind<GameObject>(typeof(GameObjects));

        Get<Button>((int)Buttons.StartButton).gameObject.BindEvent(OnStartButtonClicked);
        Get<Button>((int)Buttons.OptionButton).gameObject.BindEvent(OnOptionButtonClicked);
        return true;
    }

    private void Awake()
    {
        Init();
    }

    private void OnStartButtonClicked(PointerEventData eventData)
    {
        Managers.Sound.Play("switch10", Sound.Effect);
        Managers.Scene.LoadScene(Scene.StageSelect);
    }

    private void OnOptionButtonClicked(PointerEventData eventData)
    {
        Managers.Sound.Play("switch10", Sound.Effect);
        Managers.UI.ShowPopupUI<UI_SettingPopup>();
    }
}
