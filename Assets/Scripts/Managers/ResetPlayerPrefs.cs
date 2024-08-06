using UnityEditor;
using UnityEngine;

public class ResetPlayerPrefs : MonoBehaviour
{
    [MenuItem("Window/PlayerPrefs 초기화")]
    private static void ResetPrefs()
    {
        if (EditorUtility.DisplayDialog("게임 세이브 정보 삭제", "정말 살제 하시겠습니까?", "네", "아니오"))
        {
            Debug.Log("삭제 완료");
            PlayerPrefs.DeleteAll();
        }
    }

}
