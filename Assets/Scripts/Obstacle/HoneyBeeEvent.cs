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
            Debug.Log($"{i}번째 벌 생성");
        }
    }

    private Vector3 GetRandomFlyPosition()
    {
        // BeeSpawnRange의 범위 계산
        Transform BeeSpawnRange = childTransform[2];
        RectTransform rangeTransform = BeeSpawnRange.GetComponent<RectTransform>();
        float halfwidth = rangeTransform.rect.width / 2;
        float halfHeight = rangeTransform.rect.height / 2;

        // 랜덤 백터값 계산
        Vector3 randomPosition = new Vector3(Random.Range(-halfwidth, halfwidth), Random.Range(-halfHeight, halfHeight), 0);

        return BeeSpawnRange.position + randomPosition;
    }

    private void EndEvent()
    {
        Destroy(this.gameObject);
        playerController.moveSpeed = tempSpeed;
    }
}
