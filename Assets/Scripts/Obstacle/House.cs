using UnityEngine;

public class House : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Managers.Sound.Play("harp strum 1", Sound.Effect);
            other.gameObject.SetActive(false);
            GameObject stageClear = Managers.Resource.Load<GameObject>("UI/Popup/UI_StageClearPopup");
            Instantiate(stageClear);
        }
    }

}
