using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawSkill : MonoBehaviour
{
	private SpriteRenderer _renderer;
	private float effectDuration = 2f;
	private float transparencyDuration = 1f;
	private int damage = 1;

	private void Awake()
	{
		_renderer = GetComponent<SpriteRenderer>();
	}

	private void OnEnable()
	{
		StartCoroutine(HandleClawEffect());
	}

	private IEnumerator HandleClawEffect()
	{
		yield return new WaitForSeconds(effectDuration); // 2�� �� ������������ ����

		// �÷��̾�� �������� ������ ����
		Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 1f); // ������ �ִ� �÷��̾� ����
		foreach (var hitCollider in hitColliders)
		{
			if (hitCollider.CompareTag("Player"))
			{
				hitCollider.GetComponent<PlayerController>().TakeDamage(damage); // ������ �� ���� ����
			}
		}

		// ����Ʈ ����ȭ
		Color color = _renderer.color;
		for (float t = 0; t < transparencyDuration; t += Time.deltaTime / transparencyDuration)
		{
			color.a = Mathf.Lerp(1, 0, t);
			_renderer.color = color;
			yield return null;
		}

		Destroy(gameObject); // ����Ʈ ����
	}
}
