using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class InfinityManager : MonoBehaviour
{
    public Transform Player;
    private GameObject[] mapChunk;
    private List<GameObject> activeChunkList = new List<GameObject>(); // Ȱ�� ûũ ����Ʈ
    private List<GameObject> deactiveChunkList = new List<GameObject>(); //��Ȱ�� ûũ ����Ʈ

    private int activeChunkNumber; // 10�� �̸��̸� �ٽ� ��ġ
    private int chunkSpace = 18;
    private float deactiveDistance = -36f;
    private Vector3 lastChunkVector;

    private void Awake()
    {
        mapChunk = Resources.LoadAll<GameObject>("Prefabs/MapChunk/");
    }

    private void Start()
    {
        List<GameObject> mapChunkList = new List<GameObject>(mapChunk);
        Shuffle(mapChunkList);
        for (int i = 0; i < mapChunkList.Count; i++)
        {
            Vector3 chunkPosition = new Vector3(chunkSpace + i * chunkSpace, 0, 0);
            GameObject go = Instantiate(mapChunkList[i], chunkPosition, Quaternion.identity);
            go.SetActive(true);

            activeChunkList.Add(go); // Ȱ��ûũ ����Ʈ�� �߰� (ó�� ��� ���� ��)
            activeChunkNumber++;
            lastChunkVector = chunkPosition; // ������ ûũ�� ���������� ��� ����
        }
    }

    private void Update()
    {
        GoThroughPlayer();

        if (activeChunkNumber <= 10)
        {
            SetNewMap();
        }
    }

    private void GoThroughPlayer() // �÷��̾ �������� �ڿ� �ִ� ���� ��Ȱ��ȭ
    {
        for (int i = 0; i < activeChunkList.Count; i++)
        {
            GameObject go = activeChunkList[i];
            float gapX = go.transform.position.x - Player.position.x;
            if (gapX < deactiveDistance)
            {
                DeactiveChunk(go);
            }
        }
    }

    private void SetNewMap() //��Ȱ��ȭ �� ûũ�� �ٽ� ��ġ
    {
        Shuffle(deactiveChunkList);

        foreach (var chunk in deactiveChunkList)
        {
            Vector3 newChunkPosition = lastChunkVector + new Vector3(chunkSpace, 0, 0);
            chunk.transform.position = newChunkPosition;
            chunk.SetActive(true);

            activeChunkList.Add(chunk);
            activeChunkNumber++;
            lastChunkVector = newChunkPosition;
        }
        deactiveChunkList.Clear();
    }

    private void DeactiveChunk(GameObject chunk)
    {
        activeChunkList.Remove(chunk);
        chunk.SetActive(false);
        deactiveChunkList.Add(chunk);
        activeChunkNumber--;
    }

    private void Shuffle(List<GameObject> chunk)
    {
        for (int i = 0; i < chunk.Count; i++)
        {
            GameObject temp = chunk[i];
            int randomIndex = Random.Range(0, chunk.Count);
            chunk[i] = chunk[randomIndex];
            chunk[randomIndex] = temp;
        }
    }
}
