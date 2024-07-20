using GameBalance;
using System.Collections.Generic;
using UnityEngine;

public class RunningMapManager : MonoBehaviour
{
    MapChunkData chunkData;

    private float chunkSpace = 18f; // ¸Ê °£ °£°Ý
    public int currentStage = 1;

    void Start()
    {
        currentStage = PlayerPrefs.GetInt("StageNumber");
        chunkData = MapChunkData.MapChunkDataMap[currentStage];
        List<string> prefabPaths = chunkData.MapChunksPath;
        GameObject[] mapChunk = new GameObject[prefabPaths.Count];
        for (int i = 0; i < prefabPaths.Count; i++)
        {
            mapChunk[i] = Managers.Resource.Load<GameObject>(prefabPaths[i]);
        }

        for (int i = 0; i < mapChunk.Length; i++)
        {
            Vector3 chunkPosition = new Vector3(chunkSpace + i * chunkSpace, 0, 0);
            Instantiate(mapChunk[i], chunkPosition, Quaternion.identity);
        }
    }
}

