using System;
using TMPro;
using UnityEngine;

public class UI_Resouce : UI_Popup
{
    enum Texts
    {
        CoinTxt,
        RubyTxt,
        ActiveAbilityNumberTxt,
        AddAbilityTimeTxt
    }

    private void Awake()
    {
        Init();
		Managers.Game.OnRunningPlayCountChanged += UpdateRunningPlayCountUI;
		TimeManager.OnStaminaTimeChanged += UpdateStaminaTimeUI;
	}

    public override bool Init()
    {
        if (base.Init() == false)
        {
            return false;
        }

        Bind<TMP_Text>(typeof(Texts));

        return true;
    }

    private void Start()
    {
        ShowYouTheMoney();
        ShowActiveAbility();
    }

	private void OnDestroy()
	{
		Managers.Game.OnRunningPlayCountChanged -= UpdateRunningPlayCountUI;
		TimeManager.OnStaminaTimeChanged -= UpdateStaminaTimeUI;
	}

	private void ShowYouTheMoney()
    {
        TMP_Text coinTxt = GetText((int)Texts.CoinTxt);
        TMP_Text rubyTxt = GetText((int)Texts.RubyTxt);
        int max = 99999; // 최대 표기
        string zero = "0";
        string maxText = "+";

        int coin = Managers.Game.Gold;
        coinTxt.text = coin.ToString("#,###");
        if (coin > 99999)
        {
            coinTxt.text = max.ToString("#,###") + maxText;
        }
        else if (coin == 0)
        {
            coinTxt.text = zero;
        }

        int ruby = Managers.Game.Diamond;
        rubyTxt.text = ruby.ToString("#,###");

        if (ruby > 99999)
        {
            rubyTxt.text = max.ToString("#,###") + maxText;
        }
        else if (ruby == 0)
        {
            rubyTxt.text = zero;
        }
    }

    private void ShowActiveAbility()
    {
        TMP_Text activeAbilityNumberTxt = Get<TMP_Text>((int)Texts.ActiveAbilityNumberTxt);
        string x = " X ";
        int ability = Managers.Game.RunningPlayCount;

        activeAbilityNumberTxt.text = x + ability.ToString();
    }

	private void UpdateRunningPlayCountUI(int newCount)
	{
		TMP_Text activeAbilityNumberTxt = Get<TMP_Text>((int)Texts.ActiveAbilityNumberTxt);
		string x = " X ";
		activeAbilityNumberTxt.text = x + newCount.ToString();
	}

	private void UpdateStaminaTimeUI(float staminaTime)
	{
		TMP_Text addAbilityTimeTxt = GetText((int)Texts.AddAbilityTimeTxt);
		TimeSpan time = TimeSpan.FromSeconds(staminaTime);
		addAbilityTimeTxt.text = string.Format("{0:D2}:{1:D2}", time.Minutes, time.Seconds);
	}
}
