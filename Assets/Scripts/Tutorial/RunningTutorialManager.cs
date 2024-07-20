using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
        Bind<Image>(typeof(Images));

        return true;
    }

    void Start()
    {
        firstTutorial.SetActive(true);
    }

    void Update()
    {

    }

    public void IncreaseJumpCount()
    {
        JumpCount++;
        if (JumpCount == 1)
        {

        }
        else if (JumpCount == 2)
        {

        }
        else if (JumpCount == 3)
        {

        }
        else if (JumpCount > 4)
        {
            firstTutorial.SetActive(false);
        }
    }
}
