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
        DogImage
    }

    #endregion

    private int currentStage;

    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        currentStage = PlayerPrefs.GetInt("StageNumber");
        Debug.Log($"currentStage : {currentStage}");
        ShowStarLevel();
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

        return true;
    }

    private void OnClickNextStageButton(PointerEventData eventData)
    {
        Managers.Sound.Play("switch10", Sound.Effect);
        UI_GameScene.currentScore = 0;

        if (currentStage == 4)
        {
            Managers.Scene.LoadScene(Scene.BossScene1);
            PlayerPrefs.SetInt("StageNumber", ++currentStage);
        }
        else if (currentStage == 9)
        {
            Managers.Scene.LoadScene(Scene.BossScene2);
            PlayerPrefs.SetInt("StageNumber", ++currentStage);
        }
        else if (currentStage == 14)
        {
            Managers.Scene.LoadScene(Scene.BossScene3);
            PlayerPrefs.SetInt("StageNumber", ++currentStage);
        }
        else
        {
            PlayerPrefs.SetInt("StageNumber", ++currentStage);
            Managers.Scene.LoadScene(Scene.RunningScene);
        }
    }

    private void ShowStarLevel()
    {
        RunningMapManager.Instance.CheckStarLevel();
    }
}
