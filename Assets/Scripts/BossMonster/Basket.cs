using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket : MonoBehaviour
{
	private Rigidbody2D rb;
	public float initialForce = 10f;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	private void Start()
	{
		// 45도 각도로 초기 힘을 줘서 발사
		float angle = 120f * Mathf.Deg2Rad;
		Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
		rb.AddForce(direction * initialForce, ForceMode2D.Impulse);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Ground"))
		{
			ReflectFromGround();
		}
		else if (other.CompareTag("Ceiling"))
		{
			ReflectFromCeiling();
		}
		else if (other.CompareTag("Player"))
		{
			other.gameObject.GetComponent<PlayerController>().TakeDamage(1);

			Destroy(gameObject);
		}
	}

	private void ReflectFromGround()
	{
		// 현재 속도 벡터
		Vector2 velocity = rb.velocity;

		// 아래쪽에서 반사 (y 축 방향 반전)
		Vector2 reflectedVelocity = new Vector2(velocity.x, -velocity.y);

		// 새로운 속도 적용
		rb.velocity = reflectedVelocity;
	}

	private void ReflectFromCeiling()
	{
		// 현재 속도 벡터
		Vector2 velocity = rb.velocity;

		// 천장에서 아래로 반사
		// 속도의 y 방향을 반전시킵니다.
		Vector2 reflectedVelocity = new Vector2(velocity.x, -velocity.y);

		// 새로운 속도 적용
		rb.velocity = reflectedVelocity;
	}
}
