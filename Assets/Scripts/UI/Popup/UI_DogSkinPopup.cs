using TMPro;
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

    private void Awake()
    {
        Init();
    }

    private void Start()
    {
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
