using GameBalance;
using System;
using System.Collections.Generic;
using UnityEngine;

public class RunningMapManager : MonoBehaviour
{
    public static RunningMapManager Instance;

    MapChunkData chunkData;

    private float chunkSpace = 18f; // �� �� ����
    public int currentStage = 1;
    private Vector3 lastMapTransform;

    private List<GameObject> meats = new List<GameObject>(); //��� ����Ʈ
    private int totalMeats = 0; // �� ��� ����
    private int eatMeat = 0; //�÷��̾ ���� ��� ����

    public static event Action<Vector3> EndMapSpawn;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        currentStage = PlayerPrefs.GetInt("StageNumber");
    }

    void Start()
    {
        chunkData = MapChunkData.MapChunkDataMap[currentStage];
        List<string> prefabPaths = chunkData.MapChunksPath;
        GameObject[] mapChunk = new GameObject[prefabPaths.Count];
        for (int i = 0; i < prefabPaths.Count; i++) // ��ûũ ����
        {
            mapChunk[i] = Managers.Resource.Load<GameObject>(prefabPaths[i]);
        }

        for (int i = 0; i < mapChunk.Length; i++) // ��ûũ �� �����ǿ� ���� 
        {
            Vector3 chunkPosition = new Vector3(chunkSpace + i * chunkSpace, 0, 0);
            GameObject chunk = Instantiate(mapChunk[i], chunkPosition, Quaternion.identity);

            // meat �� �����ͼ� ����
            Meat[] meatPrefabs = chunk.GetComponentsInChildren<Meat>();
            foreach (Meat meat in meatPrefabs)
            {
                meats.Add(meat.gameObject);
            }

            if (i == mapChunk.Length - 1)
            {
                lastMapTransform = chunkPosition;
            }
        }
        totalMeats = meats.Count;
        EndMapSpawn?.Invoke(lastMapTransform);
    }

    public int CheckStarLevel() // �� ���� ��ȯ
    {
        eatMeat = UI_GameScene.currentScore;
        int oneStar = 1;
        int twoStar = 2;
        int threeStar = 3;

        float ratio = (float)eatMeat / totalMeats;

        if (ratio >= 0.9f)
        {
            Debug.Log("�� 3��!");
            return oneStar;
        }
        else if (ratio >= 0.5f)
        {
            Debug.Log("�� 2��!");
            return twoStar;
        }
        else
        {
            Debug.Log("�� 1��!");
            return threeStar;
        }
    }
}