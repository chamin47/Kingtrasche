using GameBalance;
using System.Collections;
using UnityEngine;

public class ClawSkill : MonoBehaviour
{
	private SpriteRenderer _renderer;
	private float effectDuration = 2f; // 2�� ���� �ϴÿ� �� ����
	private float transparencyDuration = 1f; // ����Ʈ�� ���������� �ð�
	private int damage = 1;
	private Vector2 direction; // ó�� ������ ����
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
		yield return new WaitForSeconds(effectDuration); // 2�� ���

		isAttacking = true; // ���� ����

		while (isAttacking)
		{
			// ���ó�� ȸ��
			transform.Rotate(Vector3.forward, 360 * Time.deltaTime);

			// ó�� ������ �������� �̵�
			transform.position += (Vector3)direction * speed * Time.deltaTime;

			yield return null;
		}
	}

	private IEnumerator FadeOutEffect()
	{
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

	public void Initialize(Vector2 initialDirection)
	{
		direction = initialDirection.normalized; // ������ ����ȭ�Ͽ� ����
	}
}
