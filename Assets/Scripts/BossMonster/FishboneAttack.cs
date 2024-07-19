using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishboneAttack : MonoBehaviour
{
    public float speed = 5f; // ÅºÈ¯ ¼Óµµ
    Rigidbody2D rb;
    public Vector2 direction;

    void Start()
    {
		rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction * speed;
        Destroy(gameObject, 4f);
	}
}
