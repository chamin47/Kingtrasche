using UnityEngine;

public class Meat : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            UI_GameScene.AddScore();
            Destroy(this.gameObject);
        }
    }
}
