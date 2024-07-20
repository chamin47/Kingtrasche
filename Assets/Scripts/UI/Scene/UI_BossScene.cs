using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_BossScene : UI_Scene
{
	#region enum
	enum GameObjects
	{
		HeartsParent
	}

	enum Buttons
	{
		PauseButton
	}

	enum Texts
	{

	}

	enum Images
	{
		HealthBarFrame,
		HealthBar
	}
	#endregion

	private GameObject[] heartContainers;
	private Image[] heartFills;
	private GameObject player;
	private GameObject boss;
	private PlayerController playerController;
	private CatBossController catBossController;
	private Image currentHealthBar;

	public Transform heartsParent;
	private GameObject heartContainerPrefab;

	private void Awake()
	{
		Init();
	}

	public override bool Init()
	{
		if (base.Init() == false)
			return false;

		Bind<GameObject>(typeof(GameObjects));
		Bind<TMP_Text>(typeof(Texts));
		Bind<Button>(typeof(Buttons));
		Bind<Image>(typeof(Images));

		Get<Button>((int)Buttons.PauseButton).gameObject.BindEvent(OnClickPauseButton);
		heartsParent = Get<GameObject>((int)GameObjects.HeartsParent).gameObject.transform;
		heartContainerPrefab = Managers.Resource.Load<GameObject>("UI/SubItem/HeartContainer");
		player = GameObject.FindWithTag("Player");
		boss = GameObject.FindWithTag("Boss");
		playerController = player.GetComponent<PlayerController>();
		catBossController = boss.GetComponent<CatBossController>();

		heartContainers = new GameObject[3];  // Assuming 5 is the max number of hearts
		heartFills = new Image[3];

		currentHealthBar = Get<Image>((int)Images.HealthBar);

		playerController.OnHealthChanged += UpdateHeartsHUD;
		catBossController.OnHealthChanged += UpdateHealthBar;
		InstantiateHeartContainers();
		UpdateHeartsHUD();  // Corrected method name

		return true;
	}

	private void UpdateHeartsHUD()
	{
		SetHeartContainers();
		SetFilledHearts();
	}

	private void SetHeartContainers()
	{
		for (int i = 0; i < heartContainers.Length; i++)
		{
			if (i < playerController.life)
			{
				heartContainers[i].SetActive(true);
			}
			else
			{
				heartContainers[i].SetActive(false);
			}
		}
	}

	private void SetFilledHearts()
	{
		for (int i = 0; i < heartFills.Length; i++)
		{
			heartFills[i].fillAmount = (i < playerController.life) ? 1 : 0;
		}
	}

	private void InstantiateHeartContainers()
	{
		// Clear existing containers
		foreach (GameObject container in heartContainers)
		{
			if (container != null)
				Destroy(container);
		}

		for (int i = 0; i < playerController.life; i++)
		{
			GameObject temp = Instantiate(heartContainerPrefab);
			temp.transform.SetParent(heartsParent, false);
			heartContainers[i] = temp;
			heartFills[i] = temp.transform.Find("HeartFill").GetComponent<Image>();
		}
	}

	private void UpdateHealthBar()
	{
		float ratio = catBossController.currentHealth / (float)catBossController.maxHealth;
		currentHealthBar.fillAmount = ratio;
		Debug.Log("보스바 UI 업데이트: " + ratio);
	}

	private void OnClickPauseButton(PointerEventData eventData)
	{
		Managers.UI.ShowPopupUI<UI_PausePopup>();
		Time.timeScale = 0;
	}
}
