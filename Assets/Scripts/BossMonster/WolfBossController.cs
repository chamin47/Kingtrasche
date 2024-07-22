using GameBalance;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WolfBossController : MonoBehaviour, IBossController
{
	public BossState currentState = BossState.Idle;
	public int currentHealth { get; private set; }
	public int maxHealth { get; private set; }
	private GameObject crescentBullet;
	private GameObject clawEffectPrefab;
	
	private Transform player;
	private Animator anim;

	private List<Skill> skillList = new List<Skill>();
	private int currentSkillIndex = 0;
	private int currentSkillCount = 0;
	private float skillInterval = 1.5f; // 스킬 실행 간격
	private float skillTimer = 0.0f;
	private bool isSkillExecuting = false;
	private bool facingRight = true;

	public event Action OnHealthChanged;

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
		crescentBullet = Managers.Resource.Load<GameObject>("BossSkill/WolfBoss/CrescentBullet");
		clawEffectPrefab = Managers.Resource.Load<GameObject>("BossSkill/WolfBoss/ClawEffect");


		// 초기 스킬 설정
		Phase1();
	}

	private void Update()
	{
		if (player.position.x < transform.position.x)
		{
			facingRight = false;
			transform.rotation = Quaternion.Euler(0, 0, 0); // 왼쪽
		}
		else
		{
			facingRight = true;
			transform.rotation = Quaternion.Euler(0, 180, 0); // 오른쪽
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
					Phase1();
				}
				break;
			case BossState.Phase2:
				if (currentHealth <= maxHealth * 0.33f)
				{
					currentState = BossState.Phase3;
					Debug.Log("Phase 3 시작");
					Phase1();
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
		yield return new WaitForSeconds(0f);
		//anim.SetTrigger("isAttack");
		Vector2 direction = player.position.x > transform.position.x ? Vector2.right : Vector2.left;
		GameObject crescent = Instantiate(crescentBullet, transform.position, Quaternion.identity);
		crescent.GetComponent<CrescentAttack>().direction = direction; // 발사 로직 예시
		yield return new WaitForSeconds(1.5f);
	}

	private IEnumerator CrescentAttackCType()
	{
		yield return new WaitForSeconds(0f);
		Vector2 direction = facingRight ? Vector2.right : Vector2.left;

		//anim.SetTrigger("isAttack");
		float[] angles = new float[] { -20f, 0f, 20f };
		foreach (float angle in angles)
		{
			float radian = angle * Mathf.Deg2Rad;
			Vector2 rotatedDirection = new Vector2(
				direction.x * Mathf.Cos(radian) - direction.y * Mathf.Sin(radian),
				direction.x * Mathf.Sin(radian) + direction.y * Mathf.Cos(radian)
			);

			GameObject crescent = Instantiate(crescentBullet, transform.position, Quaternion.identity);
			crescent.GetComponent<CrescentAttack>().direction = rotatedDirection;
		}

		yield return new WaitForSeconds(1.5f);
	}

	private IEnumerator ClawSkill()
	{
		yield return new WaitForSeconds(1f); // 스킬 연출을 위한 대기시간

		// X자 모양 이펙트 생성 및 랜덤 위치 설정
		for (int i = 0; i < 3; i++)
		{
			Vector3 randomPosition = new Vector3(
				Random.Range(-8f, 8f), // 화면의 가로 범위
				Random.Range(-4.5f, 4.5f), // 화면의 세로 범위
				0);

			Instantiate(clawEffectPrefab, randomPosition, Quaternion.identity);
		}
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
		skillList.Add(new Skill(() => CrescentAttack(), 0f, 3));
		skillList.Add(new Skill(() => CrescentAttackCType(), 0f, 3));
		skillList.Add(new Skill(() => ClawSkill(), 0f, 1));
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
