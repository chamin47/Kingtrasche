using TMPro;
using UnityEngine;

public class ProgressCanvas : MonoBehaviour
{
    public Transform player;
    public Transform home;
    public RectTransform miniPlayer; //�÷��̾� ������
    public RectTransform miniHome; // �� ������

    private float playerStartX;
    private float homeEndX;
    private Vector3 initialMiniPlayerPosition;

    public TMP_Text StageText;
    private string stage = "Stage : ";

    void Start()
    {
        player = Managers.Player.GetPlayer().transform;
        playerStartX = player.position.x;
        initialMiniPlayerPosition = miniPlayer.anchoredPosition;

        RunningMapManager.EndMapSpawn += Setting;

        StageText.text = stage + PlayerPrefs.GetInt("StageNumber").ToString();
    }

    void Update()
    {
        float presentDistance = player.position.x - playerStartX; // ������ġ�� ������ġ ����
        float totalDistance = homeEndX - playerStartX; // �� �Ÿ�
        float progress = Mathf.Clamp01(presentDistance / totalDistance); //��ô�� ���

        Vector3 newMiniPlayerPosition = Vector3.Lerp(initialMiniPlayerPosition, miniHome.anchoredPosition, progress); //���ο� ��ġ
        miniPlayer.anchoredPosition = newMiniPlayerPosition;
    }

    void Setting(Vector3 _home)
    {
        homeEndX = _home.x;
    }
}
