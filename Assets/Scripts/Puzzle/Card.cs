using System.Collections;
using UnityEngine;

public class Card : MonoBehaviour
{
	public int idx = 0;

	[Header("Card State")]
	public GameObject front;
	public GameObject back;
	public SpriteRenderer frontImage;
	//ī�� ��ġ
	private float originalX;
	private float originalY;
	public float angle;

	private Animator anim;

	[Header("Effect")]
	public float rotationSpeed = 300f;
	public float maxRadius;
	public float orbitSpeed = 5f;
	private float currentRadius;

	// ���� ���� �ð��� �߰�
	private float delay;

	// ī�尡 ��� ���� ��ġ�� ���ư����� üũ�ϴ� �÷���
	private static bool allCardsReturned;
	private static int cardsToReturn = 0;
	private static int cardsReturned = 0;

	void Start()
	{
		anim = GetComponent<Animator>();
		currentRadius = maxRadius;

		// ���� ���� �ð��� ����
		delay = Random.Range(0.1f, 0.5f);
		cardsToReturn++;
		StartCoroutine(StartRotationWithDelay());
	}

	IEnumerator StartRotationWithDelay()
	{
		yield return new WaitForSeconds(delay);

		// ī���� ���� ��ġ�� �̵��ϴ� �ڷ�ƾ ȣ��
		StartCoroutine(CardOriginalLocationMoveCo());
	}

	public void Setting(int number, float x, float y)
	{
		idx = number;
		frontImage.sprite = Resources.Load<Sprite>($"Prefabs/Puzzle/Card{idx}");
		originalX = x;
		originalY = y;
	}

	public void OpenCard()
	{
		anim.SetBool("isOpen", true);
		front.SetActive(true);
		back.SetActive(false);

		// firstCard�� ����ٸ�.
		if (CardGameManager.Instance.firstCard == null)
		{
			// firstCard�� �� ������ �Ѱ��ش�.
			CardGameManager.Instance.firstCard = this;
		}
		// firstCard�� ������� �ʴٸ�.
		else
		{
			// secondCard�� �� ������ �Ѱ��ش�.
			CardGameManager.Instance.secondCard = this;
			// Matched �Լ��� ȣ�����ش�.
			CardGameManager.Instance.Matched();
		}
	}

	public void DestroyCard()
	{
		Destroy(gameObject, 1.0f);
	}

	public void CloseCard()
	{
		Invoke("CloseCardInvoke", 1.0f);
	}

	void CloseCardInvoke()
	{
		anim.SetBool("isOpen", false);
		front.SetActive(false);
		back.SetActive(true);
	}

	IEnumerator CardOriginalLocationMoveCo()
	{
		float duration = 1.0f; // �̵��ϴ� �� �ɸ��� �ð� (��)
		float elapsedTime = 0;

		Vector3 startPosition = transform.position;
		Vector3 targetPosition = new Vector3(originalX, originalY, 0);

		while (elapsedTime < duration)
		{
			elapsedTime += Time.deltaTime;
			float percent = elapsedTime / duration;
			transform.position = Vector3.Lerp(startPosition, targetPosition, percent);
			yield return null;
		}

		// ������ ��ġ ������ �����մϴ�.
		transform.position = targetPosition;

		cardsReturned++;
		if (cardsReturned >= cardsToReturn)
		{
			allCardsReturned = true;
			StartCoroutine(ShowAllCardsFace());
		}
	}

	IEnumerator ShowAllCardsFace()
	{
		yield return new WaitForSeconds(0.5f); // ��� ī�尡 ���� ��ġ�� ������ �� ��� ���
		foreach (Card card in FindObjectsOfType<Card>())
		{
			card.ShowFaceTemporarily();
		}
	}

	public void ShowFaceTemporarily()
	{
		StartCoroutine(ShowFaceTemporarilyCo());
	}

	IEnumerator ShowFaceTemporarilyCo()
	{
		front.SetActive(true);
		back.SetActive(false);
		yield return new WaitForSeconds(1.0f); // 1�� ���� �ո��� ������
		front.SetActive(false);
		back.SetActive(true);
	}
}
