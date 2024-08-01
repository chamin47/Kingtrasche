using System.Collections;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public string bonePoolName = "BonePool";
    public Transform firePoint;
    private float fireRate = 0.2f;

    public float boneSpeed = 10f;

    public bool isFiring = false;

    private PlayerController playerController;


    void Start()
    {
        playerController = GetComponent<PlayerController>();

        if (isFiring == true)
        {
            StartCoroutine(AutoFire());
        }
        else
        {
            return;
        }
    }

    void Update()
    {

    }

    private IEnumerator AutoFire()
    {
        while (isFiring == true)
        {
            if (!playerController.isStunned)
            {
                ShootingBone();
            }

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
