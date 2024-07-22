using System.Collections;
using UnityEngine;

public class InfinityManager : MonoBehaviour
{
    private GameObject[] mapChunk;

    private int activeChunk; // 10개 미만이면 다시 배치
    private int chunkSpace = 18;
    private float deactiiveTime = 5f;
    private Vector3 lastChunkVector;


    private void Start()
    {
        mapChunk = Resources.LoadAll<GameObject>("Prefabs/MapChunk/");
        for (int i = 0; i < mapChunk.Length; i++)
        {
            Vector3 chunkPosition = new Vector3(chunkSpace + i * chunkSpace, 0, 0);
            GameObject go = Instantiate(mapChunk[i], chunkPosition, Quaternion.identity);
            go.SetActive(true);
            activeChunk++;
        }
    }

    private void Update()
    {
        if (activeChunk <= 10)
        {

        }
    }

    IEnumerator DeactiveChunk(float time)
    {
        yield return new WaitForSeconds(time);
        activeChunk--;
    }

}
