using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishboneAttack : MonoBehaviour
{
    public float speed = 5f; // źȯ �ӵ�
    Rigidbody2D rb;

    void Start()
    {
		rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
        Destroy(gameObject, 4f);
	}
}
