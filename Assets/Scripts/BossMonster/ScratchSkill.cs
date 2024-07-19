using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScratchSkill : MonoBehaviour
{
    public float speed = 5f; // «“ƒ˚±‚ ¿Ã∆Â∆Æ º”µµ
    private Rigidbody2D rb;
	public Vector2 direction;

	void Start()
    {
		rb = GetComponent<Rigidbody2D>();
		rb.velocity = new Vector2(direction.x, 0) * speed;
		Destroy(gameObject, 4f);
	}
}
