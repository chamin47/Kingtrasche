using GameBalance;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_DogSkinPopup : UI_Popup
{
    enum Buttons
    {
        // ������ �� ���� -> �� ù��° ���� �������� ù ȭ��
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

        // ������ �� ��ư ������ ���򺰷� ���� �����ͼ� ���� -> ClickImageȰ��ȭ ->  Description����
        FirstDogBtn,
        SecondDogBtn,
        ThirdDogBtn,

        PurchaseBtn, // ���Ź�ư -> PurchaseImage, LockImage ��Ȱ�� -> AdoptImageȰ�� -> AdoptText �Ծ� ������ ����
        SelectBtn // ��Ų����
    }

    enum Images
    {
        // ���� ���� �̹��� ã�ƿͼ� ����
        FirstDogPicture,
        SecondDogPicture,
        ThirdDogPicture,

        FirstClickImage,
        SecondClickImage,
        ThirdClickImage,

        PurchaseImage,
        AdoptImage, // �Ծ� ���� �̹���
        LockImage //�Ծ� �� �� �̹��� -> SelectBtn��Ȱ��
    }

    enum Texts
    {
        Description, //�� ���������� ����
        AdoptText, // �Ծ����� �� �ؽ�Ʈ ����
        CostText, // �� ������������ �Ծ� ����
    }

    private SkinData skinData1;
    private SkinData skinData2;
    private SkinData skinData3;

    public Animator animator;

    private Image firstClickImage;
    private Image secondClickImage;
    private Image thirdClickImage;

    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        firstClickImage = GetImage((int)Images.FirstClickImage);
        secondClickImage = GetImage((int)Images.SecondClickImage);
        thirdClickImage = GetImage((int)Images.ThirdClickImage);
        // �ʱ�� MountainDogBernese
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

        Get<Button>((int)Buttons.PurchaseBtn);
        Get<Button>((int)Buttons.SelectBtn);

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

        // �̹��� ����
        SettingPicture(GetImage((int)Images.FirstDogPicture), skinData1.PicturePath);
        SettingPicture(GetImage((int)Images.SecondDogPicture), skinData2.PicturePath);
        SettingPicture(GetImage((int)Images.ThirdDogPicture), skinData3.PicturePath);

        firstClickImage.gameObject.SetActive(true);
        secondClickImage.gameObject.SetActive(false);
        thirdClickImage.gameObject.SetActive(false);

        // �������
        SettingDescription(GetText((int)Texts.Description), skinData1.Description);
        // ������
        SettingCost(GetText((int)Texts.CostText), skinData1.Cost);
        // �ִϸ��̼� ����
        SettingAnimation(skinData1.SkinName);
    }

    private void OnBloodHoundBtnClicked(PointerEventData eventData)
    {
        int skinID1 = 4;
        int skinID2 = 5;
        int skinID3 = 6;
        skinData1 = SkinData.SkinDataMap[skinID1];
        skinData2 = SkinData.SkinDataMap[skinID2];
        skinData3 = SkinData.SkinDataMap[skinID3];

        // �̹��� ����
        SettingPicture(GetImage((int)Images.FirstDogPicture), skinData1.PicturePath);
        SettingPicture(GetImage((int)Images.SecondDogPicture), skinData2.PicturePath);
        SettingPicture(GetImage((int)Images.ThirdDogPicture), skinData3.PicturePath);

        firstClickImage.gameObject.SetActive(true);
        secondClickImage.gameObject.SetActive(false);
        thirdClickImage.gameObject.SetActive(false);

        // �������
        SettingDescription(GetText((int)Texts.Description), skinData1.Description);
        // ������
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

        // �̹��� ����
        SettingPicture(GetImage((int)Images.FirstDogPicture), skinData1.PicturePath);
        SettingPicture(GetImage((int)Images.SecondDogPicture), skinData2.PicturePath);
        SettingPicture(GetImage((int)Images.ThirdDogPicture), skinData3.PicturePath);

        firstClickImage.gameObject.SetActive(true);
        secondClickImage.gameObject.SetActive(false);
        thirdClickImage.gameObject.SetActive(false);

        // �������
        SettingDescription(GetText((int)Texts.Description), skinData1.Description);
        // ������
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

        // �̹��� ����
        SettingPicture(GetImage((int)Images.FirstDogPicture), skinData1.PicturePath);
        SettingPicture(GetImage((int)Images.SecondDogPicture), skinData2.PicturePath);
        SettingPicture(GetImage((int)Images.ThirdDogPicture), skinData3.PicturePath);

        firstClickImage.gameObject.SetActive(true);
        secondClickImage.gameObject.SetActive(false);
        thirdClickImage.gameObject.SetActive(false);

        // �������
        SettingDescription(GetText((int)Texts.Description), skinData1.Description);
        // ������
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

        // �̹��� ����
        SettingPicture(GetImage((int)Images.FirstDogPicture), skinData1.PicturePath);
        SettingPicture(GetImage((int)Images.SecondDogPicture), skinData2.PicturePath);
        SettingPicture(GetImage((int)Images.ThirdDogPicture), skinData3.PicturePath);

        firstClickImage.gameObject.SetActive(true);
        secondClickImage.gameObject.SetActive(false);
        thirdClickImage.gameObject.SetActive(false);

        // �������
        SettingDescription(GetText((int)Texts.Description), skinData1.Description);
        // ������
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

        // �̹��� ����
        SettingPicture(GetImage((int)Images.FirstDogPicture), skinData1.PicturePath);
        SettingPicture(GetImage((int)Images.SecondDogPicture), skinData2.PicturePath);
        SettingPicture(GetImage((int)Images.ThirdDogPicture), skinData3.PicturePath);

        firstClickImage.gameObject.SetActive(true);
        secondClickImage.gameObject.SetActive(false);
        thirdClickImage.gameObject.SetActive(false);

        // �������
        SettingDescription(GetText((int)Texts.Description), skinData1.Description);
        // ������
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

        // �̹��� ����
        SettingPicture(GetImage((int)Images.FirstDogPicture), skinData1.PicturePath);
        SettingPicture(GetImage((int)Images.SecondDogPicture), skinData2.PicturePath);
        SettingPicture(GetImage((int)Images.ThirdDogPicture), skinData3.PicturePath);

        firstClickImage.gameObject.SetActive(true);
        secondClickImage.gameObject.SetActive(false);
        thirdClickImage.gameObject.SetActive(false);

        // �������
        SettingDescription(GetText((int)Texts.Description), skinData1.Description);
        // ������
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

        // �̹��� ����
        SettingPicture(GetImage((int)Images.FirstDogPicture), skinData1.PicturePath);
        SettingPicture(GetImage((int)Images.SecondDogPicture), skinData2.PicturePath);
        SettingPicture(GetImage((int)Images.ThirdDogPicture), skinData3.PicturePath);

        firstClickImage.gameObject.SetActive(true);
        secondClickImage.gameObject.SetActive(false);
        thirdClickImage.gameObject.SetActive(false);

        // �������
        SettingDescription(GetText((int)Texts.Description), skinData1.Description);
        // ������
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

        // �̹��� ����
        SettingPicture(GetImage((int)Images.FirstDogPicture), skinData1.PicturePath);
        SettingPicture(GetImage((int)Images.SecondDogPicture), skinData2.PicturePath);
        SettingPicture(GetImage((int)Images.ThirdDogPicture), skinData3.PicturePath);

        firstClickImage.gameObject.SetActive(true);
        secondClickImage.gameObject.SetActive(false);
        thirdClickImage.gameObject.SetActive(false);

        // �������
        SettingDescription(GetText((int)Texts.Description), skinData1.Description);
        // ������
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

        // �̹��� ����
        SettingPicture(GetImage((int)Images.FirstDogPicture), skinData1.PicturePath);
        SettingPicture(GetImage((int)Images.SecondDogPicture), skinData2.PicturePath);
        SettingPicture(GetImage((int)Images.ThirdDogPicture), skinData3.PicturePath);

        firstClickImage.gameObject.SetActive(true);
        secondClickImage.gameObject.SetActive(false);
        thirdClickImage.gameObject.SetActive(false);

        // �������
        SettingDescription(GetText((int)Texts.Description), skinData1.Description);
        // ������
        SettingCost(GetText((int)Texts.CostText), skinData1.Cost);
    }


    private void OnFirstDogBtnClicked(PointerEventData eventData)
    {
        SettingDescription(GetText((int)Texts.Description), skinData1.Description);
        SettingCost(GetText((int)Texts.CostText), skinData1.Cost);
        SettingAnimation(skinData1.SkinName);

        firstClickImage.gameObject.SetActive(true);
        secondClickImage.gameObject.SetActive(false);
        thirdClickImage.gameObject.SetActive(false);
    }

    private void OnSecondDogBtnClicked(PointerEventData eventData)
    {
        SettingDescription(GetText((int)Texts.Description), skinData2.Description);
        SettingCost(GetText((int)Texts.CostText), skinData2.Cost);
        SettingAnimation(skinData2.SkinName);

        firstClickImage.gameObject.SetActive(false);
        secondClickImage.gameObject.SetActive(true);
        thirdClickImage.gameObject.SetActive(false);
    }

    private void OnThirdDogBtnClicked(PointerEventData eventData)
    {
        SettingDescription(GetText((int)Texts.Description), skinData3.Description);
        SettingCost(GetText((int)Texts.CostText), skinData1.Cost);
        SettingAnimation(skinData3.SkinName);

        firstClickImage.gameObject.SetActive(false);
        secondClickImage.gameObject.SetActive(false);
        thirdClickImage.gameObject.SetActive(true);
    }


    private void SettingPicture(Image image, string path)
    {
        Sprite sprite = Resources.Load<Sprite>(path);

        image.sprite = sprite;
    }

    private void SettingDescription(TMP_Text text, string script)
    {
        text.text = script.ToString();
    }

    private void SettingCost(TMP_Text text, int cost)
    {
        text.text = cost.ToString("#,###");
    }

    private void SettingAnimation(string name)
    {
        AnimationClip clip = Resources.Load<AnimationClip>($"DogSkinAndAnimationClip/{name}/Running");

        AnimatorOverrideController overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        overrideController["ProgressDog"] = clip;

        animator.runtimeAnimatorController = overrideController;
    }
}
