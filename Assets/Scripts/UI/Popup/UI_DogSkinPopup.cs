using GameBalance;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_DogSkinPopup : UI_Popup
{
    #region Enum
    enum Buttons
    {
        // ������ �� ���� -> �� ù��° ���� �������� ù ȭ��
        MountainDogBtn,
        AfghanHoundBtn,
        BloodHoundBtn,
        DalmationBtn,
        DobermanBtn,
        GreatDaneBtn,
        GreyHoundBtn,
        HuskyBtn,
        ShepherdBtn,
        ShibaBtn,

        // ������ �� ��ư ������ ���򺰷� ���� �����ͼ� ���� -> ClickImageȰ��ȭ ->  Description����
        FirstDogBtn,
        SecondDogBtn,
        ThirdDogBtn,

        PurchaseBtn, // ���Ź�ư -> PurchaseImage, LockImage ��Ȱ�� -> AdoptImageȰ�� -> AdoptText �Ծ� ������ ����
        SelectBtn, // ��Ų����

        BackBtn
    }

    enum Images
    {
        MountainDogBtn,
        AfghanHoundBtn,
        BloodHoundBtn,
        DalmationBtn,
        DobermanBtn,
        GreatDaneBtn,
        GreyHoundBtn,
        HuskyBtn,
        ShepherdBtn,
        ShibaBtn,

        // ���� ���� �̹��� ã�ƿͼ� ����
        FirstDogPicture,
        SecondDogPicture,
        ThirdDogPicture,

        FirstClickImage,
        SecondClickImage,
        ThirdClickImage,

        PurchaseImage,
        AdoptImage, // �Ծ� ���� �̹���
        LockImage, //�Ծ� �� �� �̹��� -> SelectBtn��Ȱ��

        MirrorDog
    }

    enum Texts
    {
        Description, //�� ���������� ����
        AdoptText, // �Ծ����� �� �ؽ�Ʈ ����
        CostText, // �� ������������ �Ծ� ����
    }
    #endregion

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

        // �ʱ�� MountainDogBernese
        int skinID1 = 1;
        int skinID2 = 2;
        int skinID3 = 3;
        // �ϸ�ũ
        ChangeBookmark(GetImage((int)Images.MountainDogBtn));
        //����
        SkinSetting(skinID1, skinID2, skinID3);
        BaseUIScene();
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        Bind<Button>(typeof(Buttons));
        Bind<TMP_Text>(typeof(Texts));
        Bind<Image>(typeof(Images));

        Get<Button>((int)Buttons.MountainDogBtn).gameObject.BindEvent(OnMountainDogBtnClicked); ;
        Get<Button>((int)Buttons.AfghanHoundBtn).gameObject.BindEvent(OnAfghanHoundBtnClicked);
        Get<Button>((int)Buttons.BloodHoundBtn).gameObject.BindEvent(OnBloodHoundBtnClicked);
        Get<Button>((int)Buttons.DalmationBtn).gameObject.BindEvent(OnDalmationBtnClicked); ;
        Get<Button>((int)Buttons.DobermanBtn).gameObject.BindEvent(OnDobermanBtnClicked); ;
        Get<Button>((int)Buttons.GreatDaneBtn).gameObject.BindEvent(OnGreatDaneBtnClicked); ;
        Get<Button>((int)Buttons.GreyHoundBtn).gameObject.BindEvent(OnGreyHoundBtnClicked); ;
        Get<Button>((int)Buttons.HuskyBtn).gameObject.BindEvent(OnHuskyBtnClicked); ;
        Get<Button>((int)Buttons.ShepherdBtn).gameObject.BindEvent(OnShepherdBtnClicked); ;
        Get<Button>((int)Buttons.ShibaBtn).gameObject.BindEvent(OnShibaBtnClicked); ;

        Get<Button>((int)Buttons.FirstDogBtn).gameObject.BindEvent(OnFirstDogBtnClicked);
        Get<Button>((int)Buttons.SecondDogBtn).gameObject.BindEvent(OnSecondDogBtnClicked);
        Get<Button>((int)Buttons.ThirdDogBtn).gameObject.BindEvent(OnThirdDogBtnClicked);

        Get<Button>((int)Buttons.PurchaseBtn).gameObject.BindEvent(OnPurchaseBtnClicked);
        Get<Button>((int)Buttons.SelectBtn).gameObject.BindEvent(OnSelectBtnClicked);

        Get<Button>((int)Buttons.BackBtn).gameObject.BindEvent(OnBackBtnClicked);

        return true;
    }

    #region SkinBtn
    private void OnMountainDogBtnClicked(PointerEventData eventData)
    {
        int skinID1 = 1;
        int skinID2 = 2;
        int skinID3 = 3;

        Managers.Sound.Play("switch10", Sound.Effect);

        // ���� �ϸ�ũ �̹��� ����
        RestoreOriginalBookmark();
        // �ϸ�ũ
        ChangeBookmark(GetImage((int)Images.MountainDogBtn));
        //����
        SkinSetting(skinID1, skinID2, skinID3);
    }

    private void OnAfghanHoundBtnClicked(PointerEventData eventData)
    {
        int skinID1 = 4;
        int skinID2 = 5;
        int skinID3 = 6;

        Managers.Sound.Play("switch10", Sound.Effect);

        // ���� �ϸ�ũ �̹��� ����
        RestoreOriginalBookmark();
        // �ϸ�ũ
        ChangeBookmark(GetImage((int)Images.AfghanHoundBtn));
        //����
        SkinSetting(skinID1, skinID2, skinID3);
    }

    private void OnBloodHoundBtnClicked(PointerEventData eventData)
    {
        int skinID1 = 7;
        int skinID2 = 8;
        int skinID3 = 9;

        Managers.Sound.Play("switch10", Sound.Effect);

        // ���� �ϸ�ũ �̹��� ����
        RestoreOriginalBookmark();
        // �ϸ�ũ
        ChangeBookmark(GetImage((int)Images.BloodHoundBtn));
        //����
        SkinSetting(skinID1, skinID2, skinID3);
    }

    private void OnDalmationBtnClicked(PointerEventData eventData)
    {
        int skinID1 = 10;
        int skinID2 = 11;
        int skinID3 = 12;

        Managers.Sound.Play("switch10", Sound.Effect);

        // ���� �ϸ�ũ �̹��� ����
        RestoreOriginalBookmark();
        // �ϸ�ũ
        ChangeBookmark(GetImage((int)Images.DalmationBtn));
        //����
        SkinSetting(skinID1, skinID2, skinID3);
    }

    private void OnDobermanBtnClicked(PointerEventData eventData)
    {
        int skinID1 = 13;
        int skinID2 = 14;
        int skinID3 = 15;

        Managers.Sound.Play("switch10", Sound.Effect);

        // ���� �ϸ�ũ �̹��� ����
        RestoreOriginalBookmark();
        // �ϸ�ũ
        ChangeBookmark(GetImage((int)Images.DobermanBtn));
        //����
        SkinSetting(skinID1, skinID2, skinID3);
    }

    private void OnGreatDaneBtnClicked(PointerEventData eventData)
    {
        int skinID1 = 16;
        int skinID2 = 17;
        int skinID3 = 18;

        Managers.Sound.Play("switch10", Sound.Effect);

        // ���� �ϸ�ũ �̹��� ����
        RestoreOriginalBookmark();
        // �ϸ�ũ
        ChangeBookmark(GetImage((int)Images.GreatDaneBtn));
        //����
        SkinSetting(skinID1, skinID2, skinID3);
    }

    private void OnGreyHoundBtnClicked(PointerEventData eventData)
    {
        int skinID1 = 19;
        int skinID2 = 20;
        int skinID3 = 21;

        Managers.Sound.Play("switch10", Sound.Effect);

        // ���� �ϸ�ũ �̹��� ����
        RestoreOriginalBookmark();
        // �ϸ�ũ
        ChangeBookmark(GetImage((int)Images.GreyHoundBtn));
        //����
        SkinSetting(skinID1, skinID2, skinID3);
    }

    private void OnHuskyBtnClicked(PointerEventData eventData)
    {
        int skinID1 = 22;
        int skinID2 = 23;
        int skinID3 = 24;

        Managers.Sound.Play("switch10", Sound.Effect);

        // ���� �ϸ�ũ �̹��� ����
        RestoreOriginalBookmark();
        // �ϸ�ũ
        ChangeBookmark(GetImage((int)Images.HuskyBtn));
        //����
        SkinSetting(skinID1, skinID2, skinID3);
    }

    private void OnShepherdBtnClicked(PointerEventData eventData)
    {
        int skinID1 = 25;
        int skinID2 = 26;
        int skinID3 = 27;

        Managers.Sound.Play("switch10", Sound.Effect);

        // ���� �ϸ�ũ �̹��� ����
        RestoreOriginalBookmark();
        // �ϸ�ũ
        ChangeBookmark(GetImage((int)Images.ShepherdBtn));
        //����
        SkinSetting(skinID1, skinID2, skinID3);
    }

    private void OnShibaBtnClicked(PointerEventData eventData)
    {
        int skinID1 = 28;
        int skinID2 = 29;
        int skinID3 = 30;

        Managers.Sound.Play("switch10", Sound.Effect);

        // ���� �ϸ�ũ �̹��� ����
        RestoreOriginalBookmark();
        // �ϸ�ũ
        ChangeBookmark(GetImage((int)Images.ShibaBtn));
        //����
        SkinSetting(skinID1, skinID2, skinID3);
    }
    #endregion

    private void OnFirstDogBtnClicked(PointerEventData eventData)
    {
        Managers.Sound.Play("switch10", Sound.Effect);
        clickedSkin = skinData1;

        SettingDescription(GetText((int)Texts.Description), skinData1.Description);
        SettingCost(GetText((int)Texts.CostText), skinData1.Cost);
        SettingAnimation(skinData1.AnimationPath);

        firstClickImage.gameObject.SetActive(true);
        secondClickImage.gameObject.SetActive(false);
        thirdClickImage.gameObject.SetActive(false);

        // ����/�Ծ� ��ư ����
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
        Managers.Sound.Play("switch10", Sound.Effect);
        BaseUIScene();
    }

    private void OnThirdDogBtnClicked(PointerEventData eventData)
    {
        Managers.Sound.Play("switch10", Sound.Effect);
        clickedSkin = skinData3;

        SettingDescription(GetText((int)Texts.Description), skinData3.Description);
        SettingCost(GetText((int)Texts.CostText), skinData3.Cost);
        SettingAnimation(skinData3.AnimationPath);

        firstClickImage.gameObject.SetActive(false);
        secondClickImage.gameObject.SetActive(false);
        thirdClickImage.gameObject.SetActive(true);

        // ����/�Ծ� ��ư ����
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
            PlayerPrefs.SetString("Skin", clickedSkin.SkinName);
            PlayerSkinManager.Instance.ChangeSkin(clickedSkin.SkinName);
            Managers.UI.ShowPopupUI<UI_SkinSelectPopup>();

            // [todo]���õ� ������ "��å ��"���� �ؽ�Ʈ ����
        }
    }

    private void OnBackBtnClicked(PointerEventData eventData)
    {
        Managers.UI.ClosePopupUI(this);
    }


    private void ChangeBookmark(Image bookmark)
    {
        // ���� �̹��� ����
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
            // ����
        }
    }


    private void SkinSetting(int skinID1, int skinID2, int skinID3)
    {
        skinData1 = SkinData.SkinDataMap[skinID1];
        skinData2 = SkinData.SkinDataMap[skinID2];
        skinData3 = SkinData.SkinDataMap[skinID3];

        // ù��° ������ �⺻ ����
        clickedSkin = skinData1;

        // �̹��� ����
        SettingPicture(GetImage((int)Images.FirstDogPicture), skinData1.PicturePath);
        SettingPicture(GetImage((int)Images.SecondDogPicture), skinData2.PicturePath);
        SettingPicture(GetImage((int)Images.ThirdDogPicture), skinData3.PicturePath);

        firstClickImage.gameObject.SetActive(true);
        secondClickImage.gameObject.SetActive(false);
        thirdClickImage.gameObject.SetActive(false);

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

        // �������
        SettingDescription(GetText((int)Texts.Description), skinData1.Description);
        // ������
        SettingCost(GetText((int)Texts.CostText), skinData1.Cost);
        // �ִϸ��̼� ����
        SettingAnimation(skinData1.AnimationPath);
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

        if (clip == null)
        {
            Debug.LogWarning($"{path}Ŭ�� ����");
            return;
        }

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
        if (IsAdopted(skinName) == false) //�Ծ� �ȵȰ��
        {
            int gold = Managers.Game.Gold;  /*PlayerPrefs.GetInt("Gold");*/
            if (gold >= cost)
            {
                gold -= cost;
                Managers.Game.Gold = gold; /*PlayerPrefs.SetInt("Gold", gold);*/
                PlayerPrefs.SetInt(skinName, 1);
                PlayerPrefs.Save();

                string adopt = "�Ծ� ��";
                adoptText.text = adopt;

                purchaseImage.gameObject.SetActive(false);
                adoptImage.gameObject.SetActive(true);
                lockImage.gameObject.SetActive(false);

                Debug.Log("�Ծ� ����!");

                //PurchaseBtn.interactable = false;
                //selectBtn.interactable = true;
                // ��ư������Ʈ�� Ȱ��ȭ/��Ȱ��ȭ
            }
            else
            {
                Managers.UI.ShowPopupUI<UI_NoCoinPopup>();
                Debug.Log(" ��Ⱑ �����մϴ�.");
            }
        }
        else // �Ծ� ���� ���
        {
            // PlayerPrefs.SetInt(skinName, 0);
            // ��ư������Ʈ�� Ȱ��ȭ/��Ȱ��ȭ
            Debug.Log("�̹� ������ �� �������Դϴ�.");
        }
    }

    private void BaseUIScene()
    {
        clickedSkin = skinData2;

        SettingDescription(GetText((int)Texts.Description), skinData2.Description);
        SettingCost(GetText((int)Texts.CostText), skinData2.Cost);
        SettingAnimation(skinData2.AnimationPath);

        firstClickImage.gameObject.SetActive(false);
        secondClickImage.gameObject.SetActive(true);
        thirdClickImage.gameObject.SetActive(false);

        // ����/�Ծ� ��ư ����
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
}
