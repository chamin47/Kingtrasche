using UnityEngine;
using UnityEngine.SceneManagement;

public class EventObstacle : MonoBehaviour
{
    private GameObject EventObj;
    private string puzzlePath = "Puzzle/Board";
    private string BeeHivePath = "";
    private string QuizPath = "";

    private GameObject player;
    private PlayerController playerController;

    private float tempSpeed;
    private float eventDistance = 7f;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
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
        Destroy(this.gameObject);
    }
}
