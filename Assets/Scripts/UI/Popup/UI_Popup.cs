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
        Managers.Sound.Play("switch10", Sound.Effect);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1.0f;
        // 목줄 하나 감소하는 로직 필요
        UI_GameScene.currentScore = 0;
    }

    protected void OnClickBackStageButton(PointerEventData eventData)
    {
        Managers.Sound.Play("switch10", Sound.Effect);
        UI_GameScene.currentScore = 0;
        Managers.Scene.LoadScene(Scene.StageSelect);
    }
}
