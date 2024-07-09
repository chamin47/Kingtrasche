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
        // [todo] ���׾������� ��ũ��Ʈ ��Ȱ��ȭ
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
        // �̸��޾ƿͼ� ����
        GameObject bone = ObjectPoolManager.instance.GetObjectFromPool(bonePoolName, firePoint.position);

        if (bone != null)
        {
            //DogBone dogBone = bone.GetComponent<DogBone>();
            bone.SetActive(true);
        }
    }
}
