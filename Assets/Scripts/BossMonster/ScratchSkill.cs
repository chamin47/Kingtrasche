using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScratchSkill : MonoBehaviour
{
    public float speed = 5f; // «“ƒ˚±‚ ¿Ã∆Â∆Æ º”µµ
	private int damage = 1;
    private Rigidbody2D rb;
	public Vector2 direction;

	void Start()
    {
		rb = GetComponent<Rigidbody2D>();
		rb.velocity = new Vector2(direction.x, 0) * speed;
		Destroy(gameObject, 4f);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			collision.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
		}
	}
}
