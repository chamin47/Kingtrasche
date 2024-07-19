using UnityEngine;

public class FishboneAttack : MonoBehaviour
{
    public float speed = 5f; // ÅºÈ¯ ¼Óµµ
    public int boneDamage = 1;
    Rigidbody2D rb;
    public Vector2 direction;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction * speed;
        Destroy(gameObject, 4f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(boneDamage);
        }
    }
}
