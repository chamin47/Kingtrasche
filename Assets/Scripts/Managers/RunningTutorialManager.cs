using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RunningTutorialManager : UI_Popup
{
    public static RunningTutorialManager Instance;

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
        ThirdJump
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

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

    public void IncreaseJumpCount()
    {
        JumpCount++;
        Debug.Log(JumpCount);
        FirstTutorialStart();
    }

    public void FirstTutorialStart()
    {
        if (JumpCount == 1)
        {
            ChangeAlphaColor(GetImage((int)Images.FirstJump));
        }
        else if (JumpCount == 2)
        {
            ChangeAlphaColor(GetImage((int)Images.SecondJump));
        }
        else if (JumpCount == 3)
        {
            ChangeAlphaColor(GetImage((int)Images.ThirdJump));
        }
        else if (JumpCount >= 4)
        {
            firstTutorial.SetActive(false);
        }
    }

    private void ChangeAlphaColor(Image image)
    {
        Color color = new Color();
        color = image.color;
        color.a = 255f / 255f;
        image.color = color;
    }
}
