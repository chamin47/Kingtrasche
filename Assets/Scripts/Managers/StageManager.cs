using UnityEngine;

public class StageManager
{
    private const int TotalStages = 21;

    public void Init()
    {
        if (!PlayerPrefs.HasKey("Stage1"))
        {
            PlayerPrefs.SetInt("Stage1", 1);
            for (int i = 2; i <= TotalStages; i++)
            {
                PlayerPrefs.SetInt("Stage" + i, 0);
            }
        }
    }

    public bool IsStageUnlocked(int stageNumber)
    {
        return PlayerPrefs.GetInt("Stage" + stageNumber) == 1;
    }

    public void UnlockStage(int stageNumber)
    {
        PlayerPrefs.SetInt("Stage" + stageNumber, 1);
    }

    public void OnStageClear(int currentStageNumber)
    {
        int nextStageNumber = currentStageNumber + 1;
        if (nextStageNumber <= 21)
        {
            UnlockStage(nextStageNumber);
            Debug.Log("스테이지 " + nextStageNumber + "이 해금되었습니다.");
        }

        // 스테이지 최초 클리어 할 때만 업적스토리 카운트 올라감.
        if (!PlayerPrefs.HasKey("StageCleared" + currentStageNumber))
        {
            PlayerPrefs.SetInt("StageCleared" + currentStageNumber, 1);

            int storyLevel = PlayerPrefs.GetInt("StoryLevel");
            storyLevel += 1;
            PlayerPrefs.SetInt("StoryLevel", storyLevel);
            PlayerPrefs.Save();
        }
    }
}
