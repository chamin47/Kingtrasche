using GameBalance;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_MissionPopup : UI_Popup
{
    enum Buttons
    {
        FirstMission,
        SecondMission,
        ThirdMission,
        FourthMission,
        FifthMission,

        RewardBtn,
        ExitBtn
    }

    enum Images
    {
        FirstMission,
        SecondMission,
        ThirdMission,
        FourthMission,
        FifthMission,

        RewardImage
    }

    enum Texts
    {
        MissionTitleText,
        MissionDescription,
        MissionGoalText,

        RewardText
    }

    private void Awake()
    {
        Init();
    }

    private void Start()
    {

    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        Bind<Button>(typeof(Buttons));
        Bind<Image>(typeof(Images));
        Bind<TMP_Text>(typeof(Texts));

        Get<Button>((int)Buttons.FirstMission).gameObject.BindEvent(OnFirstMissionBtnClicked);
        Get<Button>((int)Buttons.SecondMission).gameObject.BindEvent(OnSecondMissionBtnClicked);
        Get<Button>((int)Buttons.ThirdMission).gameObject.BindEvent(OnThirdMissionBtnClicked);
        Get<Button>((int)Buttons.FourthMission).gameObject.BindEvent(OnFourthMissionBtnClicked);
        Get<Button>((int)Buttons.FifthMission).gameObject.BindEvent(OnFifthMissionBtnClicked);

        Get<Button>((int)Buttons.RewardBtn).gameObject.BindEvent(OnRewardBtnClicked);
        Get<Button>((int)Buttons.ExitBtn).gameObject.BindEvent(OnExitBtnClicked);

        return true;
    }

    private void OnFirstMissionBtnClicked(PointerEventData eventData)
    {
        int missionNum = 1;

        MissionData missionData = MissionData.MissionDataMap[missionNum];
        Managers.Sound.Play("switch10", Sound.Effect);

    }

    private void OnSecondMissionBtnClicked(PointerEventData eventData)
    {

    }

    private void OnThirdMissionBtnClicked(PointerEventData eventData)
    {

    }

    private void OnFourthMissionBtnClicked(PointerEventData eventData)
    {

    }

    private void OnFifthMissionBtnClicked(PointerEventData eventData)
    {

    }


    private void OnRewardBtnClicked(PointerEventData eventData)
    {

    }

    private void OnExitBtnClicked(PointerEventData eventData)
    {

    }
}
