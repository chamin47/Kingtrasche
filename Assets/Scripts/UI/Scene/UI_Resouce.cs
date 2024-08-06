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

    private void Update()
    {

    }

    private void ShowYouTheMoney()
    {
        TMP_Text coinTxt = GetText((int)Texts.CoinTxt);
        TMP_Text rubyTxt = GetText((int)Texts.RubyTxt);
        int max = 99999; // 최대 표기
        string zero = "0";
        string maxText = "+";

        int coin = PlayerPrefs.GetInt("Gold");
        coinTxt.text = coin.ToString("#,###");
        if (coin > 99999)
        {
            coinTxt.text = max.ToString("#,###") + maxText;
        }
        else if (coin == 0)
        {
            coinTxt.text = zero;
        }

        int ruby = PlayerPrefs.GetInt("Diamond");
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
        int ability = PlayerPrefs.GetInt("RunningPlayCount");

        activeAbilityNumberTxt.text = x + ability.ToString();
    }
}
