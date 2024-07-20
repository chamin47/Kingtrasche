using UnityEngine;

public class DogBone : MonoBehaviour
{
    public float boneSpeed;
    public Vector2 direction;
    public string bonePoolName;
    public int boneDamage = 5;

    void Start()
    {

    }

    void Update()
    {
        transform.Translate(direction.normalized * boneSpeed * Time.deltaTime);

        if (IsOutOfScreen())
        {
            ObjectPoolManager.instance.ReturnObjectToPool(bonePoolName, gameObject);
        }
    }

    public DogBone(float _boneSpeed) // pool�� ������ �Ѱ��ֱ� ����(�׽�Ʈ)
    {
        boneSpeed = _boneSpeed;
    }

    private bool IsOutOfScreen()
    {
        Vector3 insidePosition = Camera.main.WorldToViewportPoint(transform.position); //ī�޶� ���� ������ ������.
        return insidePosition.x < 0 || insidePosition.x > 1 || insidePosition.y < 0 || insidePosition.y > 1;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Boss"))
        {
            ObjectPoolManager.instance.ReturnObjectToPool(bonePoolName, gameObject);
            other.gameObject.GetComponent<CatBossController>().TakeDamage(boneDamage);
        }
    }
}
