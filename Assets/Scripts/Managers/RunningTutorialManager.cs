using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RunningTutorialManager : UI_Popup
{
    public static RunningTutorialManager Instance;

    public GameObject Player;
    private PlayerController PlayerController;

    public GameObject firstTutorial;
    public GameObject secondTutorial;
    public GameObject Tree;

    private int JumpCount = 0;
    private float distanceFromTree;
    private float eventDistane = 7f;

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

        PlayerController = GetComponentInChildren<PlayerController>();
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        Bind<TMP_Text>(typeof(Texts));
        Bind<Image>(typeof(Images));

        return true;
    }

    private void Update()
    {
        distanceFromTree = Vector3.Distance(Tree.transform.position, Player.transform.position);
        Debug.Log(distanceFromTree);
    }

    public void IncreaseJumpCount()
    {
        JumpCount++;
        TutorialStart();
    }

    public void TutorialStart()
    {
        if (JumpCount == 1)
        {
            ChangeAlphaColor(GetImage((int)Images.FirstJump));
        }
        else if (JumpCount == 2)
        {
            ChangeAlphaColor(GetImage((int)Images.SecondJump));
        }
        else if (JumpCount >= 3)
        {
            ChangeAlphaColor(GetImage((int)Images.ThirdJump));
            Invoke("CloseFirstTutorial", 1f);
        }
        SecondTutorialStart();
    }

    private void CloseFirstTutorial()
    {
        firstTutorial.SetActive(false);
    }

    private void ChangeAlphaColor(Image image)
    {
        Color color = new Color();
        color = image.color;
        color.a = 255f / 255f;
        image.color = color;
    }

    private void SecondTutorialStart()
    {
        Debug.Log("½ÇÇà");
        if (distanceFromTree <= eventDistane)
        {
            secondTutorial.SetActive(true);
            PlayerController.moveSpeed = 0f;
        }
    }
}
