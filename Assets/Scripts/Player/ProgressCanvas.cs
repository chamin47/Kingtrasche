using TMPro;
using UnityEngine;

public class ProgressCanvas : MonoBehaviour
{
    public Transform player;
    public Transform home;
    public RectTransform miniPlayer; //플레이어 아이콘
    public RectTransform miniHome; // 집 아이콘

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
        float presentDistance = player.position.x - playerStartX; // 현재위치와 시작위치 차이
        float totalDistance = homeEndX - playerStartX; // 총 거리
        float progress = Mathf.Clamp01(presentDistance / totalDistance); //진척도 계산

        Vector3 newMiniPlayerPosition = Vector3.Lerp(initialMiniPlayerPosition, miniHome.anchoredPosition, progress); //새로운 위치
        miniPlayer.anchoredPosition = newMiniPlayerPosition;
    }

    void Setting(Vector3 _home)
    {
        homeEndX = _home.x;
    }
}
