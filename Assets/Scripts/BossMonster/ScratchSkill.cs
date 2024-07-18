using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScratchSkill : MonoBehaviour
{
    public float speed = 5f; // «“ƒ˚±‚ ¿Ã∆Â∆Æ º”µµ
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.right * speed;

        Destroy(gameObject, 4f);
    }
}
