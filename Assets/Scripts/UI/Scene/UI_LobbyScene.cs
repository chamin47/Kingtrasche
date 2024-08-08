using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_LobbyScene : UI_Scene
{
    enum GameObjects
    {
		UI_Lobby
	}

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
        Managers.Sound.Play("Caketown 1", Sound.Bgm);
    }

	public override bool Init()
    {
        if (base.Init() == false)
            return false;

        Bind<Button>(typeof(Buttons));
        Bind<GameObject>(typeof(GameObjects));

        Get<Button>((int)Buttons.Setting).gameObject.BindEvent(OnOptionButtonClicked);
        Get<Button>((int)Buttons.Shop).gameObject.BindEvent(OnShopButtonClicked);
        Get<Button>((int)Buttons.DogCollection).gameObject.BindEvent(OnDogCollectionButtonClicked);
        Get<Button>((int)Buttons.Gift).gameObject.BindEvent(OnGiftButtonClicked);
        Get<Button>((int)Buttons.ClueCollection).gameObject.BindEvent(OnClueCollectionButtonClicked);
        Get<Button>((int)Buttons.Mission).gameObject.BindEvent(OnMissionButtonClicked);
        Get<Button>((int)Buttons.Back).gameObject.BindEvent(OnBackButtonClicked);
        Get<Button>((int)Buttons.Start).gameObject.BindEvent(OnStartButtonClicked);

        GameObject ResourceContainer = Get<GameObject>((int)GameObjects.UI_Lobby);
        UI_ResourceItem item = Managers.UI.MakeSubItem<UI_ResourceItem>(ResourceContainer.transform);
		RectTransform rectTransform = item.GetComponent<RectTransform>();		
		rectTransform.anchoredPosition = Vector2.zero; // 부모의 중앙에 위치		
		rectTransform.localScale = Vector3.one;

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
        Managers.UI.ShowPopupUI<UI_OpenWaitPopup>();
    }

    private void OnDogCollectionButtonClicked(PointerEventData eventData)
    {
        Managers.Sound.Play("switch10", Sound.Effect);
        Managers.UI.ShowPopupUI<UI_DogSkinPopup>();
    }

    private void OnGiftButtonClicked(PointerEventData eventData)
    {
        Managers.Sound.Play("switch10", Sound.Effect);
		Managers.UI.ShowPopupUI<UI_OpenWaitPopup>();
	}

    private void OnClueCollectionButtonClicked(PointerEventData eventData)
    {
        Managers.Sound.Play("switch10", Sound.Effect);
		Managers.UI.ShowPopupUI<UI_OpenWaitPopup>();

	}

    private void OnMissionButtonClicked(PointerEventData eventData)
    {
        Managers.Sound.Play("switch10", Sound.Effect);
        Managers.UI.ShowPopupUI<UI_MissionPopup>();
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
