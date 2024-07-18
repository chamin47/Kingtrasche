using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	private float fishboneCooldown = 1.0f;
	private float scratchCooldown = 3.5f;
	private float punchCooldown = 5.2f;
	private float hissCooldown = 8.3f;
	private bool facingRight = true;

	private List<Skill> skillList = new List<Skill>();

	private void Start()
	{
		currentHealth = maxHealth;
		currentState = BossState.Phase1;

		// 초기 스킬 리스트 설정
		skillList.Add(new Skill(FishboneAttack, fishboneCooldown, 50));
		skillList.Add(new Skill(ScratchSkill, scratchCooldown, 30));
	}

	private void Update()
	{
		switch (currentState)
		{
			case BossState.Idle:
				break;
			case BossState.Phase1:
				UseWeightedRandomSkill();
				if (currentHealth <= maxHealth * 0.66f)
				{
					currentState = BossState.Phase2;
					Debug.Log("Phase 2 시작");
					skillList.Add(new Skill(PunchSkill, punchCooldown, 15));
				}
				break;
			case BossState.Phase2:
				UseWeightedRandomSkill();
				if (currentHealth <= maxHealth * 0.33f)
				{
					currentState = BossState.Phase3;
					Debug.Log("Phase 3 시작");
					skillList.Add(new Skill(HissSkill, hissCooldown, 5));
				}
				break;
			case BossState.Phase3:
				UseWeightedRandomSkill();
				break;
			case BossState.Dead:
				break;
		}
	}

	private void FishboneAttack()
	{
		Debug.Log("Fishbone Attack");
		fishbonePrefab = Managers.Resource.Load<GameObject>("BossSkill/CatBoss/Fishbone");
		Instantiate(fishbonePrefab, transform.position, Quaternion.identity);
	}

	private void ScratchSkill()
	{
		Debug.Log("Scratch Skill");
		scratchPrefab = Managers.Resource.Load<GameObject>("BossSkill/CatBoss/Scratch");
		Instantiate(scratchPrefab, transform.position, Quaternion.identity);
	}

	private void PunchSkill()
	{
		Debug.Log("Punch Skill");
		//punchPrefab = Managers.Resource.Load<GameObject>("BossSkill/CatBoss/Punch");
		//Instantiate(punchPrefab, transform.position, Quaternion.identity);
	}

	private void HissSkill()
	{
		Debug.Log("Hiss Skill");
		//hissPrefab = Managers.Resource.Load<GameObject>("BossSkill/CatBoss/Hiss");
		//Instantiate(hissPrefab, transform.position, Quaternion.identity);
	}


	private void UseWeightedRandomSkill()
	{
		List<Skill> availableSkills = new List<Skill>();
		foreach (var skill in skillList)
		{
			if (skill.CanUseSkill())
				availableSkills.Add(skill);
		}

		if (availableSkills.Count == 0)
			return;

		int totalWeight = 0;
		foreach (var skill in availableSkills)
		{
			totalWeight += skill.Weight;
		}

		int randomValue = UnityEngine.Random.Range(0, totalWeight);
		int currentWeight = 0;

		foreach (var skill in availableSkills)
		{
			currentWeight += skill.Weight;
			if (randomValue < currentWeight)
			{
				skill.Action.Invoke();
				break;
			}
		}
	}

	public void TakeDamage(int damage)
	{
		if (currentState == BossState.Dead)
			return;
		currentHealth -= damage;
		if (currentHealth <= 0f)
		{
			currentHealth = 0;
			currentState = BossState.Dead;
		}
	}

	private class Skill
	{
		public Action Action { get; }
		public int Weight { get; }
		private float Cooldown;
		private float Timer;

		public Skill(Action action, float cooldown, int weight)
		{
			Action = action;
			Cooldown = cooldown;
			Weight = weight;
		}

		public bool CanUseSkill()
		{
			if (Timer >= Cooldown)
			{
				Timer = 0; // 쿨다운을 충족하면 타이머 리셋
				return true;
			}
			Timer += Time.deltaTime; // 게임 업데이트 주기마다 타이머 업데이트
			return false;
		}
	}
}
