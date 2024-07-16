using UnityEngine;

public class KillingObstacle : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            //[todo] 플레이어 position.x 값을 고정시킨 후 0.5초 지나고 파괴
            Destroy(other.gameObject);
        }
    }
}
