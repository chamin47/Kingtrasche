using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_BeeQuizPopup : UI_Popup
{
    enum Buttons
    {
        SelectButton01,
        SelectButton02
    }

    enum Texts
    {
        SelectButton01Text,
        SelectButton02Text,
        TimeText
    }

    public Action OnEndEvent;
    bool isCorrect;
    TMP_Text timeText;

    private float time = 3f;
    private float timeoutTimer = 0;

    private void Awake()
    {
        Init();
        timeText = GetText((int)Texts.TimeText);
    }

    private void Update()
    {
        time -= Time.deltaTime;

        timeText.text = time.ToString("N2");
        TimeOut();
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;
        Bind<TMP_Text>(typeof(Texts));
        Bind<Button>(typeof(Buttons));

        return true;
    }

    public void CorrectButton(PointerEventData eventData)
    {
        Managers.Sound.Play("switch10", Sound.Effect);
        OnEndEvent?.Invoke();
        Destroy(this.gameObject);
    }

    private void IncorrectButton(PointerEventData eventData)
    {
        Managers.Sound.Play("switch10", Sound.Effect);
        if (SceneManager.GetActiveScene().name == "RunningTutorialScene")
        {
            RunningTutorialManager.Instance.OnPlayerDead();
            Destroy(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
            Managers.Game.GameOver();
        }
    }

    public void SetCorrectOrIncorrectBeeNumber(int correctNumber)
    {
        TMP_Text text01 = GetText((int)Texts.SelectButton01Text);
        TMP_Text text02 = GetText((int)Texts.SelectButton02Text);
        int incorrectNumber;
        string mari = " ¸¶¸®";

        incorrectNumber = UnityEngine.Random.Range(5, 10);

        if (incorrectNumber == correctNumber)
        {
            while (correctNumber == incorrectNumber)
            {
                incorrectNumber = UnityEngine.Random.Range(5, 10);
            }
        }

        int temp = UnityEngine.Random.Range(1, 11);
        if (temp <= 5)
        {
            text01.text = correctNumber.ToString() + mari;
            text02.text = incorrectNumber.ToString() + mari;
            isCorrect = true;
        }
        else
        {
            text01.text = incorrectNumber.ToString() + mari;
            text02.text = correctNumber.ToString() + mari;
            isCorrect = false;
        }

        SetCorrectOrIncorrectButton();
    }

    private void SetCorrectOrIncorrectButton()
    {
        if (isCorrect == true)
        {
            GetButton((int)Buttons.SelectButton01).gameObject.BindEvent(CorrectButton);
            GetButton((int)Buttons.SelectButton02).gameObject.BindEvent(IncorrectButton);
        }
        else if (isCorrect == false)
        {
            GetButton((int)Buttons.SelectButton01).gameObject.BindEvent(IncorrectButton);
            GetButton((int)Buttons.SelectButton02).gameObject.BindEvent(CorrectButton);
        }
    }

    private void TimeOut()
    {
        if (time <= timeoutTimer)
        {
            if (SceneManager.GetActiveScene().name == "RunningTutorialScene")
            {
                RunningTutorialManager.Instance.OnPlayerDead();
                Destroy(gameObject);
            }
            else
            {
                Managers.Game.GameOver();
                Destroy(gameObject);
            }
        }
    }
}
