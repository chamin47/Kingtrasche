using GameBalance;
using System;
using System.Collections.Generic;
using UnityEngine;

public class RunningMapManager : MonoBehaviour
{
	public static RunningMapManager Instance;

	MapChunkData chunkData;

	private float chunkSpace = 18f; // ¸Ê °£ °£°Ý
	public int currentStage = 1;
	private Vector3 lastMapTransform;

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
		for (int i = 0; i < prefabPaths.Count; i++)
		{
			mapChunk[i] = Managers.Resource.Load<GameObject>(prefabPaths[i]);
		}

		for (int i = 0; i < mapChunk.Length; i++)
		{
			Vector3 chunkPosition = new Vector3(chunkSpace + i * chunkSpace, 0, 0);
			Instantiate(mapChunk[i], chunkPosition, Quaternion.identity);
			if (i == mapChunk.Length - 1)
			{
				lastMapTransform = chunkPosition;
			}
		}
		EndMapSpawn?.Invoke(lastMapTransform);
	}
}