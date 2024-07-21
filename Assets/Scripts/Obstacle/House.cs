using UnityEngine;

public class House : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject TutorialClear = Managers.Resource.Load<GameObject>("UI/Popup/UI_StageClearPopup");
            GameObject go = Instantiate(TutorialClear);
        }
    }

}
