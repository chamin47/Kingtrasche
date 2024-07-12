using UnityEngine;

public class Card : MonoBehaviour
{
	public int idx = 0;

	public GameObject front;
	public GameObject back;

	private Animator anim;

	public SpriteRenderer frontImage;

	void Update()
	{
		anim = GetComponent<Animator>();
	}

	public void Setting(int number)
	{
		idx = number;
		frontImage.sprite = Resources.Load<Sprite>($"Prefabs/Puzzle/Card{idx}");
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
			// Mached 함수를 호출해준다.
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
}
