using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class UI_Chapter2Popup : UI_Popup
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
        Level8,
        Level9,
        Level10,
        Level11,
        Level12,
        Level13,
        Level14,
    }
    enum Images
    {
        Background,
        Level8,
        Level9,
        Level10,
        Level11,
        Level12,
        Level13,
        Level14,
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
        Get<Button>((int)Buttons.Level8).gameObject.BindEvent(OnClickLevel8Button);
        Get<Button>((int)Buttons.Level9).gameObject.BindEvent(OnClickLevel9Button);
        Get<Button>((int)Buttons.Level10).gameObject.BindEvent(OnClickLevel10Button);
        Get<Button>((int)Buttons.Level11).gameObject.BindEvent(OnClickLevel11Button);
        Get<Button>((int)Buttons.Level12).gameObject.BindEvent(OnClickLevel12Button);
        Get<Button>((int)Buttons.Level13).gameObject.BindEvent(OnClickLevel13Button);
        Get<Button>((int)Buttons.Level14).gameObject.BindEvent(OnClickLevel14Button);

        SetButtonState((int)Buttons.Level8, 8);
        SetButtonState((int)Buttons.Level9, 9);
        SetButtonState((int)Buttons.Level10, 10);
        SetButtonState((int)Buttons.Level11, 11);
        SetButtonState((int)Buttons.Level12, 12);
        SetButtonState((int)Buttons.Level13, 13);
        SetButtonState((int)Buttons.Level14, 14);

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

    private void OnClickLevel8Button(PointerEventData eventData)
    {
        if (Managers.Stage.IsStageUnlocked(8))
        {
            if (Managers.Game.RunningPlayCount > 0)
            {
                Managers.Sound.Play("switch10", Sound.Effect);
                Managers.Game.RunningPlayCount--; // 러닝 플레이권 차감
                Managers.Scene.LoadScene(Scene.StoryScene);
                PlayerPrefs.SetInt("StageNumber", 8);
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
    private void OnClickLevel9Button(PointerEventData eventData)
    {
        if (Managers.Stage.IsStageUnlocked(9))
        {
            if (Managers.Game.RunningPlayCount > 0)
            {
                Managers.Sound.Play("switch10", Sound.Effect);
                Managers.Game.RunningPlayCount--; // 러닝 플레이권 차감
                LoadingManager.LoadScene("RunningScene");
                PlayerPrefs.SetInt("StageNumber", 9);
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
    private void OnClickLevel10Button(PointerEventData eventData)
    {
        if (Managers.Stage.IsStageUnlocked(10))
        {
            if (Managers.Game.RunningPlayCount > 0)
            {
                Managers.Sound.Play("switch10", Sound.Effect);
                Managers.Game.RunningPlayCount--; // 러닝 플레이권 차감
                LoadingManager.LoadScene("RunningScene");
                PlayerPrefs.SetInt("StageNumber", 10);
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
    private void OnClickLevel11Button(PointerEventData eventData)
    {
        if (Managers.Stage.IsStageUnlocked(11))
        {
            if (Managers.Game.RunningPlayCount > 0)
            {
                Managers.Sound.Play("switch10", Sound.Effect);
                Managers.Game.RunningPlayCount--; // 러닝 플레이권 차감
                Managers.Scene.LoadScene(Scene.StoryScene);
                PlayerPrefs.SetInt("StageNumber", 11);
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
    private void OnClickLevel12Button(PointerEventData eventData)
    {
        if (Managers.Stage.IsStageUnlocked(12))
        {
            if (Managers.Game.RunningPlayCount > 0)
            {
                Managers.Sound.Play("switch10", Sound.Effect);
                Managers.Game.RunningPlayCount--; // 러닝 플레이권 차감
                LoadingManager.LoadScene("RunningScene");
                PlayerPrefs.SetInt("StageNumber", 12);
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
    private void OnClickLevel13Button(PointerEventData eventData)
    {
        if (Managers.Stage.IsStageUnlocked(13))
        {
            if (Managers.Game.RunningPlayCount > 0)
            {
                Managers.Sound.Play("switch10", Sound.Effect);
                Managers.Game.RunningPlayCount--; // 러닝 플레이권 차감
                LoadingManager.LoadScene("RunningScene");
                PlayerPrefs.SetInt("StageNumber", 13);
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
    private void OnClickLevel14Button(PointerEventData eventData)
    {
        if (Managers.Stage.IsStageUnlocked(14))
        {
            if (Managers.Game.RunningPlayCount > 0)
            {
                Managers.Sound.Play("switch10", Sound.Effect);
                Managers.Game.RunningPlayCount--; // 러닝 플레이권 차감
                Managers.Scene.LoadScene(Scene.StoryScene);
                PlayerPrefs.SetInt("StageNumber", 14);
                PlayerPrefs.SetInt("StartFrom", 5);
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
