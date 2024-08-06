using GameBalance;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryScene : BaseScene
{
	public int currentStage;

	protected override void Init()
	{
		base.Init();
		SceneType = Scene.StoryScene;
		Time.timeScale = 1.0f;

		currentStage = PlayerPrefs.GetInt("StoryNumber");
		

		PlayStory(currentStage);
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
			if (startFrom == 1)  // Started from start scene
			{
				Managers.Scene.LoadScene(Scene.LobbyScene);  // Load lobby scene
			}
			else if (startFrom == 2)  // Started from stage button
			{
				Managers.Scene.LoadScene(Scene.RunningScene);  // Load running scene
			}
		}
	}
}
