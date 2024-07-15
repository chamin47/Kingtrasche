using UnityEngine;

public class RunningMapManager : MonoBehaviour
{
    private string[] prefabPaths = { "prefabs/MapChunk/TestChunk001", "prefabs/MapChunk/TestChunk003", "prefabs/MapChunk/TestChunk005" };
    private float chunkSpace = 18f; // ¸Ê °£ °£°Ý

    void Start()
    {
        GameObject[] mapChunk = new GameObject[prefabPaths.Length];
        for (int i = 0; i < prefabPaths.Length; i++)
        {
            mapChunk[i] = Resources.Load<GameObject>(prefabPaths[i]);
        }

        for (int i = 0; i < mapChunk.Length; i++)
        {
            Vector3 chunkPosition = new Vector3(chunkSpace + i * chunkSpace, 0, 0);
            Instantiate(mapChunk[i], chunkPosition, Quaternion.identity);
        }
    }

    void Update()
    {

    }
}
