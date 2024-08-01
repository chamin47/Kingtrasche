using TMPro;
using UnityEngine;

public class UI_Resouce : UI_Popup
{
    enum Texts
    {
        CoinTxt,
        RubyTxt
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
    }

    private void ShowYouTheMoney()
    {
        TMP_Text coinTxt = GetText((int)Texts.CoinTxt);
        TMP_Text rubyTxt = GetText((int)Texts.RubyTxt);
        int max = 999999;
        string zero = "0";
        string maxText = "+";

        int coin = PlayerPrefs.GetInt("Gold");
        coinTxt.text = coin.ToString("#,###");
        if (coin > 999999)
        {
            coinTxt.text = max.ToString("#,###") + maxText;
        }
        else if (coin == 0)
        {
            coinTxt.text = zero;
        }

        int ruby = PlayerPrefs.GetInt("Diamond");
        rubyTxt.text = ruby.ToString("#,###");

        if (ruby > 999999)
        {
            rubyTxt.text = max.ToString("#,###") + maxText;
        }
        else if (ruby == 0)
        {
            rubyTxt.text = zero;
        }
    }




}
