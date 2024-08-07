using System;
using System.Collections;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float StaminaRechargeInterval { get; set; } = 300f; // 5분
    public float StaminaTime { get; set; }
    public DateTime LastGeneratedStaminaTime { get; set; }

    public static event Action<float> OnStaminaTimeChanged;

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
        //Debug.Log($"TimeManager initialized. StaminaTime: {StaminaTime}, LastGeneratedStaminaTime: {LastGeneratedStaminaTime}");
    }

    public void CalcOfflineStamina()
    {
        TimeSpan span = DateTime.Now - LastGeneratedStaminaTime;
        int generatedStamina = (int)(span.TotalMinutes / (StaminaRechargeInterval / 60));
        RechargeStamina(generatedStamina);
        DateTime generatedTime = LastGeneratedStaminaTime.AddMinutes(generatedStamina * (StaminaRechargeInterval / 60));
        TimeSpan remainingTime = generatedTime - DateTime.Now;
        StaminaTime = (float)remainingTime.TotalSeconds;
        //Debug.Log($"Offline stamina calculated. Generated Stamina: {generatedStamina}, Remaining Time: {StaminaTime}");
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
                OnStaminaTimeChanged?.Invoke(StaminaTime);
                //Debug.Log($"StaminaTime decreased: {StaminaTime}");
            }
            if (StaminaTime <= 0)
            {
                RechargeStamina();
                StaminaTime = StaminaRechargeInterval;
                OnStaminaTimeChanged?.Invoke(StaminaTime);
                //Debug.Log("Stamina recharged.");
            }
        }
    }

    private void RechargeStamina(int count = 1)
    {
        Managers.Game.NaturalRunningPlayCount += count;

        // 자연 충전된 러닝플레이권이 최대치를 초과하지 않도록 합니다.
        if (Managers.Game.NaturalRunningPlayCount > Managers.Game.MaxRunningPlayCount)
        {
            Managers.Game.NaturalRunningPlayCount = Managers.Game.MaxRunningPlayCount;
        }

        Managers.Game.RunningPlayCount = Math.Max(Managers.Game.RunningPlayCount, Managers.Game.NaturalRunningPlayCount);
        Debug.Log($"RunningPlayCount recharged. New count: {Managers.Game.RunningPlayCount}");

        LastGeneratedStaminaTime = DateTime.Now;
        StaminaTime = StaminaRechargeInterval;
        SaveState();
    }

    private void SaveState()
    {
        PlayerPrefs.SetString("LastGeneratedStaminaTime", LastGeneratedStaminaTime.ToString());
        PlayerPrefs.SetFloat("StaminaTime", StaminaTime);
        PlayerPrefs.SetInt("NaturalRunningPlayCount", Managers.Game.NaturalRunningPlayCount);
        PlayerPrefs.SetInt("RunningPlayCount", Managers.Game.RunningPlayCount);
        PlayerPrefs.Save();
        Debug.Log("State saved.");
    }

    // 애플리케이션 종료 시 호출되는 메서드
    private void OnApplicationQuit()
    {
        SaveState();
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            SaveState();
        }
    }
}
