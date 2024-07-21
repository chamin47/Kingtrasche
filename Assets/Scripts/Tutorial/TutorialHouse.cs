using UnityEngine;

public class TutorialHouse : MonoBehaviour
{
    public GameObject TutorialUI;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.SetActive(false);
            TutorialUI.SetActive(false);
            GameObject TutorialClear = Managers.Resource.Load<GameObject>("UI/Popup/UI_TutorialClearPopup");
            Instantiate(TutorialClear);
        }
    }
}
