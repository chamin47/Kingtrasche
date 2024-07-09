using UnityEngine;

public class DogBone : MonoBehaviour
{
    [SerializeField] float boneSpeed = 5f;

    void Start()
    {

    }

    void Update()
    {
        // [todo] right부분을 플레이어의 방향에 따라 변경하기
        transform.Translate(Vector2.right.normalized * boneSpeed * Time.deltaTime);
    }

    public DogBone(float _boneSpeed) // pool에 데이터 넘겨주기 위함(테스트)
    {
        boneSpeed = _boneSpeed;
    }
}
