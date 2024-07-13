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
		// �ٸ� ������ �ʱ�ȭ �޼��� ȣ��
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

	// �ٸ� ������ �ε� �� ���� �޼��� ����
	// private void LoadMonsterData() { ... }
	// public MonsterData GetMonsterData(int id) { ... }
}
