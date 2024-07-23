using GameBalance;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CatBossController : MonoBehaviour, IBossController
{
	public BossState currentState = BossState.Idle;
	public int currentHealth { get; private set; }
	public int maxHealth { get; private set; }
	private GameObject fishbonePrefab;
	private GameObject scratchPrefab;
	private GameObject punchPrefab;
	private GameObject hissPrefab;
	private Transform player;
	private Animator anim;

	private List<Skill> skillList = new List<Skill>();
	private int currentSkillIndex = 0;
	private int currentSkillCount = 0;
	private float skillInterval = 0f; // 스킬 실행 간격
	private float skillTimer = 0.0f;
	private bool isSkillExecuting = false;
	private bool facingRight = true;

	public event Action OnHealthChanged;
	private SkillData Shot_fishbone;
	private SkillData Shot_fishboneCtype;
	private SkillData Scratch;
	private SkillData Catpunch;
	private SkillData Hissing;

	private void Awake()
	{
		Shot_fishbone = SkillData.SkillDataMap[10101];
		Shot_fishboneCtype = SkillData.SkillDataMap[10102];
		Scratch = SkillData.SkillDataMap[10103];
		Catpunch = SkillData.SkillDataMap[10104];
		Hissing = SkillData.SkillDataMap[10105];
		maxHealth = BossData.BossDataMap[101].bossHP;
		skillInterval = BossData.BossDataMap[101].attackInterval;
	}

	private void Start()
	{
		currentHealth = maxHealth;
		currentState = BossState.Phase1;
		player = GameObject.FindWithTag("Player").transform;
		anim = GetComponent<Animator>();

		// 스킬 프리팹 로드
		fishbonePrefab = Managers.Resource.Load<GameObject>("BossSkill/CatBoss/Fishbone");
		scratchPrefab = Managers.Resource.Load<GameObject>("BossSkill/CatBoss/Scratch");
		hissPrefab = Managers.Resource.Load<GameObject>("BossSkill/CatBoss/Hiss");

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
		yield return new WaitForSeconds(Shot_fishbone.preDelay);
		anim.SetTrigger("isAttack");
		Vector2 direction = player.position.x > transform.position.x ? Vector2.right : Vector2.left;
		GameObject fishbone = Instantiate(fishbonePrefab, transform.position, Quaternion.identity);
		fishbone.GetComponent<FishboneAttack>().direction = direction; // 발사 로직 예시
		yield return new WaitForSeconds(Shot_fishbone.postDelay);
	}

	private IEnumerator FishboneAttackCType()
	{
		yield return new WaitForSeconds(Shot_fishboneCtype.preDelay);
		Vector2 direction = facingRight ? Vector2.right : Vector2.left;

		anim.SetTrigger("isAttack");
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

		yield return new WaitForSeconds(Shot_fishboneCtype.postDelay);
	}

	private IEnumerator ScratchSkill()
	{
		yield return new WaitForSeconds(Scratch.preDelay);
		Debug.Log("Scratch Skill");
		anim.SetTrigger("isAttack");
		Vector2 direction = player.position.x > transform.position.x ? Vector2.right : Vector2.left;
		GameObject scratch = Instantiate(scratchPrefab, transform.position, Quaternion.identity);
		scratch.GetComponent<ScratchSkill>().direction = direction;
		yield return new WaitForSeconds(Scratch.postDelay);
	}

	private IEnumerator NyangPunchSkill()
	{
		yield return new WaitForSeconds(Catpunch.preDelay);

		for (int i = 0; i < 3; i++)
		{
			GameObject pawEffect = new GameObject("PawEffect");
			pawEffect.transform.position = player.position;
			SpriteRenderer renderer = pawEffect.AddComponent<SpriteRenderer>();
			renderer.sprite = Resources.Load<Sprite>("Sprites/PawEffectSprite");

			Color color = renderer.color;
			color.a = 0.35f;
			renderer.color = color;

			pawEffect.AddComponent<NyangPunchSkill>();

			yield return new WaitForSeconds(1.0f);
		}

		yield return new WaitForSeconds(Catpunch.postDelay);
	}

	private IEnumerator HissSkill()
	{
		yield return new WaitForSeconds(Hissing.preDelay);
		GameObject skullEffect = Instantiate(hissPrefab, Vector3.zero, Quaternion.identity);

		StartCoroutine(ShakeCamera(1.0f, 0.1f)); // 1초 동안 0.1의 강도로 화면 흔들림
		yield return new WaitForSeconds(1.0f);  // 이펙트가 1초 동안 화면에 나타남

		var renderer = skullEffect.GetComponent<SpriteRenderer>();
		renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 0.5f);

		// 스턴 효과 적용
		player.GetComponent<PlayerController>().ApplyStun(3.0f);

		yield return new WaitForSeconds(Hissing.postDelay);

		Destroy(skullEffect);
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
		skillList.Add(new Skill(() => FishboneAttack(), 0f, 3));
		skillList.Add(new Skill(() => FishboneAttackCType(), 0f, 3));
		skillList.Add(new Skill(() => ScratchSkill(), 0f, 1));
	}

	private void Phase2()
	{
		skillList.Clear();
		skillList.Add(new Skill(() => FishboneAttack(), 0f, 3));
		skillList.Add(new Skill(() => FishboneAttackCType(), 0f, 3));
		skillList.Add(new Skill(() => ScratchSkill(), 0f, 1));
		skillList.Add(new Skill(() => NyangPunchSkill(), 0f, 1));
	}

	private void Phase3()
	{
		skillList.Clear();

		List<Skill> possibleSkills = new List<Skill>
	    {
		    new Skill(() => HissSkill(), 0f, 1),
			new Skill(() => FishboneAttackCType(), 0f, 1),
		    new Skill(() => ScratchSkill(), 0f, 1),
		    new Skill(() => NyangPunchSkill(), 0f, 1),
			new Skill(() => FishboneAttack(), 0f, 1),
		};

		// 가능한 스킬 목록에서 랜덤하게 선택하여 스킬 리스트를 만듭니다.
		while (possibleSkills.Count > 0)
		{
			int index = Random.Range(0, possibleSkills.Count);
			skillList.Add(possibleSkills[index]);
			possibleSkills.RemoveAt(index);
		}
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
