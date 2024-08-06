using UnityEngine;

public class GameManager
{
    public int Gold { get; set; }               // 무료재화
    public int Diamond { get; set; }            // 유료재화
    public int RunningPlayCount { get; set; }   // 러닝플레이권
    public int NaturalRunningPlayCount { get; set; } // 자연 충전된 러닝플레이권
    public int MaxRunningPlayCount { get; set; } = 999; // 최대 러닝플레이권
    //public int BestScore { get; set; } // 무한모드 최대점수
    public string Skin { get; set; } //적용스킨

    #region DogAdopt
    // 강아지 입양 여부
    public int AfghanHoundBlack { get; set; }
    public int AfghanHoundRed { get; set; }
    public int AfghanHoundTan { get; set; }

    public int BloodHoundBlack { get; set; }
    public int BloodHoundOrange { get; set; }
    public int BloodHoundRed { get; set; }

    public int DalmationBlack { get; set; }
    public int DalmationBrown { get; set; }
    public int DalmationTricolor { get; set; }

    public int DobermanBlack { get; set; }
    public int DobermanGray { get; set; }
    public int DobermanRed { get; set; }

    public int GreatDaneBlackWhite { get; set; }
    public int GreatDaneSpottedGray { get; set; }
    public int GreatDaneTan { get; set; }

    public int GreyHoundBlack { get; set; }
    public int GreyHoundTan { get; set; }
    public int GreyHoundWhiteSpotted { get; set; }

    public int HuskyBlue { get; set; }
    public int HuskyBrown { get; set; }
    public int HuskyGray { get; set; }

    public int MountainDogAustralian { get; set; }
    public int MountainDogBernese { get; set; }
    public int MountainDogLeonBerger { get; set; }

    public int ShepherdBlack { get; set; }
    public int ShepherdGray { get; set; }
    public int ShepherdPanda { get; set; }

    public int ShibaBlackTan { get; set; }
    public int ShibaCream { get; set; }
    public int ShibaOrange { get; set; }
    #endregion

    public int StoryLevel { get; set; } // 현재 진행중인 레벨
    public int StoryGoal { get; set; } // 목표 레벨
    public int StoryComplete { get; set; } //미션 클리어 여부
    public int StoryReward { get; set; } // 보상

    public int BossLevel { get; set; }
    public int BossGoal { get; set; }
    public int BossComplete { get; set; }
    public int BossReward { get; set; }

    public int BeeLevel { get; set; }
    public int BeeGoal { get; set; }
    public int BeeComplete { get; set; }
    public int BeeReward { get; set; }

    public int PuzzleLevel { get; set; }
    public int PuzzleGoal { get; set; }
    public int PuzzleComplete { get; set; }
    public int PuzzleReward { get; set; }

    public int InfinityLevel { get; set; } // 무한모드 최대 점수
    public int InfinityGoal { get; set; }
    public int InfinityComplete { get; set; }
    public int InfinityReward { get; set; }

    private void Init()
    {
        LoadGame();
    }

