using UnityEngine;

public class HoneyBeeEvent : MonoBehaviour
{
    private GameObject Player;
    private Transform[] childTransform;
    private GameObject honeyBee;

    private int randomBeeNumbers;
    private string beePath = "Puzzle/HoneyBee";


    void Start()
    {
        childTransform = GetComponentsInChildren<Transform>();
        SpawnBee();
    }


    void Update()
    {

    }

    private void SpawnBee()
    {
        honeyBee = Managers.Resource.Load<GameObject>(beePath);

        randomBeeNumbers = Random.Range(5, 10);
        for (int i = 0; i < randomBeeNumbers; i++)
        {
            Instantiate(honeyBee, childTransform[3]);
            Debug.Log($"{i}¹ø ¹ú »ý¼º");
        }
    }
}
