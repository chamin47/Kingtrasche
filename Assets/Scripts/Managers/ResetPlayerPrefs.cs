using UnityEditor;
using UnityEngine;

public class ResetPlayerPrefs : MonoBehaviour
{
    [MenuItem("Window/PlayerPrefs �ʱ�ȭ")]
    private static void ResetPrefs()
    {
        if (EditorUtility.DisplayDialog("���� ���̺� ���� ����", "���� ���� �Ͻðڽ��ϱ�?", "��", "�ƴϿ�"))
        {
            Debug.Log("���� �Ϸ�");
            PlayerPrefs.DeleteAll();
        }
    }

}
