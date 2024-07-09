using UnityEngine;

public class DogBone : MonoBehaviour
{
    [SerializeField] float boneSpeed = 5f;

    void Start()
    {

    }

    void Update()
    {
        // [todo] right�κ��� �÷��̾��� ���⿡ ���� �����ϱ�
        transform.Translate(Vector2.right.normalized * boneSpeed * Time.deltaTime);
    }

    public DogBone(float _boneSpeed) // pool�� ������ �Ѱ��ֱ� ����(�׽�Ʈ)
    {
        boneSpeed = _boneSpeed;
    }
}
