using GameBalance;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WolfBossController : MonoBehaviour
{
	public BossState currentState = BossState.Idle;
	public int maxHealth;
	public int currentHealth;
	private GameObject CrescentBullet;
	
	private Transform player;
	private Animator anim;

	private List<Skill> skillList = new List<Skill>();
	private int currentSkillIndex = 0;
	private int currentSkillCount = 0;
	private float skillInterval = 1.5f; // 스킬 실행 간격
	private float skillTimer = 0.0f;
	private bool isSkillExecuting = false;
	private bool facingRight = true;

	public delegate void Healthchanged();
	public event Healthchanged OnHealthChanged;


	private void Awake()
	{
		maxHealth = BossData.BossDataMap[102].bossHP;
	}

	private void Start()
	{
		currentHealth = maxHealth;
		currentState = BossState.Phase1;
		player = GameObject.FindWithTag("Player").transform;
		anim = GetComponent<Animator>();

		// 스킬 프리팹 로드
		CrescentBullet = Managers.Resource.Load<GameObject>("BossSkill/WolfBoss/CrescentBullet");
		

		// 초기 스킬 설정
		Phase1();
	}

	private void Update()
	{
		if (player.position.x < transform.position.x)
		{
			facingRight = false;
			transform.rotation = Quaternion.Euler(0, 180, 0); // 왼쪽
		}
		else
		{
			facingRight = true;
			transform.rotation = Quaternion.Euler(0, 0, 0); // 오른쪽
		}

		if (isSkillExecuting)
		{
			return;
		}

		skillTimer -= Time.deltaTime;
		if (skillTimer <= 0)
		{
			Skill currentSkill = skillList[currentSkillIndex];
			StartCoroutine(ExecuteSkill(currentSkill.Action));
			skillTimer = skillInterval;
			currentSkillCount++;

			if (currentSkillCount >= currentSkill.Count)
			{
				currentSkillIndex = (currentSkillIndex + 1) % skillList.Count;
				currentSkillCount = 0;
			}
		}

		UpdatePhaseTransition();
	}

	private IEnumerator ExecuteSkill(Func<IEnumerator> skill)
	{
		isSkillExecuting = true;
		yield return StartCoroutine(skill());
		isSkillExecuting = false;
	}

	private void UpdatePhaseTransition()
	{
		switch (currentState)
		{
			case BossState.Idle:
				break;
			case BossState.Phase1:
				if (currentHealth <= maxHealth * 0.66f)
				{
					currentState = BossState.Phase2;
					Debug.Log("Phase 2 시작");
					Phase2();
				}
				break;
			case BossState.Phase2:
				if (currentHealth <= maxHealth * 0.33f)
				{
					currentState = BossState.Phase3;
					Debug.Log("Phase 3 시작");
					Phase3();
				}
				break;
			case BossState.Phase3:
				if (currentHealth <= 0)
				{
					currentState = BossState.Dead;
				}
				break;
			case BossState.Dead:
				break;
		}
	}

	private IEnumerator CrescentAttack()
	{
		yield return new WaitForSeconds(1.5f);  // Post skill delay
	}

	private IEnumerator CrescentAttackCType()
	{
		

		yield return null;
	}

	private IEnumerator ClawSkill()
	{

		yield return null;
	}

	private IEnumerator BiteSkill()
	{


		yield return null;
	}

	private IEnumerator HowlingSkill()
	{
		yield return null;
	}

	private IEnumerator ShakeCamera(float duration, float magnitude)
	{
		Vector3 originalPos = Camera.main.transform.localPosition;
		float elapsed = 0.0f;

		while (elapsed < duration)
		{
			float x = Random.Range(-1f, 1f) * magnitude;
			float y = Random.Range(-1f, 1f) * magnitude;

			Camera.main.transform.localPosition = new Vector3(x, y, originalPos.z);

			elapsed += Time.deltaTime;

			yield return null;
		}

		Camera.main.transform.localPosition = originalPos;
	}

	private void Phase1()
	{
		skillList.Clear();
		
	}

	private void Phase2()
	{
		skillList.Clear();
		
	}

	private void Phase3()
	{
		skillList.Clear();
	}

	public void TakeDamage(int damage)
	{
		if (currentState == BossState.Dead)
			return;

		currentHealth -= damage;
		OnHealthChanged.Invoke();  // 이벤트 호출
		if (currentHealth <= 0)
		{
			currentHealth = 0;
			Managers.Game.GameClear();
			Destroy(gameObject);
		}
	}

	private class Skill
	{
		public Func<IEnumerator> Action { get; private set; }
		public float Cooldown { get; private set; }
		public int Count { get; private set; }

		public Skill(Func<IEnumerator> action, float cooldown, int count)
		{
			Action = action;
			Cooldown = cooldown;
			Count = count;
		}
	}
}
