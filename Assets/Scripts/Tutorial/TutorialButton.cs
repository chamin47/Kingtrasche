using UnityEngine;

public class TutorialButton : MonoBehaviour
{

    public void OnClickClearButton()
    {
        Managers.Sound.Play("switch10", Sound.Effect);
        Managers.Scene.LoadScene(Scene.StageScene);
    }
}
