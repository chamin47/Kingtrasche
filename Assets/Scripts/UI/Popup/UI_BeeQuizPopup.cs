using System;
using TMPro;
using UnityEngine.EventSystems;
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
        SelectButton02Text
    }

    public Action OnEndEvent;

    private void Awake()
    {
        Init();
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;
        Bind<TMP_Text>(typeof(Texts));
        Bind<Button>(typeof(Buttons));

        GetButton((int)Buttons.SelectButton01).gameObject.BindEvent(OnClickSelcctButton);
        GetButton((int)Buttons.SelectButton02).gameObject.BindEvent(OnClickSelcctButton);


        return true;
    }

    public void OnClickSelcctButton(PointerEventData eventData)
    {
        OnEndEvent?.Invoke();
        Destroy(this.gameObject);
    }

    public void SetCorrectOrIncorrectBeeNumber(int correctNumber)
    {
        TMP_Text text01 = GetText((int)Texts.SelectButton01Text);
        TMP_Text text02 = GetText((int)Texts.SelectButton02Text);
        string mari = " ¸¶¸®";

        int incorrectNumber;
        incorrectNumber = UnityEngine.Random.Range(5, 10);

        if (incorrectNumber == correctNumber)
        {
            while (correctNumber != incorrectNumber)
            {
                incorrectNumber = UnityEngine.Random.Range(5, 10);
            }
        }

        int temp = UnityEngine.Random.Range(1, 11);
        if (temp <= 5)
        {
            text01.text = correctNumber.ToString() + mari;
            text02.text = incorrectNumber.ToString() + mari;
        }
        else
        {
            text01.text = incorrectNumber.ToString() + mari;
            text02.text = correctNumber.ToString() + mari;
        }
    }
}
