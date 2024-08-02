using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogueHandler : UI_Popup
{
	enum Texts
	{
		NameText,
		DialogueText
	}

	enum Images
	{
		CutSceneImg,
	}

	private TMP_Text nameText; // 대화창 이름 Text
	private TMP_Text dialogueText; // 대화창 대사 Text
	private Image cutSceneImg;

	public List<string> dialogues = new List<string>(); // 대화 리스트
	public int dialogueIndex = 0; // 대화 인덱스

	public static DialogueHandler I;

	private void Awake()
	{
		I = this;
		Init();
	}

	public override bool Init()
	{
		if (base.Init() == false)
			return false;

		Bind<TMP_Text>(typeof(Texts));
		Bind<Image>(typeof(Images));

		nameText = Get<TMP_Text>((int)Texts.NameText);
		dialogueText = Get<TMP_Text>((int)Texts.DialogueText);

		Get<Image>((int)Images.CutSceneImg).sprite = Resources.Load<Sprite>("CutsceneImg/Img1");

		return true;
	}

	private void Update()
	{
		// F 누르면 CheckDialogue 호출
		if (Input.GetKeyDown(KeyCode.F))
		{
			CheckDialogue();
		}
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
	public void InitDialogues(List<string> dialogues)
	{
		this.dialogues = dialogues; // 대화 리스트 할당
		dialogueIndex = 0; // 대화 인덱스 초기화

		ShowDialogue(); // 대화창 표시
	}

	// 대화창 표시
	private void ShowDialogue()
	{
		var dialogue = dialogues[dialogueIndex]; // 현재 대화

		//ShowDialogueUI(dialogue.TalkerKo, dialogue.Scripts); // 대화창 UI 표시
	}

	// 대화창 UI 표시
	private void ShowDialogueUI(string name, string dialogue)
	{
		nameText.text = name; // 이름 표시
		dialogueText.text = dialogue; // 대사 표시

		// 팝업 활성화
		Managers.UI.ShowPopupUI<DialogueHandler>();
	}

	// 대화창 UI 숨기기
	public void HideDialogueUI()
	{
		Managers.UI.ClosePopupUI(this);
	}
}
