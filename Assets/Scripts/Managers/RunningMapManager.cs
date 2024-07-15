using UnityEngine;

public class RunningMapManager : MonoBehaviour
{
    // [todo] ���������ؼ� �ڵ����� ���������� �ʰ�� �迭 ��������
    private string[][] stageChunk =
    {
       new string[] { "MapChunk/TestChunk001", "MapChunk/TestChunk003", "MapChunk/TestChunk005", "MapChunk/TestChunk002" },
       new string[] {"MapChunk/TestChunk006","MapChunk/TestChunk007","MapChunk/TestChunk001" }
    };

    private float chunkSpace = 18f; // �� �� ����
    public int currentStage = 1; // [todo] �� ���������ѹ��� ������ currentStage �ѹ� ����

    void Start()
    {
        string[] prefabPaths = stageChunk[currentStage - 1]; //���������� mapChunk�迭�ּ� ������
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

