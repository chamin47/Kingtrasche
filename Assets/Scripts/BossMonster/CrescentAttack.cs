using GameBalance;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrescentAttack : MonoBehaviour
{
	public float speed = 5f; // ÅºÈ¯ ¼Óµµ
	public int crescentDamage;
	Rigidbody2D rb;
	public Vector2 direction;
	private void Awake()
	{
		crescentDamage = SkillData.SkillDataMap[10201].Damage;
	}

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		rb.velocity = direction * speed;
		Destroy(gameObject, 4f);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			Destroy(this.gameObject);
			collision.gameObject.GetComponent<PlayerController>().TakeDamage(crescentDamage);
		}
	}
}