    private void SaveGame()
    {
        PlayerPrefs.SetInt("Gold", Gold);
        PlayerPrefs.SetInt("Diamond", Diamond);
        PlayerPrefs.SetInt("RunningPlayCount", RunningPlayCount);
        PlayerPrefs.SetInt("NaturalRunningPlayCount", NaturalRunningPlayCount);
        PlayerPrefs.SetInt("MaxRunningPlayCount", MaxRunningPlayCount);
        //PlayerPrefs.SetInt("BestScore", BestScore);
        PlayerPrefs.SetString("Skin", Skin);

        #region SkinName
        // 강아지
        PlayerPrefs.SetInt("AfghanHoundBlack", AfghanHoundBlack);
        PlayerPrefs.SetInt("AfghanHoundRed", AfghanHoundRed);
        PlayerPrefs.SetInt("AfghanHoundTan", AfghanHoundTan);

        PlayerPrefs.SetInt("BloodHoundBlack", BloodHoundBlack);
        PlayerPrefs.SetInt("BloodHoundOrange", BloodHoundOrange);
        PlayerPrefs.SetInt("BloodHoundRed", BloodHoundRed);

        PlayerPrefs.SetInt("DalmationBlack", DalmationBlack);
        PlayerPrefs.SetInt("DalmationBrown", DalmationBrown);
        PlayerPrefs.SetInt("DalmationTricolor", DalmationTricolor);

        PlayerPrefs.SetInt("DobermanBlack", DobermanBlack);
        PlayerPrefs.SetInt("DobermanGray", DobermanGray);
        PlayerPrefs.SetInt("DobermanRed", DobermanRed);

        PlayerPrefs.SetInt("GreatDaneBlackWhite", GreatDaneBlackWhite);
        PlayerPrefs.SetInt("GreatDaneSpottedGray", GreatDaneSpottedGray);
        PlayerPrefs.SetInt("GreatDaneTan", GreatDaneTan);

        PlayerPrefs.SetInt("GreyHoundBlack", GreyHoundBlack);
        PlayerPrefs.SetInt("GreyHoundTan", GreyHoundTan);
        PlayerPrefs.SetInt("GreyHoundWhiteSpotted", GreyHoundWhiteSpotted);

        PlayerPrefs.SetInt("HuskyBlue", HuskyBlue);
        PlayerPrefs.SetInt("HuskyBrown", HuskyBrown);
        PlayerPrefs.SetInt("HuskyGray", HuskyGray);

        PlayerPrefs.SetInt("MountainDogAustralian", MountainDogAustralian);
        PlayerPrefs.SetInt("MountainDogBernese", MountainDogBernese);
        PlayerPrefs.SetInt("MountainDogLeonBerger", MountainDogLeonBerger);

        PlayerPrefs.SetInt("ShepherdBlack", ShepherdBlack);
        PlayerPrefs.SetInt("ShepherdGray", ShepherdGray);
        PlayerPrefs.SetInt("ShepherdPanda", ShepherdPanda);

        PlayerPrefs.SetInt("ShibaBlackTan", ShibaBlackTan);
        PlayerPrefs.SetInt("ShibaCream", ShibaCream);
        PlayerPrefs.SetInt("ShibaOrange", ShibaOrange);
        #endregion

        PlayerPrefs.SetInt("StoryLevel", StoryLevel);
        PlayerPrefs.SetInt("StoryGoal", StoryGoal);
        PlayerPrefs.SetInt("StoryComplete", StoryComplete);
        PlayerPrefs.SetInt("StoryReward", StoryReward);

        PlayerPrefs.SetInt("BossLevel", BossLevel);
        PlayerPrefs.SetInt("BossGoal", BossGoal);
        PlayerPrefs.SetInt("BossComplete", BossComplete);
        PlayerPrefs.SetInt("BossReward", BossReward);

        PlayerPrefs.SetInt("BeeLevel", BeeLevel);
        PlayerPrefs.SetInt("BeeGoal", BeeGoal);
        PlayerPrefs.SetInt("BeeComplete", BeeComplete);
        PlayerPrefs.SetInt("BeeReward", BeeReward);

        PlayerPrefs.SetInt("PuzzleLevel", PuzzleLevel);
        PlayerPrefs.SetInt("PuzzleGoal", PuzzleGoal);
        PlayerPrefs.SetInt("PuzzleComplete", PuzzleComplete);
        PlayerPrefs.SetInt("PuzzleReward", PuzzleReward);

        PlayerPrefs.SetInt("InfinityLevel", InfinityLevel);
        PlayerPrefs.SetInt("InfinityGoal", InfinityGoal);
        PlayerPrefs.SetInt("InfinityComplete", InfinityComplete);
        PlayerPrefs.SetInt("InfinityReward", InfinityReward);

        PlayerPrefs.Save();
    }

