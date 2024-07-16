using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject Player;

    void Start()
    {

    }

    void Update()
    {
        if (Player != null)
        {
            transform.position = new Vector3(Player.transform.position.x + 6f, 0, -10f);
        }
    }
}
