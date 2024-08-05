using GameBalance;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_StoryScene : UI_Scene
{
	enum Texts
	{
		NameText,
		DialogueText
	}

	enum Images
	{
		CutSceneImg
	}

	enum Buttons
	{
		DialogueBackground
	}

	private TMP_Text nameText; // 대화창 이름 Text
	private TMP_Text dialogueText; // 대화창 대사 Text
	private Image cutSceneImg;

	public List<StoryData> dialogues = new List<StoryData>(); // 대화 리스트
	public int dialogueIndex = 0; // 대화 인덱스

	private void Awake()
	{
		Init();
	}

	public override bool Init()
	{
		if (base.Init() == false)
			return false;

		Bind<TMP_Text>(typeof(Texts));
		Bind<Image>(typeof(Images));
		Bind<Button>(typeof(Buttons));

		nameText = Get<TMP_Text>((int)Texts.NameText);
		dialogueText = Get<TMP_Text>((int)Texts.DialogueText);
		cutSceneImg = Get<Image>((int)Images.CutSceneImg);

		Get<Button>((int)Buttons.DialogueBackground).gameObject.BindEvent(OnClickBackground);

		return true;
	}

	// 대화 확인
	public void CheckDialogue()
	{
		dialogueIndex++; // 대화 인덱스 증가

		// dialogues가 남아있다면
		if (dialogueIndex < dialogues.Count)
		{
			ShowDialogue(); // 대화창 표시
		}
		else
		{
			HideDialogueUI(); // 대화창 UI 숨기기
			Debug.Log("대화 끝");
		}
	}

	// 대화 목록 초기화
	public void InitDialogues(List<StoryData> dialogues)
	{
		this.dialogues = dialogues ?? new List<StoryData>(); // 대화 리스트 할당
		dialogueIndex = 0; // 대화 인덱스 초기화

		if (this.dialogues.Count > 0)
		{
			ShowDialogue(); // 대화창 표시
		}
		else
		{
			Debug.LogError("Dialogues list is empty or null.");
			HideDialogueUI();
		}
	}

	// 대화창 표시
	private void ShowDialogue()
	{
		if (dialogueIndex >= 0 && dialogueIndex < dialogues.Count)
		{
			var dialogue = dialogues[dialogueIndex]; // 현재 대화
			ShowDialogueUI(dialogue.Talker, dialogue.Scripts, dialogue.CharacterPortraitPath); // 대화창 UI 표시
		}
		else
		{
			Debug.LogError("Dialogue index is out of range.");
		}
	}

	// 대화창 UI 표시
	private void ShowDialogueUI(string name, string dialogue, string CutScenePath)
	{
		nameText.text = name; // 이름 표시
		dialogueText.text = dialogue; // 대사 표시
		cutSceneImg.sprite = Resources.Load<Sprite>(CutScenePath);

		// 기존 팝업을 다시 보여주기만 합니다.
		gameObject.SetActive(true);
	}

	// 대화창 UI 숨기기
	public void HideDialogueUI()
	{
		gameObject.SetActive(false);
	}

	private void OnClickBackground(PointerEventData eventData)
	{
		CheckDialogue();
	}
}