    private void LoadGame()
    {
        Gold = PlayerPrefs.GetInt("Gold", 0);
        Diamond = PlayerPrefs.GetInt("Diamond", 0);
        RunningPlayCount = PlayerPrefs.GetInt("RunningPlayCount", 0);
        NaturalRunningPlayCount = PlayerPrefs.GetInt("NaturalRunningPlayCount", 0);
        MaxRunningPlayCount = PlayerPrefs.GetInt("MaxRunningPlayCount", 10);
        Skin = PlayerPrefs.GetString("Skin", "MountainDogBernese");

        #region Skin
        //강아지
        AfghanHoundBlack = PlayerPrefs.GetInt("AfghanHoundBlack", 0);
        AfghanHoundRed = PlayerPrefs.GetInt("AfghanHoundRed", 0);
        AfghanHoundTan = PlayerPrefs.GetInt("AfghanHoundTan", 0);

        BloodHoundBlack = PlayerPrefs.GetInt("BloodHoundBlack", 0);
        BloodHoundOrange = PlayerPrefs.GetInt("BloodHoundOrange", 0);
        BloodHoundRed = PlayerPrefs.GetInt("BloodHoundRed", 0);

        DalmationBlack = PlayerPrefs.GetInt("DalmationBlack", 0);
        DalmationBrown = PlayerPrefs.GetInt("DalmationBrown", 0);
        DalmationTricolor = PlayerPrefs.GetInt("DalmationTricolor", 0);

        DobermanBlack = PlayerPrefs.GetInt("DobermanBlack", 0);
        DobermanGray = PlayerPrefs.GetInt("DobermanGray", 0);
        DobermanRed = PlayerPrefs.GetInt("DobermanRed", 0);

        GreatDaneBlackWhite = PlayerPrefs.GetInt("GreatDaneBlackWhite", 0);
        GreatDaneSpottedGray = PlayerPrefs.GetInt("GreatDaneSpottedGray", 0);
        GreatDaneTan = PlayerPrefs.GetInt("GreatDaneTan", 0);

        HuskyBlue = PlayerPrefs.GetInt("HuskyBlue", 0);
        HuskyBrown = PlayerPrefs.GetInt("HuskyBrown", 0);
        HuskyGray = PlayerPrefs.GetInt("HuskyGray", 0);

        MountainDogAustralian = PlayerPrefs.GetInt("MountainDogAustralian", 0);
        MountainDogBernese = PlayerPrefs.GetInt("MountainDogBernese", 1);
        MountainDogLeonBerger = PlayerPrefs.GetInt("MountainDogLeonBerger", 0);

        ShepherdBlack = PlayerPrefs.GetInt("ShepherdBlack", 0);
        ShepherdGray = PlayerPrefs.GetInt("ShepherdGray", 0);
        ShepherdPanda = PlayerPrefs.GetInt("ShepherdPanda", 0);

        ShibaBlackTan = PlayerPrefs.GetInt("ShibaBlackTan", 0);
        ShibaCream = PlayerPrefs.GetInt("ShibaCream", 0);
        ShibaOrange = PlayerPrefs.GetInt("ShibaOrange", 0);
        #endregion

        StoryLevel = PlayerPrefs.GetInt("StoryLevel", 0);
        StoryGoal = PlayerPrefs.GetInt("StoryGoal", 1);
        StoryComplete = PlayerPrefs.GetInt("StoryComplete", 0);
        StoryReward = PlayerPrefs.GetInt("StoryReward", 100);

        BossLevel = PlayerPrefs.GetInt("BossLevel", 0);
        BossGoal = PlayerPrefs.GetInt("BossGoal", 10);
        BossComplete = PlayerPrefs.GetInt("BossComplete", 0);
        BossReward = PlayerPrefs.GetInt("BossReward", 300);

        BeeLevel = PlayerPrefs.GetInt("BeeLevel", 0);
        BeeGoal = PlayerPrefs.GetInt("BeeGoal", 10);
        BeeComplete = PlayerPrefs.GetInt("BeeComplete", 0);
        BeeReward = PlayerPrefs.GetInt("BeeReward", 100);

        PuzzleLevel = PlayerPrefs.GetInt("PuzzleLevel", 0);
        PuzzleGoal = PlayerPrefs.GetInt("PuzzleGoal", 10);
        PuzzleComplete = PlayerPrefs.GetInt("PuzzleComplete", 0);
        PuzzleReward = PlayerPrefs.GetInt("PuzzleReward", 100);

        InfinityLevel = PlayerPrefs.GetInt("InfinityLevel", 0);
        InfinityGoal = PlayerPrefs.GetInt("InfinityGoal", 1000);
        InfinityComplete = PlayerPrefs.GetInt("InfinityComplete", 1);
        InfinityReward = PlayerPrefs.GetInt("InfinityReward", 3);
    }

    public void GameOver()
    {
        Managers.UI.ShowPopupUI<UI_GameOverPopup>();
        Time.timeScale = 0;
    }

    public void GameClear()
    {

        Managers.UI.ShowPopupUI<UI_StageClearPopup>();
        Time.timeScale = 0;
    }

    public void InfinityGameOver()
    {
        Managers.UI.ShowPopupUI<UI_InfinityGameOverPopup>();
        Time.timeScale = 0;
    }

    public void PurchaseRunningPlayCount(int amount)
    {
        if (Diamond >= amount)
        {
            Diamond -= amount;
            RunningPlayCount += amount;

            // 유료재화로 구매한 러닝플레이권은 최대치 제한 없음
            SaveGame();
        }
        else
        {
            Debug.Log("유료재화가 부족합니다.");
        }
    }
}
