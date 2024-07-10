using System.Collections;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public string bonePoolName = "BonePool";
    public Transform firePoint;
    public float fireRate = 0.5f;

    public float boneSpeed = 10f;

    public bool isFiring = true;

    private PlayerController playerController;


    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (isFiring)
        {
            StartCoroutine(AutoFire());
        }
    }

    private IEnumerator AutoFire()
    {
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
        // DogBone�� ������ �־���
        DogBone dogBone = bone.GetComponent<DogBone>();
        dogBone.boneSpeed = boneSpeed;
        dogBone.bonePoolName = bonePoolName;
        dogBone.direction = playerController.returnDirection;

        bone.SetActive(true);

    }
}
