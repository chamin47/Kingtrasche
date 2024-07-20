using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
	public int Gold { get; set; }               // ������ȭ
	public int Diamond { get; set; }            // ������ȭ
	public int RunningPlayCount { get; set; }   // �����÷��̱�
	public int NaturalRunningPlayCount { get; set; } // �ڿ� ������ �����÷��̱�
	public int MaxRunningPlayCount { get; set; } = 10; // �ִ� �����÷��̱�

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

			// ������ȭ�� ������ �����÷��̱��� �ִ�ġ ���� ����
			SaveGame();
		}
		else
		{
			Debug.Log("������ȭ�� �����մϴ�.");
		}
	}
}
