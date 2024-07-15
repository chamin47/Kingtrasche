using UnityEngine;

public class RunningMapManager : MonoBehaviour
{
    // [todo] 엑셀연동해서 자동으로 스테이지별 맵경로 배열 가져오기
    private string[][] stageChunk =
    {
       new string[] { "MapChunk/TestChunk001", "MapChunk/TestChunk003", "MapChunk/TestChunk005", "MapChunk/TestChunk002" },
       new string[] {"MapChunk/TestChunk006","MapChunk/TestChunk007","MapChunk/TestChunk001" }
    };

    private float chunkSpace = 18f; // 맵 간 간격
    public int currentStage = 1; // [todo] 각 스테이지넘버를 누르면 currentStage 넘버 변경

    void Start()
    {
        string[] prefabPaths = stageChunk[currentStage - 1]; //스테이지별 mapChunk배열주소 가져옴
        GameObject[] mapChunk = new GameObject[prefabPaths.Length];
        for (int i = 0; i < prefabPaths.Length; i++)
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

