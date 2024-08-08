using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class UI_Chapter1Popup : UI_Popup
{
    #region
    enum GameObjects
    {
        ResourceContainer
    }
    enum Texts
    {

    }
    enum Buttons
    {
        BackButton,
        Level1,
        Level2,
        Level3,
        Level4,
        Level5,
        Level6,
        Level7,
    }
    enum Images
    {
        Background,
        Level1,
        Level2,
        Level3,
        Level4,
        Level5,
        Level6,
        Level7,
        OneStar1,
        OneStar2,
        OneStar3,
        TwoStar1,
        TwoStar2,
        TwoStar3,
        ThreeStar1,
        ThreeStar2,
        ThreeStar3,
        FourStar1,
        FourStar2,
        FourStar3,
        FiveStar1,
        FiveStar2,
        FiveStar3,
        SixStar1,
        SixStar2,
        SixStar3,
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

        Bind<Button>(typeof(Buttons));
        Bind<TMP_Text>(typeof(Texts));
        Bind<Image>(typeof(Images));
        Bind<GameObject>(typeof(GameObjects));

        Get<Button>((int)Buttons.BackButton).gameObject.BindEvent(OnClickBackButton);
        Get<Button>((int)Buttons.Level1).gameObject.BindEvent(OnClickLevel1Button);
        Get<Button>((int)Buttons.Level2).gameObject.BindEvent(OnClickLevel2Button);
        Get<Button>((int)Buttons.Level3).gameObject.BindEvent(OnClickLevel3Button);
        Get<Button>((int)Buttons.Level4).gameObject.BindEvent(OnClickLevel4Button);
        Get<Button>((int)Buttons.Level5).gameObject.BindEvent(OnClickLevel5Button);
        Get<Button>((int)Buttons.Level6).gameObject.BindEvent(OnClickLevel6Button);
        Get<Button>((int)Buttons.Level7).gameObject.BindEvent(OnClickLevel7Button);

        SetButtonState((int)Buttons.Level1, 1);
        SetButtonState((int)Buttons.Level2, 2);
        SetButtonState((int)Buttons.Level3, 3);
        SetButtonState((int)Buttons.Level4, 4);
        SetButtonState((int)Buttons.Level5, 5);
        SetButtonState((int)Buttons.Level6, 6);
        SetButtonState((int)Buttons.Level7, 7);

        GameObject ResourceContainer = Get<GameObject>((int)GameObjects.ResourceContainer);
        UI_ResourceItem item = Managers.UI.MakeSubItem<UI_ResourceItem>(ResourceContainer.transform);
        RectTransform rectTransform = item.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = Vector2.zero;
        rectTransform.localScale = Vector3.one;

        return true;
    }

    private void SetButtonState(int buttonIndex, int stageNumber)
    {
        Button button = Get<Button>(buttonIndex);
        bool isUnlocked = Managers.Stage.IsStageUnlocked(stageNumber);
        button.interactable = isUnlocked;

        Image buttonImage = button.GetComponent<Image>();
        TMP_Text buttonText = button.GetComponentInChildren<TMP_Text>();

        if (isUnlocked)
        {
            buttonText.enabled = true; // 텍스트를 활성화합니다.
        }
        else
        {
            // 잠긴 상태의 이미지를 설정합니다.
            buttonImage.sprite = Resources.Load<Sprite>("Sprites/LockStageImg");
            buttonText.enabled = false; // 텍스트를 비활성화합니다.
        }
    }

    private void OnClickBackButton(PointerEventData eventData)
    {
        Managers.UI.ClosePopupUI();
    }

    private void OnClickLevel1Button(PointerEventData eventData)
    {
        if (Managers.Stage.IsStageUnlocked(1))
        {
            if (Managers.Game.RunningPlayCount > 0)
            {
                Managers.Sound.Play("switch10", Sound.Effect);
                Managers.Game.RunningPlayCount--; // 러닝 플레이권 차감
                Managers.Scene.LoadScene(Scene.StoryScene);
                PlayerPrefs.SetInt("StageNumber", 1);
                PlayerPrefs.SetInt("StartFrom", 3);
            }
            else
            {
                Debug.Log("러닝 플레이권이 부족합니다.");
            }
        }
        else
        {
            Debug.Log("스테이지가 잠겨 있습니다.");
        }
    }

    private void OnClickLevel2Button(PointerEventData eventData)
    {
        if (Managers.Stage.IsStageUnlocked(2))
        {
            if (Managers.Game.RunningPlayCount > 0)
            {
                Managers.Sound.Play("switch10", Sound.Effect);
                Managers.Game.RunningPlayCount--; // 러닝 플레이권 차감
                LoadingManager.LoadScene("RunningScene");
                PlayerPrefs.SetInt("StageNumber", 2);
            }
            else
            {
                Debug.Log("러닝 플레이권이 부족합니다.");
            }
        }
        else
        {
            Debug.Log("스테이지가 잠겨 있습니다.");
        }
    }
    private void OnClickLevel3Button(PointerEventData eventData)
    {
        if (Managers.Stage.IsStageUnlocked(3))
        {
            if (Managers.Game.RunningPlayCount > 0)
            {
                Managers.Sound.Play("switch10", Sound.Effect);
                Managers.Game.RunningPlayCount--; // 러닝 플레이권 차감
                LoadingManager.LoadScene("RunningScene");
                PlayerPrefs.SetInt("StageNumber", 3);
            }
            else
            {
                Debug.Log("러닝 플레이권이 부족합니다.");
            }
        }
        else
        {
            Debug.Log("스테이지가 잠겨 있습니다.");
        }
    }

    private void OnClickLevel4Button(PointerEventData eventData)
    {
        if (Managers.Stage.IsStageUnlocked(4))
        {
            if (Managers.Game.RunningPlayCount > 0)
            {
                Managers.Sound.Play("switch10", Sound.Effect);
                Managers.Game.RunningPlayCount--; // 러닝 플레이권 차감
                LoadingManager.LoadScene("RunningScene");
                PlayerPrefs.SetInt("StageNumber", 4);
            }
            else
            {
                Debug.Log("러닝 플레이권이 부족합니다.");
            }
        }
        else
        {
            Debug.Log("스테이지가 잠겨 있습니다.");
        }
    }
    private void OnClickLevel5Button(PointerEventData eventData)
    {
        if (Managers.Stage.IsStageUnlocked(5))
        {
            if (Managers.Game.RunningPlayCount > 0)
            {
                Managers.Sound.Play("switch10", Sound.Effect);
                Managers.Game.RunningPlayCount--; // 러닝 플레이권 차감
                Managers.Scene.LoadScene(Scene.StoryScene);
                PlayerPrefs.SetInt("StageNumber", 5);
                PlayerPrefs.SetInt("StartFrom", 3);
            }
            else
            {
                Debug.Log("러닝 플레이권이 부족합니다.");
            }
        }
        else
        {
            Debug.Log("스테이지가 잠겨 있습니다.");
        }
    }
    private void OnClickLevel6Button(PointerEventData eventData)
    {
        if (Managers.Stage.IsStageUnlocked(6))
        {
            if (Managers.Game.RunningPlayCount > 0)
            {
                Managers.Sound.Play("switch10", Sound.Effect);
                Managers.Game.RunningPlayCount--; // 러닝 플레이권 차감
                LoadingManager.LoadScene("RunningScene");
                PlayerPrefs.SetInt("StageNumber", 6);
            }
            else
            {
                Debug.Log("러닝 플레이권이 부족합니다.");
            }
        }
        else
        {
            Debug.Log("스테이지가 잠겨 있습니다.");
        }
    }
    private void OnClickLevel7Button(PointerEventData eventData)
    {
        if (Managers.Stage.IsStageUnlocked(7))
        {
            if (Managers.Game.RunningPlayCount > 0)
            {
                Managers.Sound.Play("switch10", Sound.Effect);
                Managers.Game.RunningPlayCount--; // 러닝 플레이권 차감
                Managers.Scene.LoadScene(Scene.StoryScene);
                PlayerPrefs.SetInt("StageNumber", 7);
                PlayerPrefs.SetInt("StartFrom", 4);
            }
            else
            {
                Debug.Log("러닝 플레이권이 부족합니다.");
            }
        }
        else
        {
            Debug.Log("스테이지가 잠겨 있습니다.");
        }
    }
}
