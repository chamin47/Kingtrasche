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

	private TMP_Text nameText; // ��ȭâ �̸� Text
	private TMP_Text dialogueText; // ��ȭâ ��� Text
	private Image cutSceneImg;

	public List<string> dialogues = new List<string>(); // ��ȭ ����Ʈ
	public int dialogueIndex = 0; // ��ȭ �ε���

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
		// F ������ CheckDialogue ȣ��
		if (Input.GetKeyDown(KeyCode.F))
		{
			CheckDialogue();
		}
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
	public void InitDialogues(List<string> dialogues)
	{
		this.dialogues = dialogues; // ��ȭ ����Ʈ �Ҵ�
		dialogueIndex = 0; // ��ȭ �ε��� �ʱ�ȭ

		ShowDialogue(); // ��ȭâ ǥ��
	}

	// ��ȭâ ǥ��
	private void ShowDialogue()
	{
		var dialogue = dialogues[dialogueIndex]; // ���� ��ȭ

		//ShowDialogueUI(dialogue.TalkerKo, dialogue.Scripts); // ��ȭâ UI ǥ��
	}

	// ��ȭâ UI ǥ��
	private void ShowDialogueUI(string name, string dialogue)
	{
		nameText.text = name; // �̸� ǥ��
		dialogueText.text = dialogue; // ��� ǥ��

		// �˾� Ȱ��ȭ
		Managers.UI.ShowPopupUI<DialogueHandler>();
	}

	// ��ȭâ UI �����
	public void HideDialogueUI()
	{
		Managers.UI.ClosePopupUI(this);
	}
}
