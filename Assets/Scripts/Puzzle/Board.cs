using System.Linq;
using UnityEngine;

public class Board : MonoBehaviour
{
	void Start()
	{
		int[] arr = { 0, 0, 1, 1, 2, 2, 3, 3, 4 };
		arr = arr.OrderBy(x => Random.Range(0f, 3f)).ToArray();

		// 총 카드 수를 6개 (3행 3열)로 변경
		int numRows = 3;
		int numCols = 3;

		for (int i = 0; i < numRows * numCols; i++)
		{
			GameObject go = Managers.Resource.Instantiate("Puzzle/Card", this.transform);

			// x, y 위치 계산을 3 x 3 그리드에 맞게 조정
			float x = (i % numCols) * 1.4f - 1.5f;  // 열 인덱스를 계산하여 x 위치 설정
			float y = (i / numCols) * 1.4f - 2.0f;  // 행 인덱스를 계산하여 y 위치 설정
			go.transform.position = new Vector2(x, y);
			go.GetComponent<Card>().Setting(arr[i], x, y); // 위치 정보를 Setting 함수에 전달
		}

		CardGameManager.Instance.cardCount = arr.Length;
	}
}
