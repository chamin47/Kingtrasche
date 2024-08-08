using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UI_Popup : UI_Base
{
    public override bool Init()
    {
        if (base.Init() == false)
            return false;
        Managers.UI.SetCanvas(gameObject, true);
        return true;
    }

    public virtual void ClosePopupUI()
    {
        Managers.Sound.Play("switch10", Sound.Effect);
        Managers.UI.ClosePopupUI(this);
    }

    protected void OnClickRetryButton(PointerEventData eventData)
    {
        if (Managers.Game.RunningPlayCount > 0)
        {
            Managers.Sound.Play("switch10", Sound.Effect);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Time.timeScale = 1.0f;
            Managers.Game.RunningPlayCount--;
            UI_GameScene.currentScore = 0;
        }
        else
        {
			Managers.UI.ShowPopupUI<UI_NoPlayPopup>();
		}
    }

    protected void OnClickBackStageButton(PointerEventData eventData)
    {
        Managers.Sound.Play("switch10", Sound.Effect);
        UI_GameScene.currentScore = 0;
        Managers.Scene.LoadScene(Scene.StageScene);
    }
}
