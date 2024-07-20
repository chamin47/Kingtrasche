using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
	public int Gold { get; set; }               // 무료재화
	public int Diamond { get; set; }            // 유료재화
	public int RunningPlayCount { get; set; }   // 러닝플레이권
	public int NaturalRunningPlayCount { get; set; } // 자연 충전된 러닝플레이권
	public int MaxRunningPlayCount { get; set; } = 10; // 최대 러닝플레이권

	private void Init()
	{
		LoadGame();
	}

	private void SaveGame()
	{
		PlayerPrefs.SetInt("Gold", Gold);
		PlayerPrefs.SetInt("Diamond", Diamond);
		PlayerPrefs.SetInt("RunningPlayCount", RunningPlayCount);
		PlayerPrefs.SetInt("NaturalRunningPlayCount", NaturalRunningPlayCount);
		PlayerPrefs.SetInt("MaxRunningPlayCount", MaxRunningPlayCount);
		PlayerPrefs.Save();
	}

	private void LoadGame()
	{
		Gold = PlayerPrefs.GetInt("Gold", 0);
		Diamond = PlayerPrefs.GetInt("Diamond", 0);
		RunningPlayCount = PlayerPrefs.GetInt("RunningPlayCount", 0);
		NaturalRunningPlayCount = PlayerPrefs.GetInt("NaturalRunningPlayCount", 0);
		MaxRunningPlayCount = PlayerPrefs.GetInt("MaxRunningPlayCount", 10);
	}

	public void GameOver()
	{
		Managers.UI.ShowPopupUI<UI_GameOverPopup>();
		Time.timeScale = 0;
	}

	public void PurchaseRunningPlayCount(int amount)
	{
		if (Diamond >= amount)
		{
			Diamond -= amount;
			RunningPlayCount += amount;

			// 유료재화로 구매한 러닝플레이권은 최대치 제한 없음
			SaveGame();
		}
		else
		{
			Debug.Log("유료재화가 부족합니다.");
		}
	}
}
