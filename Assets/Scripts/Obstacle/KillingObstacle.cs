using UnityEngine;

public class KillingObstacle : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            //[todo] �÷��̾� position.x ���� ������Ų �� 0.5�� ������ �ı�
            Destroy(other.gameObject);
        }
    }
}
