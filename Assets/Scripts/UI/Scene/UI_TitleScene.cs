using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;
using static Define;
using UnityEngine.EventSystems;

public class UI_TitleScene : UI_Scene
{
   enum GameObjects
	{
		Slider,
	}

	enum Buttons
	{
		StartButton,
	}

	enum Texts
	{
		TitleText,
		StartText,
	}

	private void Start()
	{
		base.Init();
		Bind<TMP_Text>(typeof(Texts));
		Bind<Button>(typeof(Buttons));
		Bind<GameObject>(typeof(GameObjects));

		Get<Button>((int)Buttons.StartButton).gameObject.BindEvent(OnButtonClicked);
		StartCoroutine(BlinkTextCoroutine(Get<TMP_Text>((int)Texts.StartText)));
	}

	private void OnButtonClicked(PointerEventData eventData)
	{
		Managers.Scene.LoadScene(Define.Scene.Lobby);
	}

	#region
	private IEnumerator BlinkTextCoroutine(TMP_Text textComponent)
	{
		float duration = 0.5f; // �����̴� �ӵ��� ����

		while (true)
		{
			// �ؽ�Ʈ ���̵� �ƿ�
			while (textComponent.alpha > 0)
			{
				textComponent.alpha -= Time.deltaTime / duration;
				yield return null;
			}
			textComponent.alpha = 0;

			// �ؽ�Ʈ ���̵� ��
			while (textComponent.alpha < 1)
			{
				textComponent.alpha += Time.deltaTime / duration;
				yield return null;
			}
			textComponent.alpha = 1;
		}
	}
	#endregion
}
