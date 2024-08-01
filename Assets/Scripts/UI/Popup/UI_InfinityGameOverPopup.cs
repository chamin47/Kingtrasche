using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_InfinityGameOverPopup : UI_Popup
{
    enum Texts
    {
        BestScoreText,
        CurrentScoreText
    }

    enum Buttons
    {
        RetryButton,
        BackStageButton
    }

    TMP_Text SetBestScore;

    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        SetBestScore = GetText((int)Texts.BestScoreText);
        TakeAndSetScore();
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        Bind<TMP_Text>(typeof(Texts));
        Bind<Button>(typeof(Buttons));

        Get<Button>((int)Buttons.RetryButton).gameObject.BindEvent(OnClickRetryButton);
        Get<Button>((int)Buttons.BackStageButton).gameObject.BindEvent(OnClickBackStageButton);

        return true;
    }

    private void TakeAndSetScore()
    {
        TMP_Text TakeCurrentScore = GetText((int)Texts.CurrentScoreText);

        int _currentScore = UI_GameScene.currentScore;
        int _bestScore = PlayerPrefs.GetInt("BestScore");

        TakeCurrentScore.text = _currentScore.ToString();

        if (_bestScore != 0)
        {
            if (_bestScore < _currentScore)
            {
                _bestScore = _currentScore;
                SetBestScore.text = _currentScore.ToString();
                PlayerPrefs.SetInt("BestScore", _bestScore);
            }
            else
            {
                _bestScore = PlayerPrefs.GetInt("BestScore");
                SetBestScore.text = _bestScore.ToString();
            }
        }
        else
        {
            _bestScore = _currentScore;
            SetBestScore.text = _currentScore.ToString();
            PlayerPrefs.SetInt("BestScore", _bestScore);
        }
    }
}
