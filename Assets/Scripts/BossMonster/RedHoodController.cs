using GameBalance;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RedHoodController : MonoBehaviour
{
	public BossState currentState = BossState.Idle;
	public int currentHealth { get; private set; }
	public int maxHealth { get; private set; }
	private GameObject basicBulletPrefab;
	private GameObject applePrefab;
	private GameObject basketPrefab;
	private GameObject laserPrefab;

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

	private void Awake()
	{
		maxHealth = 500;
	}

	private void Start()
	{
		currentHealth = maxHealth;
		currentState = BossState.Phase1;
		player = GameObject.FindWithTag("Player").transform;
		anim = GetComponent<Animator>();

		// 스킬 프리팹 로드
		basicBulletPrefab = Managers.Resource.Load<GameObject>("BossSkill/RedHoodBoss/BasicBullet");
		applePrefab = Managers.Resource.Load<GameObject>("BossSkill/RedHoodBoss/Apple");
		basketPrefab = Managers.Resource.Load<GameObject>("BossSkill/RedHoodBoss/Basket");
		laserPrefab = Managers.Resource.Load<GameObject>("BossSkill/RedHoodBoss/Laser");

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

	private IEnumerator ExecuteSkill(System.Func<IEnumerator> skill)
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
					Phase2();
				}
				break;
			case BossState.Phase2:
				if (currentHealth <= maxHealth * 0.33f)
				{
					currentState = BossState.Phase3;
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

	private IEnumerator BasicAttack()
	{
		for (int i = 0; i < 3; i++)
		{
			Vector2 direction = player.position.x > transform.position.x ? Vector2.right : Vector2.left;
			GameObject bullet = Instantiate(basicBulletPrefab, transform.position, Quaternion.identity);
			//bullet.GetComponent<Bullet>().Initialize(direction);
			yield return new WaitForSeconds(1f);
		}

		yield return new WaitForSeconds(1f);
		for (int i = 0; i < 3; i++)
		{
			Vector2 direction = new Vector2(Mathf.Cos(Mathf.PI / 3 * i), Mathf.Sin(Mathf.PI / 3 * i));
			GameObject bullet = Instantiate(basicBulletPrefab, transform.position, Quaternion.identity);
			//bullet.GetComponent<Bullet>().Initialize(direction);
			yield return new WaitForSeconds(1f);
		}
	}

	private IEnumerator BasicAttackCType()
	{
		yield return null;
	}

	private IEnumerator AppleDrop()
	{
		for (int i = 0; i < 3; i++)
		{
			// 빨간 사과
			Vector3 redApplePosition = new Vector3(Random.Range(-8f, 8f), 5f, 0);
			Instantiate(applePrefab, redApplePosition, Quaternion.identity);
			yield return new WaitForSeconds(2f);

			// 초록 사과
			Vector3 greenApplePosition = new Vector3(Random.Range(-8f, 8f), 5f, 0);
			Instantiate(applePrefab, greenApplePosition, Quaternion.identity);
			yield return new WaitForSeconds(2f);
		}
	}

	private IEnumerator BasketThrow()
	{
		for (int i = 0; i < 3; i++)
		{
			Vector3 basketPosition = new Vector3(Random.Range(-8f, 8f), 5f, 0);
			GameObject basket = Instantiate(basketPrefab, basketPosition, Quaternion.identity);
			basket.GetComponent<Rigidbody2D>().AddForce(new Vector2(1, -1) * 10f, ForceMode2D.Impulse);
			yield return new WaitForSeconds(0.5f);
		}
	}

	private IEnumerator LaserAttack()
	{
		anim.SetTrigger("isCharging");
		yield return new WaitForSeconds(3f);
		anim.SetTrigger("isAttacking");

		GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
		laser.transform.localScale = new Vector3(10f, 1f, 1f); // 예시 값

		//Camera.main.GetComponent<CameraShake>().Shake(1f, 0.2f);
		yield return new WaitForSeconds(3f);
		Destroy(laser);
	}

	private void Phase1()
	{
		skillList.Clear();
		skillList.Add(new Skill(() => BasicAttack(), 0f, 1));
		skillList.Add(new Skill(() => AppleDrop(), 0f, 1));
	}

	private void Phase2()
	{
		skillList.Clear();
		skillList.Add(new Skill(() => BasicAttack(), 0f, 1));
		skillList.Add(new Skill(() => AppleDrop(), 0f, 1));
		skillList.Add(new Skill(() => BasketThrow(), 0f, 1));
	}

	private void Phase3()
	{
		skillList.Clear();
		skillList.Add(new Skill(() => BasicAttack(), 0f, 1));
		skillList.Add(new Skill(() => AppleDrop(), 0f, 1));
		skillList.Add(new Skill(() => BasketThrow(), 0f, 1));
		skillList.Add(new Skill(() => LaserAttack(), 0f, 1));
	}

	public void TakeDamage(int damage)
	{
		if (currentState == BossState.Dead)
			return;

		currentHealth -= damage;
		OnHealthChanged?.Invoke();
		if (currentHealth <= 0)
		{
			currentHealth = 0;
			Destroy(gameObject);
		}
	}

	private class Skill
	{
		public System.Func<IEnumerator> Action { get; private set; }
		public float Cooldown { get; private set; }
		public int Count { get; private set; }

		public Skill(System.Func<IEnumerator> action, float cooldown, int count)
		{
			Action = action;
			Cooldown = cooldown;
			Count = count;
		}
	}
}
