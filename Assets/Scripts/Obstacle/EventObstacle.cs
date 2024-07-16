using UnityEngine;

public class EventObstacle : MonoBehaviour
{
    private GameObject Event;
    private string puzzlePath = "Puzzle/Board";

    public GameObject player;
    private PlayerController playerController;

    private float tempSpeed;
    private float eventDistance = 2f;
    Vector3 spawnPuzzlePosition;

    private void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        tempSpeed = playerController.moveSpeed;
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance <= tempSpeed && Event == null)
        {
            StarttPuzzle();
        }
    }

    private void StarttPuzzle()
    {
        playerController.moveSpeed = 0f;
        spawnPuzzlePosition = new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2);
        Event = Managers.Resource.Load<GameObject>(puzzlePath);
        Instantiate(Event, spawnPuzzlePosition, Quaternion.identity);
    }

    private void EndPuzzle()
    {
        playerController.moveSpeed = tempSpeed;
    }




}
