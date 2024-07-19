using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NyangPunchSkill : MonoBehaviour
{
	public float delayBeforeCheck = 2.0f;
	public float destroyDelay = 1.0f;
	private Vector2 initialPosition;

	void Start()
	{
		initialPosition = transform.position;
		StartCoroutine(CheckPlayerPosition());
	}

	private IEnumerator CheckPlayerPosition()
	{
		yield return new WaitForSeconds(delayBeforeCheck);

		GameObject player = GameObject.FindWithTag("Player");
		if (player != null && (Vector2)player.transform.position == initialPosition)
		{
			Debug.Log("Player hit by NyangPunch");
			// Implement your damage logic here
		}

		// Destroy the effect after a delay
		Destroy(gameObject, destroyDelay);
	}
}