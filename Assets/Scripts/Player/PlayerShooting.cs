using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerShooting : MonoBehaviour
{
    public string bonePoolName = "BonePool";
    public Transform firePoint;
    private float fireRate = 0.2f;

    public float boneSpeed = 10f;

    public bool isFiring = true;

    private PlayerController playerController;


    void Start()
    {
        playerController = GetComponent<PlayerController>();

        if (SceneManager.GetActiveScene().name == "RunningTutorialScene")
        {
            isFiring = false;
        }
        if (isFiring)
        {
            StartCoroutine(AutoFire());
        }
    }

    void Update()
    {

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
        // 이름받아와서 생성
        GameObject bone = ObjectPoolManager.instance.GetObjectFromPool(bonePoolName, firePoint.position);
        // DogBone에 변수값 넣어줌
        DogBone dogBone = bone.GetComponent<DogBone>();
        dogBone.boneSpeed = boneSpeed;
        dogBone.bonePoolName = bonePoolName;
        dogBone.direction = playerController.returnDirection;

        bone.SetActive(true);

    }
}
