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
		// 45�� ������ �ʱ� ���� �༭ �߻�
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
		// ���� �ӵ� ����
		Vector2 velocity = rb.velocity;

		// �Ʒ��ʿ��� �ݻ� (y �� ���� ����)
		Vector2 reflectedVelocity = new Vector2(velocity.x, -velocity.y);

		// ���ο� �ӵ� ����
		rb.velocity = reflectedVelocity;
	}

	private void ReflectFromCeiling()
	{
		// ���� �ӵ� ����
		Vector2 velocity = rb.velocity;

		// õ�忡�� �Ʒ��� �ݻ�
		// �ӵ��� y ������ ������ŵ�ϴ�.
		Vector2 reflectedVelocity = new Vector2(velocity.x, -velocity.y);

		// ���ο� �ӵ� ����
		rb.velocity = reflectedVelocity;
	}
}
