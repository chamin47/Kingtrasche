using UnityEngine;

public class HoneyBeeEvent : MonoBehaviour
{
    public GameObject player;
    private PlayerController playerController;
    private Transform[] childTransform;
    private GameObject honeyBee;

    private float tempSpeed;
    private float eventDistance = 7f;
    private int randomBeeNumbers;
    private string beePath = "Puzzle/HoneyBee";


    void Start()
    {
        childTransform = GetComponentsInChildren<Transform>();
        playerController = player.GetComponent<PlayerController>();
        tempSpeed = playerController.moveSpeed;
    }


    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance <= eventDistance && honeyBee == null)
        {
            SpawnBee();
        }
    }

    private void SpawnBee()
    {
        playerController.moveSpeed = 0f;
        honeyBee = Managers.Resource.Load<GameObject>(beePath);

        randomBeeNumbers = Random.Range(5, 10);
        for (int i = 0; i < randomBeeNumbers; i++)
        {
            Instantiate(honeyBee, childTransform[3]);
            Debug.Log($"{i}¹øÂ° ¹ú »ý¼º");
        }
    }

    private void EndEvent()
    {
        Destroy(this.gameObject);
        playerController.moveSpeed = tempSpeed;
    }
}
