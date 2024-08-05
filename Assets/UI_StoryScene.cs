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

	private TMP_Text nameText; // ��ȭâ �̸� Text
	private TMP_Text dialogueText; // ��ȭâ ��� Text
	private Image cutSceneImg;

	public List<StoryData> dialogues = new List<StoryData>(); // ��ȭ ����Ʈ
	public int dialogueIndex = 0; // ��ȭ �ε���

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

	// ��ȭ Ȯ��
	public void CheckDialogue()
	{
		dialogueIndex++; // ��ȭ �ε��� ����

		// dialogues�� �����ִٸ�
		if (dialogueIndex < dialogues.Count)
		{
			ShowDialogue(); // ��ȭâ ǥ��
		}
		else
		{
			HideDialogueUI(); // ��ȭâ UI �����
			Debug.Log("��ȭ ��");
		}
	}

	// ��ȭ ��� �ʱ�ȭ
	public void InitDialogues(List<StoryData> dialogues)
	{
		this.dialogues = dialogues ?? new List<StoryData>(); // ��ȭ ����Ʈ �Ҵ�
		dialogueIndex = 0; // ��ȭ �ε��� �ʱ�ȭ

		if (this.dialogues.Count > 0)
		{
			ShowDialogue(); // ��ȭâ ǥ��
		}
		else
		{
			Debug.LogError("Dialogues list is empty or null.");
			HideDialogueUI();
		}
	}

	// ��ȭâ ǥ��
	private void ShowDialogue()
	{
		if (dialogueIndex >= 0 && dialogueIndex < dialogues.Count)
		{
			var dialogue = dialogues[dialogueIndex]; // ���� ��ȭ
			ShowDialogueUI(dialogue.Talker, dialogue.Scripts, dialogue.CharacterPortraitPath); // ��ȭâ UI ǥ��
		}
		else
		{
			Debug.LogError("Dialogue index is out of range.");
		}
	}

	// ��ȭâ UI ǥ��
	private void ShowDialogueUI(string name, string dialogue, string CutScenePath)
	{
		nameText.text = name; // �̸� ǥ��
		dialogueText.text = dialogue; // ��� ǥ��
		cutSceneImg.sprite = Resources.Load<Sprite>(CutScenePath);

		// ���� �˾��� �ٽ� �����ֱ⸸ �մϴ�.
		gameObject.SetActive(true);
	}

	// ��ȭâ UI �����
	public void HideDialogueUI()
	{
		gameObject.SetActive(false);
	}

	private void OnClickBackground(PointerEventData eventData)
	{
		CheckDialogue();
	}
}
