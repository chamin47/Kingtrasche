using GameBalance;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RunningMapManager : MonoBehaviour
{    
    MapChunkData chunkData;

    private float chunkSpace = 18f; // �� �� ����
    private int currentStage; // [todo] �� ���������ѹ��� ������ currentStage �ѹ� ����

	void Start()
    {
        currentStage = SceneManager.GetActiveScene().buildIndex;
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

