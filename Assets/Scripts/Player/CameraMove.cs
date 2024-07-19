using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private GameObject Player;

    void Start()
    {
        Player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        if (Player != null)
        {
            transform.position = new Vector3(Player.transform.position.x + 6f, 0, -10f);
        }
    }
}
