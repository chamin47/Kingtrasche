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

	private Dictionary<int, int> stageDialogueMap;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		currentStage = PlayerPrefs.GetInt("StageNumber");
		stageDialogueMap = new Dictionary<int, int>
		{
			{ 1, 011 }, { 3, 031 }, { 5, 051 }, { 6, 061 }, { 8, 081 }, 
			{ 10, 101 }, { 11, 111 }, { 13, 131 }, { 15, 151 },
        };
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

		ShowDialoguePopup(currentStage);
	}

	private void ShowDialoguePopup(int stage)
	{
		if (stageDialogueMap.TryGetValue(stage, out int dialogueStage))
		{
			List<StoryData> dialogues = Managers.Dialogue.GetDialogueForStage(dialogueStage);
			if (dialogues != null && dialogues.Count > 0)
			{
				UI_DialoguePopup dialoguePopup = Managers.UI.ShowPopupUI<UI_DialoguePopup>();
				if (dialoguePopup != null)
				{
					dialoguePopup.InitDialogues(dialogues);
				}
			}
		}
	}
}