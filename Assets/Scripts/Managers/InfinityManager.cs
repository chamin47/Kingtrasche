using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

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
        Shuffle(mapChunk);
        GameObject[] SuffleChunk = new GameObject[mapChunk.Length];
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

    private void Shuffle(GameObject[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            GameObject temp = array[i];
            int randomIndex = Random.Range(0, array.Length);
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
    }
}
