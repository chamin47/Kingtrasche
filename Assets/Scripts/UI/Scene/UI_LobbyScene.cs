using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_LobbyScene : UI_Scene
{
    enum Buttons
    {
        Setting,
        Shop,
        DogCollection,
        Gift,
        ClueCollection,
        Mission,
        Back,
        Start
    }

    private void Awake()
    {
        Init();
        Managers.Player.SpawnPlayer();
        Managers.Sound.Play("Nostalgia__LOOP", Sound.Bgm);
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        Bind<Button>(typeof(Buttons));

        Get<Button>((int)Buttons.Setting).gameObject.BindEvent(OnOptionButtonClicked);
        Get<Button>((int)Buttons.Shop).gameObject.BindEvent(OnShopButtonClicked);
        Get<Button>((int)Buttons.DogCollection).gameObject.BindEvent(OnDogCollectionButtonClicked);
        Get<Button>((int)Buttons.Gift).gameObject.BindEvent(OnGiftButtonClicked);
        Get<Button>((int)Buttons.ClueCollection).gameObject.BindEvent(OnClueCollectionButtonClicked);
        Get<Button>((int)Buttons.Mission).gameObject.BindEvent(OnMissionButtonClicked);
        Get<Button>((int)Buttons.Back).gameObject.BindEvent(OnBackButtonClicked);
        Get<Button>((int)Buttons.Start).gameObject.BindEvent(OnStartButtonClicked);

        return true;
    }

    private void OnOptionButtonClicked(PointerEventData eventData)
    {
        Managers.Sound.Play("switch10", Sound.Effect);
        Managers.UI.ShowPopupUI<UI_SettingPopup>();
    }

    private void OnShopButtonClicked(PointerEventData eventData)
    {
        Managers.Sound.Play("switch10", Sound.Effect);

    }

    private void OnDogCollectionButtonClicked(PointerEventData eventData)
    {
        Managers.Sound.Play("switch10", Sound.Effect);
        Managers.UI.ShowPopupUI<UI_DogSkinPopup>();

    }

    private void OnGiftButtonClicked(PointerEventData eventData)
    {
        Managers.Sound.Play("switch10", Sound.Effect);
        Managers.UI.ShowPopupUI<UI_DailyBoxPopup>();
    }

    private void OnClueCollectionButtonClicked(PointerEventData eventData)
    {
        Managers.Sound.Play("switch10", Sound.Effect);

    }

    private void OnMissionButtonClicked(PointerEventData eventData)
    {
        Managers.Sound.Play("switch10", Sound.Effect);

    }

    private void OnBackButtonClicked(PointerEventData eventData)
    {
        Managers.Sound.Play("switch10", Sound.Effect);
        Managers.Scene.LoadScene(Scene.TitleScene);
    }

    private void OnStartButtonClicked(PointerEventData eventData)
    {
        Managers.Sound.Play("switch10", Sound.Effect);
        Managers.UI.ShowPopupUI<UI_ModeSelectPopup>();
    }
}
