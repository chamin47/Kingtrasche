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

	// �ڽ� �ݶ��̴� ũ��� ������ ����
	public Vector2 boxColliderSize = new Vector2(1.9f, 1.7f);
	public Vector2 boxColliderOffset = Vector2.zero;

	// Start is called before the first frame update
	void Start()
    {
		initialPosition = transform.position;
		_renderer = GetComponent<SpriteRenderer>();

		// BoxCollider2D �������� �߰�
		boxCollider = gameObject.AddComponent<BoxCollider2D>();
		boxCollider.isTrigger = true;
		boxCollider.enabled = false;

		// �ڽ� �ݶ��̴� ũ��� ������ ����
		boxCollider.size = boxColliderSize;
		boxCollider.offset = boxColliderOffset;

		StartCoroutine(CheckPlayerPosition());
	}

	private IEnumerator CheckPlayerPosition()
	{
		yield return new WaitForSeconds(delayBeforeCheck);

		// ���� ���� ������ �������ϰ� ����
		SetAlpha(1f);
		// �ڽ� �ݶ��̴� Ȱ��ȭ
		boxCollider.enabled = true;

		// ���� �ð� ���� ��ٸ� (���� ���� 1�� ���¿��� �ڽ� �ݶ��̴� Ȱ��ȭ)
		yield return new WaitForSeconds(activeDuration);

		// ���� ���� �ٽ� 0.35�� �ǵ���
		SetAlpha(0.35f);
		// �ڽ� �ݶ��̴� ��Ȱ��ȭ
		boxCollider.enabled = false;

		// �ı� ������ ���� �ð� ���� ��ٸ�
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
			// ������ ���� ����
			other.GetComponent<PlayerController>().TakeDamage(damage);
		}
	}
}
