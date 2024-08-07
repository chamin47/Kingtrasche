using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CardGameManager : MonoBehaviour
{
    public static CardGameManager Instance { get; private set; }

    public Card firstCard;
    public Card secondCard;

    public TextMeshProUGUI timeText;

    public int cardCount = 0;
    float time = 10.0f;
    float TimeoutTimer = 0f;

    public Action OnEndEvent;
    public bool canClickCards = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        timeText.text = time.ToString("N2");
        TimeOut();
    }

    public void Matched()
    {
        if (firstCard.idx == secondCard.idx)
        {
            firstCard.DestroyCard();
            secondCard.DestroyCard();
            cardCount -= 2;
            CheckGameClear();
        }
        else
        {
            firstCard.CloseCard();
            secondCard.CloseCard();
        }

        firstCard = null;
        secondCard = null;
    }

    private void CheckGameClear()
    {
        // 모든 카드가 매치되었는지 확인
        if (cardCount == 1)
        {
            GameClear();
        }
    }

    private void TimeOut()
    {
        if (time <= TimeoutTimer)
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

    private void GameClear()
    {
        Destroy(gameObject);
        OnEndEvent.Invoke();
        Debug.LogError("Clear!");
    }
}
