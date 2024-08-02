using TMPro;
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

    private void Awake()
    {
        Init();
    }

    private void Start()
    {
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
        Get<Button>((int)Buttons.BloodHoundBtn);
        Get<Button>((int)Buttons.DalmationBtn);
        Get<Button>((int)Buttons.DobermanBtn);
        Get<Button>((int)Buttons.GreatDaneBtn);
        Get<Button>((int)Buttons.GreyHoundBtn);
        Get<Button>((int)Buttons.HuskyBtn);
        Get<Button>((int)Buttons.MountainDogBtn);
        Get<Button>((int)Buttons.ShepherdBtn);
        Get<Button>((int)Buttons.ShibaBtn);

        Get<Button>((int)Buttons.FirstDogBtn);
        Get<Button>((int)Buttons.SecondDogBtn);
        Get<Button>((int)Buttons.ThirdDogBtn);

        Get<Button>((int)Buttons.PurchaseBtn);
        Get<Button>((int)Buttons.SelectBtn);

        return true;
    }

    private void OnAfghanHoundBtnClicked(PointerEventData eventData)
    {

    }
}
