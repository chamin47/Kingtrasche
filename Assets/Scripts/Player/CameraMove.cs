using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraMove : MonoBehaviour
{
    private GameObject Player;
    public Transform[] childGameObject;
    private int stageNumber;

    void Start()
    {
        childGameObject = GetComponentsInChildren<Transform>();
        Player = GameObject.FindWithTag("Player");
        if (SceneManager.GetActiveScene().name == "RunningScene")
        {
            stageNumber = RunningMapManager.Instance.currentStage;
        }
    }

    void Update()
    {
        if (Player != null)
        {
            transform.position = new Vector3(Player.transform.position.x + 6f, 0, -10f);
        }
    }
}

