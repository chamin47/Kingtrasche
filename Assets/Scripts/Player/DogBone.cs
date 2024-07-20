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

    public DogBone(float _boneSpeed) // pool에 데이터 넘겨주기 위함(테스트)
    {
        boneSpeed = _boneSpeed;
    }

    private bool IsOutOfScreen()
    {
        Vector3 insidePosition = Camera.main.WorldToViewportPoint(transform.position); //카메라 내의 포지션 값인지.
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
