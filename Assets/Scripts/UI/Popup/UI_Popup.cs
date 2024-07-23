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
        Managers.UI.ClosePopupUI(this);
    }

    protected void OnClickRetryButton(PointerEventData eventData)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1.0f;
        // 목줄 하나 감소하는 로직 필요
        UI_GameScene.currentScore = 0;
    }

    protected void OnClickBackStageButton(PointerEventData eventData)
    {
        Managers.Scene.LoadScene(Scene.StageSelect);
    }
}
