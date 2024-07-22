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
		yield return new WaitForSeconds(effectDuration); // 2초 뒤 도장형식으로 찍힘

		// 플레이어에게 데미지를 입히는 로직
		Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 1f); // 범위에 있는 플레이어 감지
		foreach (var hitCollider in hitColliders)
		{
			if (hitCollider.CompareTag("Player"))
			{
				hitCollider.GetComponent<PlayerController>().TakeDamage(damage); // 데미지 값 조정 가능
			}
		}

		// 이펙트 투명화
		Color color = _renderer.color;
		for (float t = 0; t < transparencyDuration; t += Time.deltaTime / transparencyDuration)
		{
			color.a = Mathf.Lerp(1, 0, t);
			_renderer.color = color;
			yield return null;
		}

		Destroy(gameObject); // 이펙트 삭제
	}
}
