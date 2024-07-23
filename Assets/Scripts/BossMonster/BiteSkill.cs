using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiteSkill : MonoBehaviour
{
	public float delayBeforeCheck = 2.0f;
	public float activeDuration = 0.5f;
	public float destroyDelay = 1.0f;
	private int damage = 1;
	private Vector2 initialPosition;
	private SpriteRenderer _renderer;
	private BoxCollider2D boxCollider;

	// 박스 콜라이더 크기와 오프셋 설정
	public Vector2 boxColliderSize = new Vector2(1.9f, 1.7f);
	public Vector2 boxColliderOffset = Vector2.zero;

	// Start is called before the first frame update
	void Start()
    {
		initialPosition = transform.position;
		_renderer = GetComponent<SpriteRenderer>();

		// BoxCollider2D 동적으로 추가
		boxCollider = gameObject.AddComponent<BoxCollider2D>();
		boxCollider.isTrigger = true;
		boxCollider.enabled = false;

		// 박스 콜라이더 크기와 오프셋 설정
		boxCollider.size = boxColliderSize;
		boxCollider.offset = boxColliderOffset;

		StartCoroutine(CheckPlayerPosition());
	}

	private IEnumerator CheckPlayerPosition()
	{
		yield return new WaitForSeconds(delayBeforeCheck);

		// 알파 값을 완전히 불투명하게 설정
		SetAlpha(1f);
		// 박스 콜라이더 활성화
		boxCollider.enabled = true;

		// 일정 시간 동안 기다림 (알파 값이 1인 상태에서 박스 콜라이더 활성화)
		yield return new WaitForSeconds(activeDuration);

		// 알파 값을 다시 0.35로 되돌림
		SetAlpha(0.35f);
		// 박스 콜라이더 비활성화
		boxCollider.enabled = false;

		// 파괴 전까지 남은 시간 동안 기다림
		yield return new WaitForSeconds(destroyDelay - activeDuration);

		Destroy(gameObject);
	}

	private void SetAlpha(float alpha)
	{
		if (_renderer != null)
		{
			Color color = _renderer.color;
			color.a = alpha;
			_renderer.color = color;
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			Debug.Log("Player hit by Bite");
			// 데미지 로직 구현
			other.GetComponent<PlayerController>().TakeDamage(damage);
		}
	}
}
