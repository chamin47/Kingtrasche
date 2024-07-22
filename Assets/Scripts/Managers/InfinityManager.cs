using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class InfinityManager : MonoBehaviour
{
    public Transform Player;
    private GameObject[] mapChunk;
    private List<GameObject> activeChunkList = new List<GameObject>(); // 활성 청크 리스트
    private List<GameObject> deactiveChunkList = new List<GameObject>(); //비활성 청크 리스트

    private int activeChunkNumber; // 10개 미만이면 다시 배치
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

            activeChunkList.Add(go); // 활성청크 리스트에 추가 (처음 모든 맵이 들어감)
            activeChunkNumber++;
            lastChunkVector = chunkPosition; // 마지막 청크의 포지션으로 계속 업뎃
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

    private void GoThroughPlayer() // 플레이어가 지나가면 뒤에 있는 맵이 비활성화
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

    private void SetNewMap() //비활성화 된 청크만 다시 배치
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
