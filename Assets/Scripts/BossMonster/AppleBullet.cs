using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleBullet : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			other.GetComponent<PlayerController>().TakeDamage(1);
			Destroy(gameObject);
		}
	}
}
