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

	private void Start()
	{
		StartCoroutine(DestroyAfterDuration(3f));
	}

	private IEnumerator DestroyAfterDuration(float duration)
	{
		yield return new WaitForSeconds(duration);
		Destroy(gameObject);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			other.gameObject.GetComponent<PlayerController>().TakeDamage(1);
		}
	}

	private void Update()
	{
		transform.Translate(direction * Time.deltaTime * 5f);
	}
}