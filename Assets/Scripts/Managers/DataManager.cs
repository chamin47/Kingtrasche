using GameBalance;
using System.Collections;
using System.Collections.Generic;
using UGS;
using UnityEngine;

public class DataManager
{
	private Dictionary<int, PlayerData> playerDataMap = new Dictionary<int, PlayerData>();

	public void Init()
	{
		LoadPlayerData();
		// 다른 데이터 초기화 메서드 호출
		// LoadMonsterData();
	}

	private void LoadPlayerData()
	{
		UnityGoogleSheet.LoadFromGoogle<int, PlayerData>((list, map) =>
		{
			foreach (var data in list)
			{
				playerDataMap[data.index] = data;
			}
		}, true);
	}

	public PlayerData GetPlayerData(int id)
	{
		if (playerDataMap.TryGetValue(id, out PlayerData data))
		{
			return data;
		}
		else
		{
			Debug.LogError("Player data not found!");
			return null;
		}
	}

	// 다른 데이터 로드 및 접근 메서드 예시
	// private void LoadMonsterData() { ... }
	// public MonsterData GetMonsterData(int id) { ... }
}
