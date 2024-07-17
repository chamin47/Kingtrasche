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

        Camera cam = Camera.main;
        Vector3 CamCenter = new Vector3(0.5f, 0.5f, 0);
        Vector3 worldCenter = cam.ViewportToWorldPoint(CamCenter); // ī�޶� �߾���ǥ�� ������ǥ�� ��ȯ
        worldCenter.z = 0;

        for (int i = 0; i < numRows * numCols; i++)
        {
            GameObject go = Managers.Resource.Instantiate("Puzzle/Card", this.transform);

            // x, y ��ġ ����� 3 x 3 �׸��忡 �°� ����
            float x = (i % numCols) * 1.4f - 1.5f;  // �� �ε����� ����Ͽ� x ��ġ ����
            float y = (i / numCols) * 1.4f - 2.0f;  // �� �ε����� ����Ͽ� y ��ġ ����
            Vector3 cardPosition = new Vector3(worldCenter.x + x, worldCenter.y + y, 0);

            go.transform.position = cardPosition;

            go.GetComponent<Card>().Setting(arr[i], cardPosition.x, cardPosition.y); // ��ġ ������ Setting �Լ��� ����
        }

        CardGameManager.Instance.cardCount = arr.Length;
    }
}
