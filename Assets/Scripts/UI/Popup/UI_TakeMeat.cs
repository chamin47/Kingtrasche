using TMPro;

public class UI_TakeMeat : UI_Popup
{
    private int currentScore;

    enum Texts
    {
        CurrentMeatText
    }

    private void Awake()
    {
        Init();
    }

    void Update()
    {

    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        Bind<TMP_Text>(typeof(Texts));

        return true;
    }


}
