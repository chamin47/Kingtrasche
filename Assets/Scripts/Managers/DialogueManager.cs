using GameBalance;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager
{

	private Dictionary<int, List<StoryData>> stageDialogues = new Dictionary<int, List<StoryData>>();

	public void Init()
	{
		GenerateStageDialogues();
	}

	private void GenerateStageDialogues()
	{
		stageDialogues = new Dictionary<int, List<StoryData>>
		{
			{
				1000, new List<StoryData>		// 타이틀 -> 스타트버튼
				{
					StoryData.StoryDataMap[1001],
					StoryData.StoryDataMap[1002],
					StoryData.StoryDataMap[1003],
					StoryData.StoryDataMap[1004],
					StoryData.StoryDataMap[1005],
					StoryData.StoryDataMap[1006],
					StoryData.StoryDataMap[1007],
					StoryData.StoryDataMap[1008],
					StoryData.StoryDataMap[1101],
					StoryData.StoryDataMap[1102],
					StoryData.StoryDataMap[1103],
					StoryData.StoryDataMap[1104],
					StoryData.StoryDataMap[1105],
					StoryData.StoryDataMap[1106],
					StoryData.StoryDataMap[1107],
					StoryData.StoryDataMap[1108],
					StoryData.StoryDataMap[1009],					
				}
			},
			{
				1001, new List<StoryData>		// 스토리모드 선택 이후
				{
					StoryData.StoryDataMap[2001],
					StoryData.StoryDataMap[2002],
					StoryData.StoryDataMap[2003],
					StoryData.StoryDataMap[2004],
					StoryData.StoryDataMap[2005],
					StoryData.StoryDataMap[2006],
					StoryData.StoryDataMap[2101],
					StoryData.StoryDataMap[2102],
					StoryData.StoryDataMap[2103],
					StoryData.StoryDataMap[2104],
					StoryData.StoryDataMap[2105],
					StoryData.StoryDataMap[2106],
					StoryData.StoryDataMap[2107],
					StoryData.StoryDataMap[2108],
					StoryData.StoryDataMap[2201],
					StoryData.StoryDataMap[2202],
					StoryData.StoryDataMap[2203],
					StoryData.StoryDataMap[2204],
					StoryData.StoryDataMap[2205],
					StoryData.StoryDataMap[2206],
					StoryData.StoryDataMap[2207],
				}
			},
			{
				1010, new List<StoryData>
				{
					StoryData.StoryDataMap[3001],
					StoryData.StoryDataMap[3002],
					StoryData.StoryDataMap[3003],
					StoryData.StoryDataMap[3004],
					StoryData.StoryDataMap[3005],
				}
			},
			{
				1030, new List<StoryData>
				{
					StoryData.StoryDataMap[4001],
					StoryData.StoryDataMap[4002],
					StoryData.StoryDataMap[4003],
					StoryData.StoryDataMap[4004],
					StoryData.StoryDataMap[4005],
					StoryData.StoryDataMap[4006],
					StoryData.StoryDataMap[4007],
					StoryData.StoryDataMap[4008],
					StoryData.StoryDataMap[4009],
					StoryData.StoryDataMap[4101],
					StoryData.StoryDataMap[4102],
					StoryData.StoryDataMap[4103],
					StoryData.StoryDataMap[4104],
				}
			},
			{
				1050, new List<StoryData>
				{
					StoryData.StoryDataMap[5001],
					StoryData.StoryDataMap[5002],
					StoryData.StoryDataMap[5003],
					StoryData.StoryDataMap[5004],
					StoryData.StoryDataMap[5005],
					StoryData.StoryDataMap[5006],
				}
			},
			{
				1060, new List<StoryData>
				{
					StoryData.StoryDataMap[6001],
					StoryData.StoryDataMap[6002],
					StoryData.StoryDataMap[6003],
					StoryData.StoryDataMap[6004],
					StoryData.StoryDataMap[6005],
					StoryData.StoryDataMap[6101],
					StoryData.StoryDataMap[6102],
					StoryData.StoryDataMap[6103],
					StoryData.StoryDataMap[6104],
					StoryData.StoryDataMap[6105],
					StoryData.StoryDataMap[6106],
					StoryData.StoryDataMap[6201],
					StoryData.StoryDataMap[6202],
					StoryData.StoryDataMap[6203],
					StoryData.StoryDataMap[6204],
					StoryData.StoryDataMap[6205],
					StoryData.StoryDataMap[6206],
					StoryData.StoryDataMap[6207],
					StoryData.StoryDataMap[6208],
					StoryData.StoryDataMap[6209],
				}
			},
			{
				1070, new List<StoryData>
				{
					StoryData.StoryDataMap[7001],
					StoryData.StoryDataMap[7002],
					StoryData.StoryDataMap[7003],
					StoryData.StoryDataMap[7004],
					StoryData.StoryDataMap[7005],
					StoryData.StoryDataMap[7101],
					StoryData.StoryDataMap[7102],
					StoryData.StoryDataMap[7103],
					StoryData.StoryDataMap[7104],
				}
			},
			{
				1100, new List<StoryData>
				{
					StoryData.StoryDataMap[8001],
					StoryData.StoryDataMap[8002],
					StoryData.StoryDataMap[8003],
					StoryData.StoryDataMap[8004],
					StoryData.StoryDataMap[8005],
					StoryData.StoryDataMap[8006],
					StoryData.StoryDataMap[8007],
					StoryData.StoryDataMap[8008],
					StoryData.StoryDataMap[8009],
					StoryData.StoryDataMap[8010],
				}
			},
			{
				1110, new List<StoryData>
				{
					StoryData.StoryDataMap[9001],
					StoryData.StoryDataMap[9002],
					StoryData.StoryDataMap[9003],
					StoryData.StoryDataMap[9004],
					StoryData.StoryDataMap[9005],
					StoryData.StoryDataMap[9006],
					StoryData.StoryDataMap[9007],
					StoryData.StoryDataMap[9008],
					StoryData.StoryDataMap[9009],
					StoryData.StoryDataMap[9010],
				}
			},
			{
				1130, new List<StoryData>
				{
					StoryData.StoryDataMap[10001],
					StoryData.StoryDataMap[10002],
					StoryData.StoryDataMap[10003],
					StoryData.StoryDataMap[10004],
					StoryData.StoryDataMap[10005],
					StoryData.StoryDataMap[10006],
				}
			},
			{
				1150, new List<StoryData>
				{
					StoryData.StoryDataMap[11001],
					StoryData.StoryDataMap[11002],
					StoryData.StoryDataMap[11003],
					StoryData.StoryDataMap[11004],
					StoryData.StoryDataMap[11005],
					StoryData.StoryDataMap[11006],
					StoryData.StoryDataMap[11007],
					StoryData.StoryDataMap[11008],
					StoryData.StoryDataMap[11009],
				}
			},
			{
				1160, new List<StoryData>
				{
					StoryData.StoryDataMap[12001],
					StoryData.StoryDataMap[12002],
					StoryData.StoryDataMap[12003],
					StoryData.StoryDataMap[12004],
					StoryData.StoryDataMap[12005],
					StoryData.StoryDataMap[12006],
					StoryData.StoryDataMap[12007],
					StoryData.StoryDataMap[12008],
					StoryData.StoryDataMap[12008],
					StoryData.StoryDataMap[12101],
					StoryData.StoryDataMap[12102],
					StoryData.StoryDataMap[12103],
					StoryData.StoryDataMap[12104],
					StoryData.StoryDataMap[12105],
					StoryData.StoryDataMap[12106],
					StoryData.StoryDataMap[12107],
					StoryData.StoryDataMap[12108],
					StoryData.StoryDataMap[12109],
					StoryData.StoryDataMap[12110],
					StoryData.StoryDataMap[12111],
					StoryData.StoryDataMap[12112],
					StoryData.StoryDataMap[12201],
					StoryData.StoryDataMap[12202],
					StoryData.StoryDataMap[12203],
					StoryData.StoryDataMap[12204],
					StoryData.StoryDataMap[12205],
					StoryData.StoryDataMap[12206],
					StoryData.StoryDataMap[12301],
					StoryData.StoryDataMap[12302],
					StoryData.StoryDataMap[12303],
					StoryData.StoryDataMap[12304],
					StoryData.StoryDataMap[12305],
					StoryData.StoryDataMap[12306],
					StoryData.StoryDataMap[12307],
					StoryData.StoryDataMap[12308],
					StoryData.StoryDataMap[12309],
					StoryData.StoryDataMap[12310],
					StoryData.StoryDataMap[12311],
					StoryData.StoryDataMap[12312],
					StoryData.StoryDataMap[1010],
					StoryData.StoryDataMap[1011],
					StoryData.StoryDataMap[1012],
					StoryData.StoryDataMap[1013],
					StoryData.StoryDataMap[1014],
					StoryData.StoryDataMap[1015],
					StoryData.StoryDataMap[1016],
					StoryData.StoryDataMap[1017],
				}
			},
        };
	}

	public List<StoryData> GetDialogueForStage(int stage)
	{
		if (stageDialogues.ContainsKey(stage))
		{
			return stageDialogues[stage];
		}
		return new List<StoryData>();
	}
}
