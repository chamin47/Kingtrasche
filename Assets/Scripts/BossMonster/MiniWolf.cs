using System.Collections;
using UnityEngine;

public class MiniWolf : MonoBehaviour
{
	private float speed = 5f;
	private int damage = 1;
	private Vector3 moveDirection;

	public void Initialize(Vector3 direction)
	{
		moveDirection = new Vector3(direction.x, 0, 0).normalized;
		StartCoroutine(RunTowardsDirection());
	}

	private IEnumerator RunTowardsDirection()
	{
		while (true)
		{
			transform.position += moveDirection * speed * Time.deltaTime;
			yield return null;
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
			if (playerController != null)
			{
				playerController.TakeDamage(damage);
			}

			// ´Á´ë°¡ Ãæµ¹ ÈÄ »ç¶óÁü
			Destroy(gameObject);
		}
	}
}
