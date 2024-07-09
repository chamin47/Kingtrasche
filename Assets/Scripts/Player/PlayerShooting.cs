using System.Collections;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public string bonePoolName = "BonePool";
    public Transform firePoint;
    public float fireRate = 0.5f;

    private bool isFiring = false;


    void Start()
    {
        // [todo] 러닝씬에서는 스크립트 비활성화
    }

    void Update()
    {
        if (!isFiring)
        {
            StartCoroutine(AutoFire());
        }


    }

    private IEnumerator AutoFire()
    {
        isFiring = true;
        while (isFiring)
        {
            ShootingBone();
            yield return new WaitForSeconds(fireRate);
        }
    }

    private void ShootingBone()
    {
        // 이름받아와서 생성
        GameObject bone = ObjectPoolManager.instance.GetObjectFromPool(bonePoolName, firePoint.position);

        if (bone != null)
        {
            //DogBone dogBone = bone.GetComponent<DogBone>();
            bone.SetActive(true);
        }
    }
}
