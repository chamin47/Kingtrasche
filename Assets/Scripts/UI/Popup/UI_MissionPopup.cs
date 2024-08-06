using GameBalance;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_MissionPopup : UI_Popup
{
    #region Enum
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
    #endregion

    private MissionData clickedMission;

    private TMP_Text missionTitleText;
    private TMP_Text missionDescription;
    private TMP_Text missionGoalText;

    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        missionTitleText = Get<TMP_Text>((int)Texts.MissionTitleText);
        missionDescription = Get<TMP_Text>((int)Texts.MissionDescription);
        missionGoalText = Get<TMP_Text>((int)Texts.RewardText);
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
        SettingMissionState(missionNum);
    }

    private void OnSecondMissionBtnClicked(PointerEventData eventData)
    {
        int missionNum = 2;
    }

    private void OnThirdMissionBtnClicked(PointerEventData eventData)
    {
        int missionNum = 3;
    }

    private void OnFourthMissionBtnClicked(PointerEventData eventData)
    {
        int missionNum = 4;
    }

    private void OnFifthMissionBtnClicked(PointerEventData eventData)
    {
        int missionNum = 5;
    }


    private void OnRewardBtnClicked(PointerEventData eventData)
    {
        Managers.Sound.Play("switch10", Sound.Effect);
    }

    private void OnExitBtnClicked(PointerEventData eventData)
    {
        Managers.Sound.Play("switch10", Sound.Effect);
        Managers.UI.ClosePopupUI(this);
    }


    private void SettingMissionState(int missionNum)
    {
        Managers.Sound.Play("switch10", Sound.Effect);

        MissionData missionData = MissionData.MissionDataMap[missionNum];
        clickedMission = missionData;

        missionTitleText.text = missionData.MissionTitle;
        missionDescription.text = missionData.Description;

    }

    private bool IsGoal(string data)
    {
        int goalComplete = PlayerPrefs.GetInt(data);

        if (goalComplete == 0)
            return false;
        else
            return true;
    }

    private void ChangeTitleImage()
    {
        // 미션완료했으면 초록색 바. 그렇지 않으면 원래 이미지

    }
}
