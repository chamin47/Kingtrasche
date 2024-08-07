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

    enum MissionID
    {
        Story = 1,
        Boss = 2,
        Bee = 3,
        Puzzle = 4,
        Infinity = 5
    }
    #endregion

    private MissionData clickedMission;

    private TMP_Text missionTitleText;
    private TMP_Text missionDescription;
    private TMP_Text missionGoalText;
    private TMP_Text rewardText;

    private Image firstMission;
    private Image secondMission;
    private Image thirdMission;
    private Image fourthMission;
    private Image fifthMission;

    private Image rewardImage;
    private Sprite originalSprite;
    private Sprite changeSprite;

    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        missionTitleText = Get<TMP_Text>((int)Texts.MissionTitleText);
        missionDescription = Get<TMP_Text>((int)Texts.MissionDescription);
        missionGoalText = Get<TMP_Text>((int)Texts.MissionGoalText);
        rewardText = Get<TMP_Text>((int)Texts.RewardText);

        firstMission = Get<Image>((int)Images.FirstMission);
        secondMission = Get<Image>((int)Images.SecondMission);
        thirdMission = Get<Image>((int)Images.ThirdMission);
        fourthMission = Get<Image>((int)Images.FourthMission);
        fifthMission = Get<Image>((int)Images.FifthMission);

        rewardImage = Get<Image>((int)Images.RewardImage);
        originalSprite = Resources.Load<Sprite>("Sprites/GrayBtnFrame");
        changeSprite = Resources.Load<Sprite>("Sprites/GreenBtnFrame");

        SettingMissionState(1);
        SettingImage();
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
        SettingMissionState(missionNum);
    }

    private void OnThirdMissionBtnClicked(PointerEventData eventData)
    {
        int missionNum = 3;
        SettingMissionState(missionNum);
    }

    private void OnFourthMissionBtnClicked(PointerEventData eventData)
    {
        int missionNum = 4;
        SettingMissionState(missionNum);
    }

    private void OnFifthMissionBtnClicked(PointerEventData eventData)
    {
        int missionNum = 5;
        SettingMissionState(missionNum);
    }


    private void OnRewardBtnClicked(PointerEventData eventData)
    {
        Managers.Sound.Play("switch10", Sound.Effect);

        if (IsGoal(clickedMission.Complete) == true)
        {
            GetReward();
            //보상버튼 받으면 목표 달성 초기화
            PlayerPrefs.SetInt(clickedMission.Complete, 0);
            PlayerPrefs.Save();
            UpdateGoalAndReward();
            // 실시간 UI 업데이트
            SettingMissionState(clickedMission.MissionID);
            SettingImage();
        }
        else
        {
            return;
        }
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

        // 현재 달성중 및 목표치 업데이트
        SettingGoalText();
        // 미션완료 여부에 따라 타이틀 이미지 변경세팅
        SettingImage();
    }

    private bool IsGoal(string data)
    {
        int goalComplete = PlayerPrefs.GetInt(data);

        if (goalComplete == 0)
            return false;
        else
            return true;
    }

    private void SettingImage()
    {
        ChangeRewardImage(rewardImage, clickedMission.RewardImagePath);

        // 미션완료했으면 초록색 바. 그렇지 않으면 원래 이미지
        Image[] missionImage = { firstMission, secondMission, thirdMission, fourthMission, fifthMission };
        for (int i = 0; i < missionImage.Length; i++)
        {
            MissionData data = MissionData.MissionDataMap[i + 1];

            if (IsGoal(data.Complete) == true) // 완료됐으면 이미지 교체
            {
                missionImage[i].sprite = changeSprite;
            }
            else // 미션이 완료되지 않았으면 그대로
            {
                missionImage[i].sprite = originalSprite;
            }
        }
    }

    private void ChangeRewardImage(Image image, string path)
    {
        Sprite sprite = Resources.Load<Sprite>(path);
        rewardImage.sprite = sprite;
    }

    private void SettingGoalText()
    {
        Button rewardBtn = Get<Button>((int)Buttons.RewardBtn);
        int currentLevel = PlayerPrefs.GetInt(clickedMission.Level);
        int goalLevel = PlayerPrefs.GetInt(clickedMission.GoalLevel);
        int reward = PlayerPrefs.GetInt(clickedMission.Reward);
        string slash = " / ";
        string currentLevelstr = currentLevel.ToString("#,##0");
        string goalLevelStr = goalLevel.ToString("#,##0");

        missionGoalText.text = currentLevelstr + slash + goalLevelStr;
        rewardText.text = reward.ToString("#,##0");


        // 목표달성 여부에 따라 보상버튼 활성/비활성화
        if (currentLevel >= goalLevel)
        {
            // 목표달성
            PlayerPrefs.SetInt(clickedMission.Complete, 1);
            PlayerPrefs.Save();
            rewardBtn.interactable = true;
        }
        else if (currentLevel < goalLevel)
        {
            rewardBtn.interactable = false;
        }
        else if (goalLevel == clickedMission.MaxGoal)
        {
            rewardBtn.interactable = false;
        }
    }

    private void UpdateGoalAndReward()
    {
        int currentLevel = PlayerPrefs.GetInt(clickedMission.Level);
        int goalLevel = PlayerPrefs.GetInt(clickedMission.GoalLevel);
        int reward = PlayerPrefs.GetInt(clickedMission.Reward);

        MissionID missionId = (MissionID)clickedMission.MissionID;

        switch (missionId)
        {
            case MissionID.Story:
                if (goalLevel <= clickedMission.MaxGoal)
                {
                    goalLevel += clickedMission.Goal;
                    reward += clickedMission.IncreadeReward;
                    PlayerPrefs.SetInt(clickedMission.GoalLevel, goalLevel);
                    PlayerPrefs.SetInt(clickedMission.Reward, reward);
                    PlayerPrefs.Save();
                }
                else
                {
                    // 모든 미션을 달성했다 멍! 팝업제작 
                }
                break;

            case MissionID.Infinity:
                if (goalLevel < 100000)
                {
                    goalLevel += clickedMission.Goal;
                }
                else if (goalLevel < 200000)
                {
                    goalLevel += clickedMission.Goal * 2;
                }
                else if (goalLevel < clickedMission.MaxGoal)
                {
                    goalLevel += clickedMission.Goal * 3;
                }
                else if (goalLevel >= clickedMission.MaxGoal)
                {
                    goalLevel = clickedMission.MaxGoal;
                }
                else
                {
                    // 모든 미션을 달성했다 멍! 팝업제작 
                }
                reward += clickedMission.IncreadeReward;
                PlayerPrefs.SetInt(clickedMission.GoalLevel, goalLevel);
                PlayerPrefs.SetInt(clickedMission.Reward, reward);
                PlayerPrefs.Save();
                break;

            default:
                if (goalLevel < 100)
                {
                    goalLevel += clickedMission.Goal;
                }
                else if (goalLevel < 200)
                {
                    goalLevel += clickedMission.Goal * 2;
                }
                else if (goalLevel < clickedMission.MaxGoal)
                {
                    goalLevel += clickedMission.Goal * 3;
                }
                else if (goalLevel >= clickedMission.MaxGoal)
                {
                    goalLevel = clickedMission.MaxGoal;
                }
                else
                {
                    // 모든 미션을 달성했다 멍! 팝업제작 
                }
                reward += clickedMission.IncreadeReward;
                PlayerPrefs.SetInt(clickedMission.GoalLevel, goalLevel);
                PlayerPrefs.SetInt(clickedMission.Reward, reward);
                PlayerPrefs.Save();
                break;
        }
    }

    private void GetReward()
    {
        int coin = PlayerPrefs.GetInt("Gold");
        //int playTicket = PlayerPrefs.GetInt("RunningPlayCount");
        int playTicket = Managers.Game.RunningPlayCount;

        if (clickedMission.MissionID == 5)
        {
            playTicket += PlayerPrefs.GetInt(clickedMission.Reward);
            //PlayerPrefs.SetInt("RunningPlayCount", playTicket); 
            Managers.Game.RunningPlayCount = playTicket;
            PlayerPrefs.Save();
        }
        else
        {
            coin += PlayerPrefs.GetInt(clickedMission.Reward);
            PlayerPrefs.SetInt("Gold", coin);
            PlayerPrefs.Save();
        }
    }
}
