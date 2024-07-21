using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardGameManager : MonoBehaviour
{
    public static CardGameManager Instance { get; private set; }

    public Card firstCard;
    public Card secondCard;

    public TextMeshProUGUI timeText;

    public int cardCount = 0;
	float time = 0.0f;
    public float TimeoutTimer = 20;

    public Action OnEndEvent;

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
        time += Time.deltaTime;
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
        if (time >= TimeoutTimer)
        {
            Managers.Game.GameOver();
            Destroy(gameObject);
        }
	}

    private void GameClear()
    {
        Destroy(gameObject);
        OnEndEvent.Invoke();
        Debug.LogError("Clear!");
    }
}
