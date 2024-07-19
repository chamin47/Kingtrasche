using System.Collections;
using UnityEngine;

public class Card : MonoBehaviour
{
	public int idx = 0;

	[Header("Card State")]
	public GameObject front;
	public GameObject back;
	public SpriteRenderer frontImage;
	//카드 위치
	private float originalX;
	private float originalY;
	public float angle;

	private Animator anim;

	[Header("Effect")]
	public float rotationSpeed = 300f;
	public float maxRadius;
	public float orbitSpeed = 5f;
	private float currentRadius;

	// 랜덤 지연 시간을 추가
	private float delay;

	// 카드가 모두 원래 위치로 돌아갔는지 체크하는 플래그
	private static bool allCardsReturned;
	private static int cardsToReturn = 0;
	private static int cardsReturned = 0;

	void Start()
	{
		anim = GetComponent<Animator>();
		currentRadius = maxRadius;

		// 랜덤 지연 시간을 설정
		delay = Random.Range(0.1f, 0.5f);
		cardsToReturn++;
		StartCoroutine(StartRotationWithDelay());
	}

	IEnumerator StartRotationWithDelay()
	{
		yield return new WaitForSeconds(delay);

		// 카드의 원래 위치로 이동하는 코루틴 호출
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

		// firstCard가 비었다면.
		if (CardGameManager.Instance.firstCard == null)
		{
			// firstCard에 내 정보를 넘겨준다.
			CardGameManager.Instance.firstCard = this;
		}
		// firstCard가 비어있지 않다면.
		else
		{
			// secondCard에 내 정보를 넘겨준다.
			CardGameManager.Instance.secondCard = this;
			// Matched 함수를 호출해준다.
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
		float duration = 1.0f; // 이동하는 데 걸리는 시간 (초)
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

		// 마지막 위치 설정을 보장합니다.
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
		yield return new WaitForSeconds(0.5f); // 모든 카드가 원래 위치에 도달한 후 잠시 대기
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
		yield return new WaitForSeconds(1.0f); // 1초 동안 앞면을 보여줌
		front.SetActive(false);
		back.SetActive(true);
	}
}
