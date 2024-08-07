using UnityEngine;
using UnityEngine.SceneManagement;

public class EventObstacle : MonoBehaviour
{
    private GameObject player;
    private PlayerController playerController;

    private GameObject EventObj;
    private string puzzlePath = "Puzzle/Board";

    private float tempSpeed;
    private float eventDistance = 7f;

    private void Start()
    {
        //player = GameObject.FindWithTag("Player");
        player = PlayerManager.playerManager.GetPlayer();
        playerController = player.GetComponent<PlayerController>();
        tempSpeed = playerController.moveSpeed;
    }

    void Update()
    {
        float distance = Vector3.Distance(this.transform.position, player.transform.position);
        if (distance <= eventDistance && EventObj == null)
        {
            StartEvent();
        }
    }

    private void StartEvent()
    {
        playerController.moveSpeed = 0f;
        playerController.isPuzzlOn = true;
        EventObj = Managers.Resource.Load<GameObject>(puzzlePath);

        if (SceneManager.GetActiveScene().name == "RunningTutorialScene")
        {
            RunningTutorialManager.Instance.secondTutorial.SetActive(false);
            RunningTutorialManager.Instance.thirdTutorial.SetActive(true);
            Invoke("InstantiateGO", 2f);
        }
        else
        {
            InstantiateGO();
        }
    }

    private void InstantiateGO()
    {
        if (SceneManager.GetActiveScene().name == "RunningTutorialScene")
        {
            RunningTutorialManager.Instance.thirdTutorial.SetActive(false);
        }
        Instantiate(EventObj);
        CardGameManager.Instance.OnEndEvent += EndEvent;
    }

    private void EndEvent()
    {
        Managers.Sound.Play("harp strum 1", Sound.Effect);
        playerController.moveSpeed = tempSpeed;
        playerController.isPuzzlOn = false;

        int puzzleLevel = PlayerPrefs.GetInt("PuzzleLevel");
        puzzleLevel += 1;
        PlayerPrefs.SetInt("PuzzleLevel", puzzleLevel);
        PlayerPrefs.Save();

        Destroy(this.gameObject);
    }
}
