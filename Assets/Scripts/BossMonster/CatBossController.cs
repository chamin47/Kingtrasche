using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CatBossController : MonoBehaviour
{
	public enum BossState
	{
		Idle,
		Phase1,
		Phase2,
		Phase3,
		Dead
	}

	public BossState currentState = BossState.Idle;
	private int maxHealth = 200;
	public int currentHealth;
	private GameObject fishbonePrefab;
	private GameObject scratchPrefab;
	private GameObject punchPrefab;
	private GameObject hissPrefab;
	private Transform player;

	private List<Skill> skillList = new List<Skill>();
	private int currentSkillIndex = 0;
	private int currentSkillCount = 0;
	private float skillInterval = 1.5f; // 스킬 실행 간격
	private float skillTimer = 0.0f;
	private bool isSkillExecuting = false;
	private bool facingRight = true;

	private void Start()
	{
		currentHealth = maxHealth;
		currentState = BossState.Phase1;
		player = GameObject.FindWithTag("Player").transform;

		// 스킬 프리팹 로드
		fishbonePrefab = Managers.Resource.Load<GameObject>("BossSkill/CatBoss/Fishbone");
		scratchPrefab = Managers.Resource.Load<GameObject>("BossSkill/CatBoss/Scratch");
		punchPrefab = Managers.Resource.Load<GameObject>("BossSkill/CataBoss/Nyangpunch");
		hissPrefab = Managers.Resource.Load<GameObject>("BossSkill/CatBoss/Nyangpunch");

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

	private IEnumerator FishboneAttack()
	{
		Debug.Log("Fishbone Attack");
		Vector2 direction = player.position.x > transform.position.x ? Vector2.right : Vector2.left;
		GameObject fishbone = Instantiate(fishbonePrefab, transform.position, Quaternion.identity);
		fishbone.GetComponent<FishboneAttack>().direction = direction; // 발사 로직 예시
		yield return new WaitForSeconds(1.5f);  // Post skill delay
	}

	private IEnumerator FishboneAttackCType()
	{
		Debug.Log("Fishbone Attack CType");
		Vector2 direction = facingRight ? Vector2.right : Vector2.left;

		float[] angles = new float[] { -20f, 0f, 20f };
		foreach (float angle in angles)
		{
			float radian = angle * Mathf.Deg2Rad;
			Vector2 rotatedDirection = new Vector2(
				direction.x * Mathf.Cos(radian) - direction.y * Mathf.Sin(radian),
				direction.x * Mathf.Sin(radian) + direction.y * Mathf.Cos(radian)
			);

			GameObject fishbone = Instantiate(fishbonePrefab, transform.position, Quaternion.identity);
			fishbone.GetComponent<FishboneAttack>().direction = rotatedDirection;
		}

		yield return null;
	}


	private IEnumerator ScratchSkill()
	{
		yield return new WaitForSeconds(1.0f);  // Pre skill delay
		Debug.Log("Scratch Skill");
		Vector2 direction = player.position.x > transform.position.x ? Vector2.right : Vector2.left;
		GameObject scratch = Instantiate(scratchPrefab, transform.position, Quaternion.identity);
		scratch.GetComponent<ScratchSkill>().direction = direction;
		yield return new WaitForSeconds(2.0f);  // Post skill delay
	}

	private IEnumerator NyangPunchSkill()
	{
		Debug.Log("NyangPunch Skill");
		Vector2 initialPosition = player.position;

		for (int i = 0; i < 3; i++)
		{
			GameObject pawEffect = new GameObject("PawEffect"); // Create a new GameObject for the paw effect
			pawEffect.transform.position = initialPosition;
			SpriteRenderer renderer = pawEffect.AddComponent<SpriteRenderer>();
			renderer.sprite = Resources.Load<Sprite>("PawEffectSprite"); // Set the sprite for the paw effect

			// Add NyangPunchEffect script to handle the effect logic
			pawEffect.AddComponent<NyangPunchSkill>();

			// Wait for 1 second before placing the next paw effect
			yield return new WaitForSeconds(1.0f);
		}

		yield return new WaitForSeconds(3.0f); // Wait for the effect to become transparent and disappear
	}

	private IEnumerator HissSkill()
	{
		Debug.Log("Hiss Skill");
		// hissPrefab 로직 구현 예정
		yield return new WaitForSeconds(2.5f);  // Post skill delay
	}

	private void Phase1()
	{
		skillList.Clear();
		skillList.Add(new Skill(() => FishboneAttack(), 2.0f, 3));
		skillList.Add(new Skill(() => FishboneAttackCType(), 2.0f, 3));
		skillList.Add(new Skill(() => ScratchSkill(), 4.0f, 1));
	}

	private void Phase2()
	{
		skillList.Clear();
		skillList.Clear();
		skillList.Add(new Skill(() => FishboneAttack(), 2.0f, 3));
		skillList.Add(new Skill(() => FishboneAttackCType(), 2.0f, 3));
		skillList.Add(new Skill(() => ScratchSkill(), 4.0f, 1));
		skillList.Add(new Skill(() => FishboneAttack(), 2.0f, 3));
		skillList.Add(new Skill(() => FishboneAttackCType(), 2.0f, 3));
		skillList.Add(new Skill(() => ScratchSkill(), 4.0f, 1));
	}

	private void Phase3()
	{
		skillList.Clear();
		skillList.Add(new Skill(() => HissSkill(), 9.0f, 1));  // 필살기 먼저 실행
		skillList.Add(new Skill(() => FishboneAttack(), 2.0f, Random.Range(4, 6))); // 기본 공격 자주 실행
		skillList.Add(new Skill(() => ScratchSkill(), 4.0f, Random.Range(2, 3)));   // 첫 번째 스킬 보통 실행
		skillList.Add(new Skill(() => NyangPunchSkill(), 6.0f, 1));  // 두 번째 스킬 드물게 실행
		skillList.Add(new Skill(() => FishboneAttack(), 2.0f, Random.Range(3, 5))); // 기본 공격 자주 실행
		skillList.Add(new Skill(() => ScratchSkill(), 4.0f, 1));  // 첫 번째 스킬 보통 실행
		skillList.Add(new Skill(() => NyangPunchSkill(), 6.0f, Random.Range(1, 2))); // 두 번째 스킬 드물게 실행
		skillList.Add(new Skill(() => FishboneAttack(), 2.0f, Random.Range(4, 6))); // 기본 공격 자주 실행
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
