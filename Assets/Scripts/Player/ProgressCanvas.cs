using UnityEngine;

public class ProgressCanvas : MonoBehaviour
{
    public Transform player;
    private Transform home;
    private RectTransform miniPlayer; //�÷��̾� ������
    private RectTransform miniHome; // �� ������

    private float playerStartZ;
    private float homeEndZ;
    private Vector3 initialMiniPlayerPosition;

    void Start()
    {
        home = transform.Find("EndMap");

        playerStartZ = player.position.z;
        homeEndZ = home.position.z;
        initialMiniPlayerPosition = miniPlayer.anchoredPosition;
    }


    void Update()
    {

    }
}
