using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_BeeQuizPopup : UI_Popup
{
    enum Buttons
    {
        SecletButton01,
        SecletButton02
    }

    enum Texts
    {
        SecletButton01Text,
        SecletButton02Text
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

        GetButton((int)Buttons.SecletButton01).gameObject.BindEvent(OnClickSelcctButton);
        GetButton((int)Buttons.SecletButton02).gameObject.BindEvent(OnClickSelcctButton);


        return true;
    }

    public void OnClickSelcctButton(PointerEventData eventData)
    {
        Debug.Log("ÆË¾÷ ´ÝÈû");
        OnEndEvent.Invoke();
        Debug.Log("ÄÝ¹éÈ£Ãâ");
        Destroy(this.gameObject);
    }
}
