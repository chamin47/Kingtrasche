using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class UI_Chapter3Popup : UI_Popup
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
        Level15,
        Level16,
        Level17,
        Level18,
        Level19,
        Level20,
        Level21,
    }
    enum Images
    {
        Background,
        Level15,
        Level16,
        Level17,
        Level18,
        Level19,
        Level20,
        Level21,
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
        Get<Button>((int)Buttons.Level15).gameObject.BindEvent(OnClickLevel15Button);
        Get<Button>((int)Buttons.Level16).gameObject.BindEvent(OnClickLevel16Button);
        Get<Button>((int)Buttons.Level17).gameObject.BindEvent(OnClickLevel17Button);
        Get<Button>((int)Buttons.Level18).gameObject.BindEvent(OnClickLevel18Button);
        Get<Button>((int)Buttons.Level19).gameObject.BindEvent(OnClickLevel19Button);
        Get<Button>((int)Buttons.Level20).gameObject.BindEvent(OnClickLevel20Button);
        Get<Button>((int)Buttons.Level21).gameObject.BindEvent(OnClickLevel21Button);

        SetButtonState((int)Buttons.Level15, 15);
        SetButtonState((int)Buttons.Level16, 16);
        SetButtonState((int)Buttons.Level17, 17);
        SetButtonState((int)Buttons.Level18, 18);
        SetButtonState((int)Buttons.Level19, 19);
        SetButtonState((int)Buttons.Level20, 20);
        SetButtonState((int)Buttons.Level21, 21);

        GameObject ResourceContainer = Get<GameObject>((int)GameObjects.ResourceContainer);
        UI_ResourceItem item = Managers.UI.MakeSubItem<UI_ResourceItem>(ResourceContainer.transform);
        RectTransform rectTransform = item.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = Vector2.zero;
        rectTransform.localScale = Vector3.one;

        return true;
    }

    private string ConvertNumberToWord(int number)
    {
        switch (number)
        {
            case 15: return "One";
            case 16: return "Two";
            case 17: return "Three";
            case 18: return "Four";
            case 19: return "Five";
            case 20: return "Six";
            case 21: return "Seven";
            // 필요한 만큼 추가
            default: return number.ToString();
        }
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

            int starCount = Managers.Stage.GetStarCount(stageNumber);
            SetStarImages(stageNumber, starCount, true);
        }
        else
        {
            // 잠긴 상태의 이미지를 설정합니다.
            buttonImage.sprite = Resources.Load<Sprite>("Sprites/LockStageImg");
            buttonText.enabled = false; // 텍스트를 비활성화합니다.
            SetStarImages(stageNumber, 0, false); // 잠긴 스테이지의 별 이미지를 비활성화합니다.
        }
    }

    private void SetStarImages(int stageNumber, int starCount, bool isUnlocked)
    {
        string stageWord = ConvertNumberToWord(stageNumber);
        for (int i = 1; i <= 3; i++)
        {
            Image starImage = Get<Image>((int)System.Enum.Parse(typeof(Images), $"{stageWord}Star{i}"));
            if (isUnlocked)
            {
                starImage.enabled = i <= starCount; // 별 이미지를 활성화
            }
            else
            {
                starImage.enabled = false; // 별 이미지를 비활성화
            }
        }
    }

    private void OnClickBackButton(PointerEventData eventData)
    {
        Managers.UI.ClosePopupUI();
    }

    private void OnClickLevel15Button(PointerEventData eventData)
    {
        if (Managers.Stage.IsStageUnlocked(15))
        {
            if (Managers.Game.RunningPlayCount > 0)
            {
                Managers.Sound.Play("switch10", Sound.Effect);
                Managers.Game.RunningPlayCount--; // 러닝 플레이권 차감
                Managers.Scene.LoadScene(Scene.StoryScene);
                PlayerPrefs.SetInt("StageNumber", 15);
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

    private void OnClickLevel16Button(PointerEventData eventData)
    {
        if (Managers.Stage.IsStageUnlocked(16))
        {
            if (Managers.Game.RunningPlayCount > 0)
            {
                Managers.Sound.Play("switch10", Sound.Effect);
                Managers.Game.RunningPlayCount--; // 러닝 플레이권 차감
                LoadingManager.LoadScene("RunningScene");
                PlayerPrefs.SetInt("StageNumber", 16);
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

    private void OnClickLevel17Button(PointerEventData eventData)
    {
        if (Managers.Stage.IsStageUnlocked(17))
        {
            if (Managers.Game.RunningPlayCount > 0)
            {
                Managers.Sound.Play("switch10", Sound.Effect);
                Managers.Game.RunningPlayCount--; // 러닝 플레이권 차감
                LoadingManager.LoadScene("RunningScene");
                PlayerPrefs.SetInt("StageNumber", 17);
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
    private void OnClickLevel18Button(PointerEventData eventData)
    {
        if (Managers.Stage.IsStageUnlocked(18))
        {
            if (Managers.Game.RunningPlayCount > 0)
            {
                Managers.Sound.Play("switch10", Sound.Effect);
                Managers.Game.RunningPlayCount--; // 러닝 플레이권 차감
                Managers.Scene.LoadScene(Scene.StoryScene);
                PlayerPrefs.SetInt("StageNumber", 18);
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
    private void OnClickLevel19Button(PointerEventData eventData)
    {
        if (Managers.Stage.IsStageUnlocked(19))
        {
            if (Managers.Game.RunningPlayCount > 0)
            {
                Managers.Sound.Play("switch10", Sound.Effect);
                Managers.Game.RunningPlayCount--; // 러닝 플레이권 차감
                LoadingManager.LoadScene("RunningScene");
                PlayerPrefs.SetInt("StageNumber", 19);
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
    private void OnClickLevel20Button(PointerEventData eventData)
    {
        if (Managers.Stage.IsStageUnlocked(20))
        {
            if (Managers.Game.RunningPlayCount > 0)
            {
                Managers.Sound.Play("switch10", Sound.Effect);
                Managers.Game.RunningPlayCount--; // 러닝 플레이권 차감
                LoadingManager.LoadScene("RunningScene");
                PlayerPrefs.SetInt("StageNumber", 20);
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
    private void OnClickLevel21Button(PointerEventData eventData)
    {
        if (Managers.Stage.IsStageUnlocked(21))
        {
            if (Managers.Game.RunningPlayCount > 0)
            {
                Managers.Sound.Play("switch10", Sound.Effect);
                Managers.Game.RunningPlayCount--; // 러닝 플레이권 차감
                Managers.Scene.LoadScene(Scene.StoryScene);
                PlayerPrefs.SetInt("StageNumber", 21);
                PlayerPrefs.SetInt("StartFrom", 6);
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
