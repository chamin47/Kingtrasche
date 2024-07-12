using System.Linq;
using UnityEngine;

public class Board : MonoBehaviour
{
	void Start()
	{
		int[] arr = { 0, 0, 1, 1, 2, 2, 3, 3, 4 };
		arr = arr.OrderBy(x => Random.Range(0f, 3f)).ToArray();

		// �� ī�� ���� 6�� (3�� 3��)�� ����
		int numRows = 3;
		int numCols = 3;

		for (int i = 0; i < numRows * numCols; i++)
		{
			GameObject go = Managers.Resource.Instantiate("Puzzle/Card", this.transform);

			// x, y ��ġ ����� 3 x 3 �׸��忡 �°� ����
			float x = (i % numCols) * 1.4f - 1.5f;  // �� �ε����� ����Ͽ� x ��ġ ����
			float y = (i / numCols) * 1.4f - 2.0f;  // �� �ε����� ����Ͽ� y ��ġ ����
			go.transform.position = new Vector2(x, y);
			go.GetComponent<Card>().Setting(arr[i], x, y); // ��ġ ������ Setting �Լ��� ����
		}

		CardGameManager.Instance.cardCount = arr.Length;
	}
}
