using UnityEngine;

public class CamereMove : MonoBehaviour
{
    public PlayerController controller;

    void Start()
    {

    }

    void Update()
    {
        transform.position += Vector3.right * controller.moveSpeed * Time.deltaTime;
    }
}
