using GameBalance;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryScene : BaseScene
{
    public int currentStage;
    private Dictionary<int, int> stageToStoryMap;

    protected override void Init()
    {
        base.Init();
        SceneType = Scene.StoryScene;
        Time.timeScale = 1.0f;

        InitializeStageToStoryMap();

        int stageNumber = PlayerPrefs.GetInt("StageNumber");
        currentStage = stageToStoryMap.ContainsKey(stageNumber) ? stageToStoryMap[stageNumber] : 0;


        PlayStory(currentStage);
    }

    private void InitializeStageToStoryMap()
    {
        stageToStoryMap = new Dictionary<int, int>
        {
            {1, 1010},
            {5, 1030},
            {7, 1050},
            {8, 1060},
            {11, 1070},
            {14, 1100},
            {15, 1110},
            {18, 1130},
            {21, 1150},
            {22, 1160},
            {1000, 1000},
            {1001, 1001},
            {1160, 1160}
        };
    }

    public override void Clear()
    {
        Debug.Log("StoryScene Clear!");
    }

    private void PlayStory(int stage)
    {
        List<StoryData> dialogues = Managers.Dialogue.GetDialogueForStage(stage);
        if (dialogues.Count > 0)
        {
            StartCoroutine(PlayDialogueCoroutine(dialogues));
        }
        else
        {
            LoadRunningScene();
        }
    }

    private IEnumerator PlayDialogueCoroutine(List<StoryData> dialogues)
    {
        UI_StoryScene storyUI = Managers.UI.ShowSceneUI<UI_StoryScene>();
        if (storyUI != null)
        {
            storyUI.InitDialogues(dialogues);
            while (!storyUI.IsDialogueFinished)
            {
                yield return null;
            }
        }

        LoadRunningScene(); // 모든 대화가 끝나면 다음 씬을 로드합니다.
    }

    private void LoadRunningScene()
    {
        if (PlayerPrefs.GetInt("TutorialPlayed", 0) == 0)
        {
            // Set the tutorial as played
            PlayerPrefs.SetInt("TutorialPlayed", 1);
            PlayerPrefs.Save();
            Managers.Scene.LoadScene(Scene.RunningTutorialScene);
        }
        else
        {
            int startFrom = PlayerPrefs.GetInt("StartFrom");
            if (startFrom == 1)
            {
                Managers.Scene.LoadScene(Scene.LobbyScene);
            }
            else if (startFrom == 2)
            {
                Managers.Scene.LoadScene(Scene.StageScene);
            }
            else if (startFrom == 3)
            {
                LoadingManager.LoadScene("RunningScene");
            }
            else if (startFrom == 4)
            {
                LoadingManager.LoadScene("BossScene1");
            }
            else if (startFrom == 5)
            {
                LoadingManager.LoadScene("BossScene2");
            }
            else if (startFrom == 6)
            {
                LoadingManager.LoadScene("BossScene3");
            }
            else if (startFrom == 7)
            {
				Managers.Scene.LoadScene(Scene.LobbyScene);
			}
        }
    }
}
