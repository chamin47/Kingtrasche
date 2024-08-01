using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
	private Vector2 direction;

	public void SetDirection(Vector2 dir)
	{
		direction = dir;
	}
	

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			other.gameObject.GetComponent<PlayerController>().TakeDamage(1);
		}
	}
}