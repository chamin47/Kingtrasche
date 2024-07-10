using UnityEngine;

public class CamereMove : MonoBehaviour
{
    public GameObject Player;

    void Start()
    {

    }

    void Update()
    {
        transform.position = new Vector3(Player.transform.position.x + 7.24f, 0, -10f);
    }
}
