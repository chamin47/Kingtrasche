using UnityEngine;

public class TutorialButton : MonoBehaviour
{

    public void OnClickClearButton()
    {
        Managers.Scene.LoadScene(Scene.StageScene);
    }
}
