using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RunningTutorialManager : UI_Popup
{
    public static RunningTutorialManager Instance;

    private GameObject player;
    private PlayerController playerController;
    public EventObstacle eventObstacle;
    public HoneyBeeEvent honeyBeeEvent;

    public GameObject firstTutorial;
    public GameObject secondTutorial;
    public GameObject thirdTutorial;
    public GameObject fourthTutorial;

    public GameObject Tree;

    private int JumpCount = 0;
    private float distanceFromTree;
    private float eventDistane = 7f;
    private float tempSpeed;
    private int stopCount = 1;

    private Vector3 respawnPosition;
    private float backDistance = -8f; // 뒤쪽으로 5만큼 이동

    private bool isFirstComplete = false;
    public bool isSecondComplete = false;
    public bool isThirdComplete = false;
    public bool isFourthComplete = false;

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

    private void Start()
    {
        player = PlayerManager.playerManager.GetPlayer();
        playerController = player.GetComponent<PlayerController>();
        tempSpeed = playerController.moveSpeed;
    }

    private void Update()
    {
        RespawnPosition();
        distanceFromTree = Vector3.Distance(Tree.transform.position, player.transform.position);
        if (distanceFromTree <= eventDistane && stopCount == 1)
        {
            SecondTutorialStart();
        }
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
    }

    private void CloseFirstTutorial()
    {
        firstTutorial.SetActive(false);
        isFirstComplete = true;
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
        playerController.moveSpeed = 0f;
        stopCount--;

        secondTutorial.SetActive(true);
        Invoke("CloseFirstDescription", 2f);
    }

    private void CloseFirstDescription()
    {
        TMP_Text firstDescription = GetText((int)Texts.DescriptionText);
        TMP_Text secondDescription = GetText((int)Texts.DoubleJumpText);

        firstDescription.gameObject.SetActive(false);
        secondDescription.gameObject.SetActive(true);
        playerController.moveSpeed = tempSpeed;
    }

    public void OnPlayerDead()
    {
        RespawnPlayer();
    }

    private void RespawnPosition()
    {
        respawnPosition = player.transform.position;
        respawnPosition.x += backDistance;
    }

    private void RespawnPlayer()
    {

        if (isFirstComplete == false)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else if (isSecondComplete == false)
        {
            Managers.Player.SpawnPlayer();
            player.transform.position = respawnPosition;
            player.gameObject.SetActive(true);
            playerController.isDead = false;
            SecondTutorialStart();
        }
        else if (isThirdComplete == false)
        {
            player.gameObject.SetActive(true);
            playerController.moveSpeed = tempSpeed;
            eventObstacle.StartEvent();
        }
        else
        {
            playerController.moveSpeed = tempSpeed;
            honeyBeeEvent.SpawnBee();
        }
    }
}
