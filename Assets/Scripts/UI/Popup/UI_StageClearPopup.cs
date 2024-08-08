using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_StageClearPopup : UI_Popup
{
    #region enum
    enum GameObjects
    {
    }

    enum Buttons
    {
        NextStageButton,
        RetryButton,
        BackStageButton
    }

    enum Texts
    {
        StageClearText,
        DogText
    }

    enum Images
    {
        DogImage,
		StarImg
	}

    #endregion

    private int currentStage;
    private int StarPoint;

    private void Awake()
    {
		currentStage = PlayerPrefs.GetInt("StageNumber");
		Debug.Log($"currentStage : {currentStage}");
		StarPoint = ShowStarLevel(); // 별 1개는 1, 2개는 2, 3개는 3으로 숫자로 반환됨.
		Init();
    }

    private void Start()
    {
       
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;
        Bind<TMP_Text>(typeof(Texts));
        Bind<Button>(typeof(Buttons));
        Bind<Image>(typeof(Images));

        Get<Button>((int)Buttons.NextStageButton).gameObject.BindEvent(OnClickNextStageButton);
        Get<Button>((int)Buttons.RetryButton).gameObject.BindEvent(OnClickRetryButton);
        Get<Button>((int)Buttons.BackStageButton).gameObject.BindEvent(OnClickBackStageButton);


        switch (StarPoint)
        {
            case 0: 
                Get<Image>((int)Images.StarImg).sprite = Resources.Load<Sprite>("Sprites/Starzero");
                break;
            case 1:
				Get<Image>((int)Images.StarImg).sprite = Resources.Load<Sprite>("Sprites/Starone");
				break;
            case 2:
				Get<Image>((int)Images.StarImg).sprite = Resources.Load<Sprite>("Sprites/Startwo");
				break;
            case 3:
				Get<Image>((int)Images.StarImg).sprite = Resources.Load<Sprite>("Sprites/Starthree");
				break;
        }

        return true;
    }

    private void OnClickNextStageButton(PointerEventData eventData)
    {
		if (Managers.Game.RunningPlayCount <= 0)
		{
			Debug.Log("러닝 플레이권이 부족합니다.");
			return;
		}
		Managers.Game.RunningPlayCount--;
		Managers.Sound.Play("switch10", Sound.Effect);
        UI_GameScene.currentScore = 0;

        if (currentStage == 6)
        {
            Managers.Scene.LoadScene(Scene.StoryScene);
			PlayerPrefs.SetInt("StartFrom", 4);
			PlayerPrefs.SetInt("StageNumber", ++currentStage);
        }
        else if (currentStage == 13)
        {
            Managers.Scene.LoadScene(Scene.StoryScene);
			PlayerPrefs.SetInt("StartFrom", 5);
			PlayerPrefs.SetInt("StageNumber", ++currentStage);
        }
        else if (currentStage == 20)
        {
            Managers.Scene.LoadScene(Scene.StoryScene);
			PlayerPrefs.SetInt("StartFrom", 6);
			PlayerPrefs.SetInt("StageNumber", ++currentStage);
        }
        else
        {
            PlayerPrefs.SetInt("StageNumber", ++currentStage);
			PlayerPrefs.SetInt("StartFrom", 3);
			Managers.Scene.LoadScene(Scene.StoryScene);
        }
    }

    private int ShowStarLevel()
    {
        return RunningMapManager.Instance.CheckStarLevel();
    }
}
