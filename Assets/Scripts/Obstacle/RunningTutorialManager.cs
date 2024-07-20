using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RunningTutorialManager : UI_Popup
{
    public GameObject firstTutorial;
    public GameObject secondTutorial;
    public GameObject Player;
    public Image firstJump;
    public Image secondJump;
    public Image thirdJump;

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
    }

    void Update()
    {

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
            ChangeAlphaColor(firstJump);
        }
        else if (JumpCount == 2)
        {
            ChangeAlphaColor(secondJump);
        }
        else if (JumpCount == 3)
        {
            ChangeAlphaColor(thirdJump);
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
