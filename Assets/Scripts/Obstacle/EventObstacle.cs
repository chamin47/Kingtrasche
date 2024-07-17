using UnityEngine;

public class EventObstacle : MonoBehaviour
{
    private GameObject EventObj;
    private string puzzlePath = "Puzzle/Board";
    private string BeeHivePath = " ";
    private string QuizPath = " ";
    // [todo] ��ֹ��� ���� path���� -> Enum���� �����غ���

    public GameObject player;
    private PlayerController playerController;

    private float tempSpeed;
    private float eventDistance = 2f;

    private void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        tempSpeed = playerController.moveSpeed;
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance <= tempSpeed && EventObj == null)
        {
            StartEvent(puzzlePath);
        }
    }

    private void StartEvent(string path)
    {
        playerController.moveSpeed = 0f;
        EventObj = Managers.Resource.Load<GameObject>(path);
        Instantiate(EventObj);
    }

    private void EndEvent()
    {
        Destroy(this.gameObject);
        playerController.moveSpeed = tempSpeed;
    }
}
