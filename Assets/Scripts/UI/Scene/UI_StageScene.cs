using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_StageSelectScene : UI_Scene
{
    #region enum
    enum GameObjects
    {
    }

    enum Buttons
    {
        BackButton,
        Level1,
        Level2,
        Level3,
        Level4,
        Level5,
        Level6,
        Level7,
        Level8,
        Level9,
        Level10,
        Level11,
        Level12,
        Level13,
        Level14,
        Level15,
    }

    enum Texts
    {
        Level1Text,
        Level2Text,
        Level3Text,
        Level4Text,
        Level5Text,
        Level6Text,
        Level7Text,
        Level8Text,
        Level9Text,
        Level10Text,
        Level11Text,
        Level12Text,
        Level13Text,
        Level14Text,
        Level15Text,
    }
    #endregion

    public override bool Init()
    {
        if (base.Init() == false)
            return false;
        Bind<TMP_Text>(typeof(Texts));
        Bind<Button>(typeof(Buttons));
        #region Get<Button>
        Get<Button>((int)Buttons.BackButton).gameObject.BindEvent(OnClickBackButton);
        Get<Button>((int)Buttons.Level1).gameObject.BindEvent(OnClickLevel1Button);
        Get<Button>((int)Buttons.Level2).gameObject.BindEvent(OnClickLevel2Button);
        Get<Button>((int)Buttons.Level3).gameObject.BindEvent(OnClickLevel3Button);
        Get<Button>((int)Buttons.Level4).gameObject.BindEvent(OnClickLevel4Button);
        Get<Button>((int)Buttons.Level5).gameObject.BindEvent(OnClickLevel5Button);
        Get<Button>((int)Buttons.Level6).gameObject.BindEvent(OnClickLevel6Button);
        Get<Button>((int)Buttons.Level7).gameObject.BindEvent(OnClickLevel7Button);
        Get<Button>((int)Buttons.Level8).gameObject.BindEvent(OnClickLevel8Button);
        Get<Button>((int)Buttons.Level9).gameObject.BindEvent(OnClickLevel9Button);
        Get<Button>((int)Buttons.Level10).gameObject.BindEvent(OnClickLevel10Button);
        Get<Button>((int)Buttons.Level11).gameObject.BindEvent(OnClickLevel11Button);
        Get<Button>((int)Buttons.Level12).gameObject.BindEvent(OnClickLevel12Button);
        Get<Button>((int)Buttons.Level13).gameObject.BindEvent(OnClickLevel13Button);
        Get<Button>((int)Buttons.Level14).gameObject.BindEvent(OnClickLevel14Button);
        Get<Button>((int)Buttons.Level15).gameObject.BindEvent(OnClickLevel15Button);
        #endregion

        return true;
    }

    private void OnClickBackButton(PointerEventData eventData)
    {
        Managers.Sound.Play("switch10", Sound.Effect);
        Managers.Scene.LoadScene(Scene.LobbyScene);
    }

    #region LevelButton
    private void OnClickLevel1Button(PointerEventData eventData)
    {
        Managers.Sound.Play("switch10", Sound.Effect);
        Managers.Scene.LoadScene(Scene.RunningScene);
        PlayerPrefs.SetInt("StageNumber", 1);
    }
    private void OnClickLevel2Button(PointerEventData eventData)
    {
        Managers.Sound.Play("switch10", Sound.Effect);
        Managers.Scene.LoadScene(Scene.RunningScene);
        PlayerPrefs.SetInt("StageNumber", 2);
    }
    private void OnClickLevel3Button(PointerEventData eventData)
    {
        Managers.Sound.Play("switch10", Sound.Effect);
        Managers.Scene.LoadScene(Scene.RunningScene);
        PlayerPrefs.SetInt("StageNumber", 3);
    }
    private void OnClickLevel4Button(PointerEventData eventData)
    {
        Managers.Sound.Play("switch10", Sound.Effect);
        Managers.Scene.LoadScene(Scene.RunningScene);
        PlayerPrefs.SetInt("StageNumber", 4);
    }
    private void OnClickLevel5Button(PointerEventData eventData)
    {
        Managers.Sound.Play("switch10", Sound.Effect);
        Managers.Scene.LoadScene(Scene.BossScene1);
        PlayerPrefs.SetInt("StageNumber", 5);
    }
    private void OnClickLevel6Button(PointerEventData eventData)
    {
        Managers.Sound.Play("switch10", Sound.Effect);
        Managers.Scene.LoadScene(Scene.RunningScene);
        PlayerPrefs.SetInt("StageNumber", 6);
    }
    private void OnClickLevel7Button(PointerEventData eventData)
    {
        Managers.Sound.Play("switch10", Sound.Effect);
        Managers.Scene.LoadScene(Scene.RunningScene);
        PlayerPrefs.SetInt("StageNumber", 7);
    }
    private void OnClickLevel8Button(PointerEventData eventData)
    {
        Managers.Sound.Play("switch10", Sound.Effect);
        Managers.Scene.LoadScene(Scene.RunningScene);
        PlayerPrefs.SetInt("StageNumber", 8);
    }
    private void OnClickLevel9Button(PointerEventData eventData)
    {
        Managers.Sound.Play("switch10", Sound.Effect);
        Managers.Scene.LoadScene(Scene.RunningScene);
        PlayerPrefs.SetInt("StageNumber", 9);
    }
    private void OnClickLevel10Button(PointerEventData eventData)
    {
        Managers.Sound.Play("switch10", Sound.Effect);
        Managers.Scene.LoadScene(Scene.BossScene2);
        PlayerPrefs.SetInt("StageNumber", 10);
    }
    private void OnClickLevel11Button(PointerEventData eventData)
    {
        Managers.Sound.Play("switch10", Sound.Effect);
        Managers.Scene.LoadScene(Scene.RunningScene);
        PlayerPrefs.SetInt("StageNumber", 11);
    }
    private void OnClickLevel12Button(PointerEventData eventData)
    {
        Managers.Sound.Play("switch10", Sound.Effect);
        Managers.Scene.LoadScene(Scene.RunningScene);
        PlayerPrefs.SetInt("StageNumber", 12);
    }
    private void OnClickLevel13Button(PointerEventData eventData)
    {
        Managers.Sound.Play("switch10", Sound.Effect);
        Managers.Scene.LoadScene(Scene.RunningScene);
        PlayerPrefs.SetInt("StageNumber", 13);
    }
    private void OnClickLevel14Button(PointerEventData eventData)
    {
        Managers.Sound.Play("switch10", Sound.Effect);
        Managers.Scene.LoadScene(Scene.RunningScene);
        PlayerPrefs.SetInt("StageNumber", 14);
    }
    private void OnClickLevel15Button(PointerEventData eventData)
    {
        Managers.Sound.Play("switch10", Sound.Effect);
        Managers.Scene.LoadScene(Scene.BossScene3);
        PlayerPrefs.SetInt("StageNumber", 15);
    }
    #endregion
}
