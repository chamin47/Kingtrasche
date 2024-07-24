using GameBalance;
using System.Collections;
using UnityEngine;

public class ClawSkill : MonoBehaviour
{
	private SpriteRenderer _renderer;
	private float effectDuration = 2f; // 2초 동안 하늘에 떠 있음
	private float transparencyDuration = 1f; // 이펙트가 투명해지는 시간
	private int damage = 1;
	private Vector2 direction; // 처음 정해진 방향
	private bool isAttacking = false;
	private float speed = 5f;

	private void Awake()
	{
		_renderer = GetComponent<SpriteRenderer>();
		speed = SkillData.SkillDataMap[10203].speed;
	}

	private void OnEnable()
	{
		StartCoroutine(HandleClawEffect());
	}

	private IEnumerator HandleClawEffect()
	{
		yield return new WaitForSeconds(effectDuration); // 2초 대기

		isAttacking = true; // 공격 시작

		while (isAttacking)
		{
			// 톱니처럼 회전
			transform.Rotate(Vector3.forward, 360 * Time.deltaTime);

			// 처음 정해진 방향으로 이동
			transform.position += (Vector3)direction * speed * Time.deltaTime;

			yield return null;
		}
	}

	private IEnumerator FadeOutEffect()
	{
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

	public void Initialize(Vector2 initialDirection)
	{
		direction = initialDirection.normalized; // 방향을 정규화하여 설정
	}
}
