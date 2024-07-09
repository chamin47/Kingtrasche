using UnityEngine;

public class DogBone : MonoBehaviour
{
    public float boneSpeed = 10f;
    public Vector2 direction;

    void Start()
    {

    }

    void Update()
    {
        transform.Translate(direction.normalized * boneSpeed * Time.deltaTime);
    }

    public DogBone(float _boneSpeed) // pool�� ������ �Ѱ��ֱ� ����(�׽�Ʈ)
    {
        boneSpeed = _boneSpeed;
    }
}
