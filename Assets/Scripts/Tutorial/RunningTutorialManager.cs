using TMPro;
using UnityEngine;

public class RunningTutorialManager : UI_Base
{
    public GameObject firstTutorial;
    public GameObject secondTutorial;
    public GameObject Player;

    private int JumpCount = 0;

    enum Texts
    {
        JumpText,
        DescriptionText,
        DoubleJumpText
    }

    enum Images
    {
        FirstJump,
        SecondJump,
        thirdJump
    }

    private void Awake()
    {
        Init();
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;
        Bind<TMP_Text>(typeof(Texts));

        return true;
    }

    void Start()
    {
        firstTutorial.SetActive(true);
    }

    void Update()
    {

    }


}
