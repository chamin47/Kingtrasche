using System;
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
        BossImg,
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

	private Image bossImg;

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

		bossImg = Get<Image>((int)Images.BossImg);

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

    private string ConvertNumberToWord(int number)
    {
        switch (number)
        {
            case 8: return "One";
            case 9: return "Two";
            case 10: return "Three";
            case 11: return "Four";
            case 12: return "Five";
            case 13: return "Six";
            case 14: return "Seven";
            // �ʿ��� ��ŭ �߰�
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
            buttonText.enabled = true; // �ؽ�Ʈ�� Ȱ��ȭ�մϴ�.
			bossImg.enabled = true;
			int starCount = Managers.Stage.GetStarCount(stageNumber);
            SetStarImages(stageNumber, starCount, true);
        }
        else
        {
            // ��� ������ �̹����� �����մϴ�.
            buttonImage.sprite = Resources.Load<Sprite>("Sprites/LockStageImg");
            buttonText.enabled = false; // �ؽ�Ʈ�� ��Ȱ��ȭ�մϴ�.
			bossImg.enabled = false;
			SetStarImages(stageNumber, 0, false); // ��� ���������� �� �̹����� ��Ȱ��ȭ�մϴ�.
        }
    }

	private void SetStarImages(int stageNumber, int starCount, bool isUnlocked)
	{
		string stageWord = ConvertNumberToWord(stageNumber);
		for (int i = 1; i <= 3; i++)
		{
			string enumName = $"{stageWord}Star{i}";
			if (Enum.TryParse(typeof(Images), enumName, out var result))
			{
				Image starImage = Get<Image>((int)result);
				if (starImage != null)
				{
					starImage.enabled = isUnlocked && i <= starCount;
				}
			}
			else
			{
				Debug.LogWarning($"Enum value '{enumName}' not found in Images enum. Skipping.");
			}
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
                Managers.Game.RunningPlayCount--; // ���� �÷��̱� ����
                Managers.Scene.LoadScene(Scene.StoryScene);
                PlayerPrefs.SetInt("StageNumber", 8);
                PlayerPrefs.SetInt("StartFrom", 3);
            }
            else
            {
                Debug.Log("���� �÷��̱��� �����մϴ�.");
            }
        }
        else
        {
            Debug.Log("���������� ��� �ֽ��ϴ�.");
        }

    }
    private void OnClickLevel9Button(PointerEventData eventData)
    {
        if (Managers.Stage.IsStageUnlocked(9))
        {
            if (Managers.Game.RunningPlayCount > 0)
            {
                Managers.Sound.Play("switch10", Sound.Effect);
                Managers.Game.RunningPlayCount--; // ���� �÷��̱� ����
                LoadingManager.LoadScene("RunningScene");
                PlayerPrefs.SetInt("StageNumber", 9);
            }
            else
            {
                Debug.Log("���� �÷��̱��� �����մϴ�.");
            }
        }
        else
        {
            Debug.Log("���������� ��� �ֽ��ϴ�.");
        }

    }
    private void OnClickLevel10Button(PointerEventData eventData)
    {
        if (Managers.Stage.IsStageUnlocked(10))
        {
            if (Managers.Game.RunningPlayCount > 0)
            {
                Managers.Sound.Play("switch10", Sound.Effect);
                Managers.Game.RunningPlayCount--; // ���� �÷��̱� ����
                LoadingManager.LoadScene("RunningScene");
                PlayerPrefs.SetInt("StageNumber", 10);
            }
            else
            {
                Debug.Log("���� �÷��̱��� �����մϴ�.");
            }
        }
        else
        {
            Debug.Log("���������� ��� �ֽ��ϴ�.");
        }

    }
    private void OnClickLevel11Button(PointerEventData eventData)
    {
        if (Managers.Stage.IsStageUnlocked(11))
        {
            if (Managers.Game.RunningPlayCount > 0)
            {
                Managers.Sound.Play("switch10", Sound.Effect);
                Managers.Game.RunningPlayCount--; // ���� �÷��̱� ����
                Managers.Scene.LoadScene(Scene.StoryScene);
                PlayerPrefs.SetInt("StageNumber", 11);
                PlayerPrefs.SetInt("StartFrom", 3);
            }
            else
            {
                Debug.Log("���� �÷��̱��� �����մϴ�.");
            }
        }
        else
        {
            Debug.Log("���������� ��� �ֽ��ϴ�.");
        }

    }
    private void OnClickLevel12Button(PointerEventData eventData)
    {
        if (Managers.Stage.IsStageUnlocked(12))
        {
            if (Managers.Game.RunningPlayCount > 0)
            {
                Managers.Sound.Play("switch10", Sound.Effect);
                Managers.Game.RunningPlayCount--; // ���� �÷��̱� ����
                LoadingManager.LoadScene("RunningScene");
                PlayerPrefs.SetInt("StageNumber", 12);
            }
            else
            {
                Debug.Log("���� �÷��̱��� �����մϴ�.");
            }
        }
        else
        {
            Debug.Log("���������� ��� �ֽ��ϴ�.");
        }

    }
    private void OnClickLevel13Button(PointerEventData eventData)
    {
        if (Managers.Stage.IsStageUnlocked(13))
        {
            if (Managers.Game.RunningPlayCount > 0)
            {
                Managers.Sound.Play("switch10", Sound.Effect);
                Managers.Game.RunningPlayCount--; // ���� �÷��̱� ����
                LoadingManager.LoadScene("RunningScene");
                PlayerPrefs.SetInt("StageNumber", 13);
            }
            else
            {
                Debug.Log("���� �÷��̱��� �����մϴ�.");
            }
        }
        else
        {
            Debug.Log("���������� ��� �ֽ��ϴ�.");
        }

    }
    private void OnClickLevel14Button(PointerEventData eventData)
    {
        if (Managers.Stage.IsStageUnlocked(14))
        {
            if (Managers.Game.RunningPlayCount > 0)
            {
                Managers.Sound.Play("switch10", Sound.Effect);
                Managers.Game.RunningPlayCount--; // ���� �÷��̱� ����
                Managers.Scene.LoadScene(Scene.StoryScene);
                PlayerPrefs.SetInt("StageNumber", 14);
                PlayerPrefs.SetInt("StartFrom", 5);
            }
            else
            {
                Debug.Log("���� �÷��̱��� �����մϴ�.");
            }
        }
        else
        {
            Debug.Log("���������� ��� �ֽ��ϴ�.");
        }
    }
}
