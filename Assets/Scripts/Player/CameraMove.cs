using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private GameObject Player;
    public Transform[] childGameObject;
    private int stageNumber;

    void Start()
    {
        childGameObject = GetComponentsInChildren<Transform>();
        Player = GameObject.FindWithTag("Player");
        stageNumber = Managers.Map.currentStage;

        ChangeBackground();
    }

    void Update()
    {
        if (Player != null)
        {
            transform.position = new Vector3(Player.transform.position.x + 6f, 0, -10f);
        }
    }

    private void ChangeBackground()
    {
        Transform morning = childGameObject[2];
        Transform sunset = childGameObject[10];
        Transform night = childGameObject[20];
        if (stageNumber == 1 || stageNumber == 2 || stageNumber == 3 || stageNumber == 4)
        {
            sunset.gameObject.SetActive(false);
            night.gameObject.SetActive(false);

        }
        else if (stageNumber == 6 || stageNumber == 7 || stageNumber == 8 || stageNumber == 9)
        {
            morning.gameObject.SetActive(false);
            night.gameObject.SetActive(false);

        }
        else if (stageNumber == 11 || stageNumber == 12 || stageNumber == 13 || stageNumber == 14)
        {
            morning.gameObject.SetActive(false);
            sunset.gameObject.SetActive(false);

        }
        else
        {
            sunset.gameObject.SetActive(false);
            night.gameObject.SetActive(false);
        }
    }
}

