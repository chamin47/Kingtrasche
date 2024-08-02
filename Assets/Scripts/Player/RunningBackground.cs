using UnityEngine;
using UnityEngine.SceneManagement;

public class RunningBackground : MonoBehaviour
{
    private int stageNumber;
    public RectTransform morning;
    public RectTransform sunset;
    public RectTransform night;

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "RunningScene")
        {
            stageNumber = RunningMapManager.Instance.currentStage;
        }
        ChangeBackground();
    }

    private void ChangeBackground()
    {
        if (stageNumber == 1 || stageNumber == 2 || stageNumber == 3 || stageNumber == 4)
        {
            Managers.Sound.Play("ArcadeGameBGM#17", Sound.Bgm);
            sunset.gameObject.SetActive(false);
            night.gameObject.SetActive(false);

        }
        else if (stageNumber == 6 || stageNumber == 7 || stageNumber == 8 || stageNumber == 9)
        {
            Managers.Sound.Play("Happy walk", Sound.Bgm);
            morning.gameObject.SetActive(false);
            night.gameObject.SetActive(false);

        }
        else if (stageNumber == 11 || stageNumber == 12 || stageNumber == 13 || stageNumber == 14)
        {
            Managers.Sound.Play("one_0", Sound.Bgm);
            morning.gameObject.SetActive(false);
            sunset.gameObject.SetActive(false);

        }
        else
        {
            Managers.Sound.Play("SummerChallenge", Sound.Bgm);
            sunset.gameObject.SetActive(false);
            night.gameObject.SetActive(false);
        }
    }
}
