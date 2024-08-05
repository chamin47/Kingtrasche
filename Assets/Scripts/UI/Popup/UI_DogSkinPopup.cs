using GameBalance;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_DogSkinPopup : UI_Popup
{
    enum Buttons
    {
        // 강아지 종 선택 -> 각 첫번째 색의 강아지로 첫 화면
        AfghanHoundBtn,
        BloodHoundBtn,
        DalmationBtn,
        DobermanBtn,
        GreatDaneBtn,
        GreyHoundBtn,
        HuskyBtn,
        MountainDogBtn,
        ShepherdBtn,
        ShibaBtn,

        // 강아지 종 버튼 누르면 색깔별로 사진 가져와서 변경 -> ClickImage활성화 ->  Description변경
        FirstDogBtn,
        SecondDogBtn,
        ThirdDogBtn,

        PurchaseBtn, // 구매버튼 -> PurchaseImage, LockImage 비활성 -> AdoptImage활성 -> AdoptText 입양 중으로 변경
        SelectBtn // 스킨변경
    }

    enum Images
    {
        AfghanHoundBtn,
        BloodHoundBtn,
        DalmationBtn,
        DobermanBtn,
        GreatDaneBtn,
        GreyHoundBtn,
        HuskyBtn,
        MountainDogBtn,
        ShepherdBtn,
        ShibaBtn,

        // 색에 따라 이미지 찾아와서 변경
        FirstDogPicture,
        SecondDogPicture,
        ThirdDogPicture,

        FirstClickImage,
        SecondClickImage,
        ThirdClickImage,

        PurchaseImage,
        AdoptImage, // 입양 중인 이미지
        LockImage //입양 전 락 이미지 -> SelectBtn비활성
    }

    enum Texts
    {
        Description, //각 강아지마다 설명
        AdoptText, // 입양했을 시 텍스트 변경
        CostText, // 각 강아지마다의 입양 가격
    }

    private SkinData skinData1;
    private SkinData skinData2;
    private SkinData skinData3;
    private SkinData clickedSkin;

    public Animator animator;

    private Image originalBookmarkImage;
    private Sprite originalBookmarkSprite;

    private Image firstClickImage;
    private Image secondClickImage;
    private Image thirdClickImage;

    TMP_Text adoptText;
    Image purchaseImage;
    Image adoptImage;
    Image lockImage;
    Button selectBtn;
    Button PurchaseBtn;

    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        firstClickImage = GetImage((int)Images.FirstClickImage);
        secondClickImage = GetImage((int)Images.SecondClickImage);
        thirdClickImage = GetImage((int)Images.ThirdClickImage);

        adoptText = GetText((int)Texts.AdoptText);
        purchaseImage = GetImage((int)Images.PurchaseImage);
        adoptImage = GetImage((int)Images.AdoptImage);
        lockImage = GetImage((int)Images.LockImage);
        selectBtn = GetButton((int)Buttons.SelectBtn);
        PurchaseBtn = GetButton((int)Buttons.PurchaseBtn);
        // 초기는 MountainDogBernese
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        Bind<Button>(typeof(Buttons));
        Bind<TMP_Text>(typeof(Texts));
        Bind<Image>(typeof(Images));

        Get<Button>((int)Buttons.AfghanHoundBtn).gameObject.BindEvent(OnAfghanHoundBtnClicked);
        Get<Button>((int)Buttons.BloodHoundBtn).gameObject.BindEvent(OnBloodHoundBtnClicked);
        Get<Button>((int)Buttons.DalmationBtn).gameObject.BindEvent(OnDalmationBtnClicked); ;
        Get<Button>((int)Buttons.DobermanBtn).gameObject.BindEvent(OnDobermanBtnClicked); ;
        Get<Button>((int)Buttons.GreatDaneBtn).gameObject.BindEvent(OnGreatDaneBtnClicked); ;
        Get<Button>((int)Buttons.GreyHoundBtn).gameObject.BindEvent(OnGreyHoundBtnClicked); ;
        Get<Button>((int)Buttons.HuskyBtn).gameObject.BindEvent(OnHuskyBtnClicked); ;
        Get<Button>((int)Buttons.MountainDogBtn).gameObject.BindEvent(OnMountainDogBtnClicked); ;
        Get<Button>((int)Buttons.ShepherdBtn).gameObject.BindEvent(OnShepherdBtnClicked); ;
        Get<Button>((int)Buttons.ShibaBtn).gameObject.BindEvent(OnShibaBtnClicked); ;

        Get<Button>((int)Buttons.FirstDogBtn).gameObject.BindEvent(OnFirstDogBtnClicked);
        Get<Button>((int)Buttons.SecondDogBtn).gameObject.BindEvent(OnSecondDogBtnClicked);
        Get<Button>((int)Buttons.ThirdDogBtn).gameObject.BindEvent(OnThirdDogBtnClicked);

        Get<Button>((int)Buttons.PurchaseBtn).gameObject.BindEvent(OnPurchaseBtnClicked);
        Get<Button>((int)Buttons.SelectBtn).gameObject.BindEvent(OnSelectBtnClicked);

        return true;
    }

    private void OnAfghanHoundBtnClicked(PointerEventData eventData)
    {
        int skinID1 = 1;
        int skinID2 = 2;
        int skinID3 = 3;
        skinData1 = SkinData.SkinDataMap[skinID1];
        skinData2 = SkinData.SkinDataMap[skinID2];
        skinData3 = SkinData.SkinDataMap[skinID3];

        // 첫번째 강아지 기본 선택
        clickedSkin = skinData1;

        // 원래 북마크 이미지 변경
        RestoreOriginalBookmark();

        // 북마크
        ChangeBookmark(GetImage((int)Images.AfghanHoundBtn));

        // 이미지 셋팅
        SettingPicture(GetImage((int)Images.FirstDogPicture), skinData1.PicturePath);
        SettingPicture(GetImage((int)Images.SecondDogPicture), skinData2.PicturePath);
        SettingPicture(GetImage((int)Images.ThirdDogPicture), skinData3.PicturePath);

        firstClickImage.gameObject.SetActive(true);
        secondClickImage.gameObject.SetActive(false);
        thirdClickImage.gameObject.SetActive(false);

        // 설명셋팅
        SettingDescription(GetText((int)Texts.Description), skinData1.Description);
        // 비용셋팅
        SettingCost(GetText((int)Texts.CostText), skinData1.Cost);
        // 애니메이션 세팅
        SettingAnimation(skinData1.AnimationPath);
    }

    private void OnBloodHoundBtnClicked(PointerEventData eventData)
    {
        int skinID1 = 4;
        int skinID2 = 5;
        int skinID3 = 6;
        skinData1 = SkinData.SkinDataMap[skinID1];
        skinData2 = SkinData.SkinDataMap[skinID2];
        skinData3 = SkinData.SkinDataMap[skinID3];



        // 원래 북마크 이미지 변경
        RestoreOriginalBookmark();

        // 북마크
        ChangeBookmark(GetImage((int)Images.BloodHoundBtn));

        // 이미지 셋팅
        SettingPicture(GetImage((int)Images.FirstDogPicture), skinData1.PicturePath);
        SettingPicture(GetImage((int)Images.SecondDogPicture), skinData2.PicturePath);
        SettingPicture(GetImage((int)Images.ThirdDogPicture), skinData3.PicturePath);

        firstClickImage.gameObject.SetActive(true);
        secondClickImage.gameObject.SetActive(false);
        thirdClickImage.gameObject.SetActive(false);

        // 설명셋팅
        SettingDescription(GetText((int)Texts.Description), skinData1.Description);
        // 비용셋팅
        SettingCost(GetText((int)Texts.CostText), skinData1.Cost);
    }

    private void OnDalmationBtnClicked(PointerEventData eventData)
    {
        int skinID1 = 7;
        int skinID2 = 8;
        int skinID3 = 9;
        skinData1 = SkinData.SkinDataMap[skinID1];
        skinData2 = SkinData.SkinDataMap[skinID2];
        skinData3 = SkinData.SkinDataMap[skinID3];

        // 원래 북마크 이미지 변경
        RestoreOriginalBookmark();

        // 북마크
        ChangeBookmark(GetImage((int)Images.DalmationBtn));

        // 이미지 셋팅
        SettingPicture(GetImage((int)Images.FirstDogPicture), skinData1.PicturePath);
        SettingPicture(GetImage((int)Images.SecondDogPicture), skinData2.PicturePath);
        SettingPicture(GetImage((int)Images.ThirdDogPicture), skinData3.PicturePath);

        firstClickImage.gameObject.SetActive(true);
        secondClickImage.gameObject.SetActive(false);
        thirdClickImage.gameObject.SetActive(false);

        // 설명셋팅
        SettingDescription(GetText((int)Texts.Description), skinData1.Description);
        // 비용셋팅
        SettingCost(GetText((int)Texts.CostText), skinData1.Cost);
    }

    private void OnDobermanBtnClicked(PointerEventData eventData)
    {
        int skinID1 = 10;
        int skinID2 = 11;
        int skinID3 = 12;
        skinData1 = SkinData.SkinDataMap[skinID1];
        skinData2 = SkinData.SkinDataMap[skinID2];
        skinData3 = SkinData.SkinDataMap[skinID3];

        // 원래 북마크 이미지 변경
        RestoreOriginalBookmark();

        // 북마크
        ChangeBookmark(GetImage((int)Images.DobermanBtn));

        // 이미지 셋팅
        SettingPicture(GetImage((int)Images.FirstDogPicture), skinData1.PicturePath);
        SettingPicture(GetImage((int)Images.SecondDogPicture), skinData2.PicturePath);
        SettingPicture(GetImage((int)Images.ThirdDogPicture), skinData3.PicturePath);

        firstClickImage.gameObject.SetActive(true);
        secondClickImage.gameObject.SetActive(false);
        thirdClickImage.gameObject.SetActive(false);

        // 설명셋팅
        SettingDescription(GetText((int)Texts.Description), skinData1.Description);
        // 비용셋팅
        SettingCost(GetText((int)Texts.CostText), skinData1.Cost);
    }

    private void OnGreatDaneBtnClicked(PointerEventData eventData)
    {
        int skinID1 = 13;
        int skinID2 = 14;
        int skinID3 = 15;
        skinData1 = SkinData.SkinDataMap[skinID1];
        skinData2 = SkinData.SkinDataMap[skinID2];
        skinData3 = SkinData.SkinDataMap[skinID3];

        // 원래 북마크 이미지 변경
        RestoreOriginalBookmark();

        // 북마크
        ChangeBookmark(GetImage((int)Images.GreatDaneBtn));

        // 이미지 셋팅
        SettingPicture(GetImage((int)Images.FirstDogPicture), skinData1.PicturePath);
        SettingPicture(GetImage((int)Images.SecondDogPicture), skinData2.PicturePath);
        SettingPicture(GetImage((int)Images.ThirdDogPicture), skinData3.PicturePath);

        firstClickImage.gameObject.SetActive(true);
        secondClickImage.gameObject.SetActive(false);
        thirdClickImage.gameObject.SetActive(false);

        // 설명셋팅
        SettingDescription(GetText((int)Texts.Description), skinData1.Description);
        // 비용셋팅
        SettingCost(GetText((int)Texts.CostText), skinData1.Cost);
    }

    private void OnGreyHoundBtnClicked(PointerEventData eventData)
    {
        int skinID1 = 16;
        int skinID2 = 17;
        int skinID3 = 18;
        skinData1 = SkinData.SkinDataMap[skinID1];
        skinData2 = SkinData.SkinDataMap[skinID2];
        skinData3 = SkinData.SkinDataMap[skinID3];

        // 원래 북마크 이미지 변경
        RestoreOriginalBookmark();

        // 북마크
        ChangeBookmark(GetImage((int)Images.GreyHoundBtn));

        // 이미지 셋팅
        SettingPicture(GetImage((int)Images.FirstDogPicture), skinData1.PicturePath);
        SettingPicture(GetImage((int)Images.SecondDogPicture), skinData2.PicturePath);
        SettingPicture(GetImage((int)Images.ThirdDogPicture), skinData3.PicturePath);

        firstClickImage.gameObject.SetActive(true);
        secondClickImage.gameObject.SetActive(false);
        thirdClickImage.gameObject.SetActive(false);

        // 설명셋팅
        SettingDescription(GetText((int)Texts.Description), skinData1.Description);
        // 비용셋팅
        SettingCost(GetText((int)Texts.CostText), skinData1.Cost);
    }

    private void OnHuskyBtnClicked(PointerEventData eventData)
    {
        int skinID1 = 19;
        int skinID2 = 20;
        int skinID3 = 21;
        skinData1 = SkinData.SkinDataMap[skinID1];
        skinData2 = SkinData.SkinDataMap[skinID2];
        skinData3 = SkinData.SkinDataMap[skinID3];

        // 원래 북마크 이미지 변경
        RestoreOriginalBookmark();

        // 북마크
        ChangeBookmark(GetImage((int)Images.HuskyBtn));

        // 이미지 셋팅
        SettingPicture(GetImage((int)Images.FirstDogPicture), skinData1.PicturePath);
        SettingPicture(GetImage((int)Images.SecondDogPicture), skinData2.PicturePath);
        SettingPicture(GetImage((int)Images.ThirdDogPicture), skinData3.PicturePath);

        firstClickImage.gameObject.SetActive(true);
        secondClickImage.gameObject.SetActive(false);
        thirdClickImage.gameObject.SetActive(false);

        // 설명셋팅
        SettingDescription(GetText((int)Texts.Description), skinData1.Description);
        // 비용셋팅
        SettingCost(GetText((int)Texts.CostText), skinData1.Cost);
    }

    private void OnMountainDogBtnClicked(PointerEventData eventData)
    {
        int skinID1 = 22;
        int skinID2 = 23;
        int skinID3 = 24;
        skinData1 = SkinData.SkinDataMap[skinID1];
        skinData2 = SkinData.SkinDataMap[skinID2];
        skinData3 = SkinData.SkinDataMap[skinID3];

        // 원래 북마크 이미지 변경
        RestoreOriginalBookmark();

        // 북마크
        ChangeBookmark(GetImage((int)Images.MountainDogBtn));

        // 이미지 셋팅
        SettingPicture(GetImage((int)Images.FirstDogPicture), skinData1.PicturePath);
        SettingPicture(GetImage((int)Images.SecondDogPicture), skinData2.PicturePath);
        SettingPicture(GetImage((int)Images.ThirdDogPicture), skinData3.PicturePath);

        firstClickImage.gameObject.SetActive(true);
        secondClickImage.gameObject.SetActive(false);
        thirdClickImage.gameObject.SetActive(false);

        // 설명셋팅
        SettingDescription(GetText((int)Texts.Description), skinData1.Description);
        // 비용셋팅
        SettingCost(GetText((int)Texts.CostText), skinData1.Cost);
    }

    private void OnShepherdBtnClicked(PointerEventData eventData)
    {
        int skinID1 = 25;
        int skinID2 = 26;
        int skinID3 = 27;
        skinData1 = SkinData.SkinDataMap[skinID1];
        skinData2 = SkinData.SkinDataMap[skinID2];
        skinData3 = SkinData.SkinDataMap[skinID3];

        // 원래 북마크 이미지 변경
        RestoreOriginalBookmark();

        // 북마크
        ChangeBookmark(GetImage((int)Images.ShepherdBtn));

        // 이미지 셋팅
        SettingPicture(GetImage((int)Images.FirstDogPicture), skinData1.PicturePath);
        SettingPicture(GetImage((int)Images.SecondDogPicture), skinData2.PicturePath);
        SettingPicture(GetImage((int)Images.ThirdDogPicture), skinData3.PicturePath);

        firstClickImage.gameObject.SetActive(true);
        secondClickImage.gameObject.SetActive(false);
        thirdClickImage.gameObject.SetActive(false);

        // 설명셋팅
        SettingDescription(GetText((int)Texts.Description), skinData1.Description);
        // 비용셋팅
        SettingCost(GetText((int)Texts.CostText), skinData1.Cost);
    }

    private void OnShibaBtnClicked(PointerEventData eventData)
    {
        int skinID1 = 28;
        int skinID2 = 29;
        int skinID3 = 30;
        skinData1 = SkinData.SkinDataMap[skinID1];
        skinData2 = SkinData.SkinDataMap[skinID2];
        skinData3 = SkinData.SkinDataMap[skinID3];

        // 원래 북마크 이미지 변경
        RestoreOriginalBookmark();

        // 북마크
        ChangeBookmark(GetImage((int)Images.ShibaBtn));

        // 이미지 셋팅
        SettingPicture(GetImage((int)Images.FirstDogPicture), skinData1.PicturePath);
        SettingPicture(GetImage((int)Images.SecondDogPicture), skinData2.PicturePath);
        SettingPicture(GetImage((int)Images.ThirdDogPicture), skinData3.PicturePath);

        firstClickImage.gameObject.SetActive(true);
        secondClickImage.gameObject.SetActive(false);
        thirdClickImage.gameObject.SetActive(false);

        // 설명셋팅
        SettingDescription(GetText((int)Texts.Description), skinData1.Description);
        // 비용셋팅
        SettingCost(GetText((int)Texts.CostText), skinData1.Cost);
    }


    private void OnFirstDogBtnClicked(PointerEventData eventData)
    {
        clickedSkin = skinData1;

        SettingDescription(GetText((int)Texts.Description), skinData1.Description);
        SettingCost(GetText((int)Texts.CostText), skinData1.Cost);
        SettingAnimation(skinData1.AnimationPath);

        firstClickImage.gameObject.SetActive(true);
        secondClickImage.gameObject.SetActive(false);
        thirdClickImage.gameObject.SetActive(false);

        // 구매/입양 버튼 세팅
        if (IsAdopted(clickedSkin.SkinName) == false)
        {
            purchaseImage.gameObject.SetActive(true);
            adoptImage.gameObject.SetActive(false);
            lockImage.gameObject.SetActive(true);
        }
        else
        {
            purchaseImage.gameObject.SetActive(false);
            adoptImage.gameObject.SetActive(true);
            lockImage.gameObject.SetActive(false);
        }
    }

    private void OnSecondDogBtnClicked(PointerEventData eventData)
    {
        clickedSkin = skinData2;

        SettingDescription(GetText((int)Texts.Description), skinData2.Description);
        SettingCost(GetText((int)Texts.CostText), skinData2.Cost);
        SettingAnimation(skinData2.AnimationPath);

        firstClickImage.gameObject.SetActive(false);
        secondClickImage.gameObject.SetActive(true);
        thirdClickImage.gameObject.SetActive(false);

        // 구매/입양 버튼 세팅
        if (IsAdopted(clickedSkin.SkinName) == false)
        {
            purchaseImage.gameObject.SetActive(true);
            adoptImage.gameObject.SetActive(false);
            lockImage.gameObject.SetActive(true);
        }
        else
        {
            purchaseImage.gameObject.SetActive(false);
            adoptImage.gameObject.SetActive(true);
            lockImage.gameObject.SetActive(false);
        }
    }

    private void OnThirdDogBtnClicked(PointerEventData eventData)
    {
        clickedSkin = skinData3;

        SettingDescription(GetText((int)Texts.Description), skinData3.Description);
        SettingCost(GetText((int)Texts.CostText), skinData3.Cost);
        SettingAnimation(skinData3.AnimationPath);

        firstClickImage.gameObject.SetActive(false);
        secondClickImage.gameObject.SetActive(false);
        thirdClickImage.gameObject.SetActive(true);

        // 구매/입양 버튼 세팅
        if (IsAdopted(clickedSkin.SkinName) == false)
        {
            purchaseImage.gameObject.SetActive(true);
            adoptImage.gameObject.SetActive(false);
            lockImage.gameObject.SetActive(true);
        }
        else
        {
            purchaseImage.gameObject.SetActive(false);
            adoptImage.gameObject.SetActive(true);
            lockImage.gameObject.SetActive(false);
        }
    }


    private void OnPurchaseBtnClicked(PointerEventData eventData)
    {
        if (clickedSkin != null)
        {
            BuySkin(clickedSkin.Cost, clickedSkin.SkinName);
        }
    }

    private void OnSelectBtnClicked(PointerEventData eventData)
    {
        if (clickedSkin != null && IsAdopted(clickedSkin.SkinName) == true)
        {
            Debug.Log(clickedSkin.SkinName + "선택됨");
        }
    }


    private void ChangeBookmark(Image bookmark)
    {
        // 원래 이미지 저장
        originalBookmarkImage = bookmark;
        originalBookmarkSprite = bookmark.sprite;

        Sprite sprite = Resources.Load<Sprite>("Sprites/BookMark01");
        bookmark.sprite = sprite;
    }

    private void RestoreOriginalBookmark()
    {
        if (originalBookmarkImage != null && originalBookmarkSprite != null)
        {
            originalBookmarkImage.sprite = originalBookmarkSprite;
            originalBookmarkImage = null;
            originalBookmarkSprite = null;
            // 리셋
        }
    }

    private void SettingPicture(Image image, string path)
    {
        Sprite sprite = Resources.Load<Sprite>(path);

        image.sprite = sprite;
    }

    private void SettingDescription(TMP_Text text, string script)
    {
        text.text = script;
    }

    private void SettingCost(TMP_Text text, int cost)
    {
        text.text = cost.ToString("#,###");
    }

    private void SettingAnimation(string path)
    {
        AnimationClip clip = Resources.Load<AnimationClip>(path);

        AnimatorOverrideController overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        overrideController["ProgressDog"] = clip;

        animator.runtimeAnimatorController = overrideController;
    }


    private bool IsAdopted(string skinNsme)
    {
        int adopt = PlayerPrefs.GetInt(skinNsme);
        if (adopt == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void BuySkin(int cost, string skinName)
    {


        if (IsAdopted(skinName) == false) //입양 안된경우
        {
            int gold = PlayerPrefs.GetInt("Gold");
            if (gold >= cost)
            {
                gold -= cost;
                PlayerPrefs.SetInt("Gold", gold);
                PlayerPrefs.SetInt(skinName, 1);

                string adopt = "입양 중";
                adoptText.text = adopt;

                purchaseImage.gameObject.SetActive(false);
                adoptImage.gameObject.SetActive(true);
                lockImage.gameObject.SetActive(false);

                Debug.Log("압양 성공!");

                //PurchaseBtn.interactable = false;
                //selectBtn.interactable = true;
                // 버튼컴포넌트만 활성화/비활성화
            }
            else
            {
                // [todo] 구매불가 팝업 
                Debug.Log(" 고기가 부족합니다.");
            }
        }
        else // 입양 중인 경우
        {
            PlayerPrefs.SetInt(skinName, 0);
            // 버튼컴포넌트만 활성화/비활성화
            Debug.Log("이미 가족이 된 강아지입니다.");
        }
    }


}
