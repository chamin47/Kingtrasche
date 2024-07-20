using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
	public float StaminaRechargeInterval { get; set; } = 600f; // 10��
	public float StaminaTime { get; set; }
	public DateTime LastGeneratedStaminaTime { get; set; }

	private void Awake()
	{
		Init();
	}

	public void Init()
	{
		StaminaTime = PlayerPrefs.GetFloat("StaminaTime", StaminaRechargeInterval);
		LastGeneratedStaminaTime = DateTime.Parse(PlayerPrefs.GetString("LastGeneratedStaminaTime", DateTime.Now.ToString()));
		CalcOfflineStamina();
		TimerStart();
	}

	public void CalcOfflineStamina()
	{
		TimeSpan span = DateTime.Now - LastGeneratedStaminaTime;
		int generatedStamina = (int)(span.TotalMinutes / (StaminaRechargeInterval / 60));
		RechargeStamina(generatedStamina);
		DateTime generatedTime = LastGeneratedStaminaTime.AddMinutes(generatedStamina * (StaminaRechargeInterval / 60));
		TimeSpan remainingTime = generatedTime - DateTime.Now;
		StaminaTime = (float)remainingTime.TotalSeconds;
	}

	public void TimerStart()
	{
		StartCoroutine(CoStartTimer());
	}

	private IEnumerator CoStartTimer()
	{
		while (true)
		{
			yield return new WaitForSeconds(1f);
			if (StaminaTime > 0)
			{
				StaminaTime--;
			}
			if (StaminaTime <= 0)
			{
				RechargeStamina();
				StaminaTime = StaminaRechargeInterval;
			}
		}
	}

	private void RechargeStamina(int count = 1)
	{
		if (Managers.Game.NaturalRunningPlayCount < Managers.Game.MaxRunningPlayCount)
		{
			Managers.Game.NaturalRunningPlayCount += count;

			// �ڿ� ������ �����÷��̱��� �ִ�ġ�� �ʰ����� �ʵ��� �մϴ�.
			if (Managers.Game.NaturalRunningPlayCount > Managers.Game.MaxRunningPlayCount)
			{
				Managers.Game.NaturalRunningPlayCount = Managers.Game.MaxRunningPlayCount;
			}

			Managers.Game.RunningPlayCount = Math.Max(Managers.Game.RunningPlayCount, Managers.Game.NaturalRunningPlayCount);
		}

		LastGeneratedStaminaTime = DateTime.Now;
		StaminaTime = StaminaRechargeInterval;
		SaveState();
	}

	private void SaveState()
	{
		PlayerPrefs.SetString("LastGeneratedStaminaTime", LastGeneratedStaminaTime.ToString());
		PlayerPrefs.SetFloat("StaminaTime", StaminaTime);
		PlayerPrefs.Save();
	}

	// ���ø����̼� ���� �� ȣ��Ǵ� �޼���
	private void OnApplicationQuit()
	{
		SaveState();
	}
}
