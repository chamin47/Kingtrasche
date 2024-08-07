using GameBalance;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class UI_Chapter3Popup : UI_Popup
{
	#region
	enum GameObjects
	{

	}
	enum Texts
	{

	}
	enum Buttons
	{
		BackButton,
		Level15,
		Level16,
		Level17,
		Level18,
		Level19,
		Level20,
		Level21,
	}
	enum Images
	{
		Background,
		Level15,
		Level16,
		Level17,
		Level18,
		Level19,
		Level20,
		Level21,
		OneStar1,
		OneStar2,
		OneStar3,
		TwoStar1,
		TwoStar2,
		TwoStar3,
		ThreeStar1,
		ThreeStar2,
		ThreeStar3,
		FourStar1,
		FourStar2,
		FourStar3,
		FiveStar1,
		FiveStar2,
		FiveStar3,
		SixStar1,
		SixStar2,
		SixStar3,
	}
	#endregion

	private void Awake()
	{
		Init();
	}

	public override bool Init()
	{
		if (base.Init() == false)
			return false;

		Bind<Button>(typeof(Buttons));
		Bind<TMP_Text>(typeof(Texts));
		Bind<Image>(typeof(Images));

		Get<Button>((int)Buttons.BackButton).gameObject.BindEvent(OnClickBackButton);
		Get<Button>((int)Buttons.Level15).gameObject.BindEvent(OnClickLevel15Button);
		Get<Button>((int)Buttons.Level16).gameObject.BindEvent(OnClickLevel16Button);
		Get<Button>((int)Buttons.Level17).gameObject.BindEvent(OnClickLevel17Button);
		Get<Button>((int)Buttons.Level18).gameObject.BindEvent(OnClickLevel18Button);
		Get<Button>((int)Buttons.Level19).gameObject.BindEvent(OnClickLevel19Button);
		Get<Button>((int)Buttons.Level20).gameObject.BindEvent(OnClickLevel20Button);
		Get<Button>((int)Buttons.Level21).gameObject.BindEvent(OnClickLevel21Button);

		return true;
	}

	private void OnClickBackButton(PointerEventData eventData)
	{
		Managers.UI.ClosePopupUI();
	}

	private void OnClickLevel15Button(PointerEventData eventData)
	{
		if (Managers.Game.RunningPlayCount > 0)
		{
			Managers.Sound.Play("switch10", Sound.Effect);
			Managers.Game.RunningPlayCount--; // ���� �÷��̱� ����
			Managers.Scene.LoadScene(Scene.RunningScene);
			PlayerPrefs.SetInt("StageNumber", 15);
			//PlayerPrefs.SetInt("StartFrom", 6);
		}
		else
		{
			Debug.Log("���� �÷��̱��� �����մϴ�.");
		}
	}

	private void OnClickLevel16Button(PointerEventData eventData)
	{
		if (Managers.Game.RunningPlayCount > 0)
		{
			Managers.Sound.Play("switch10", Sound.Effect);
			Managers.Game.RunningPlayCount--; // ���� �÷��̱� ����
			Managers.Scene.LoadScene(Scene.RunningScene);
			PlayerPrefs.SetInt("StageNumber", 16);
		}
		else
		{
			Debug.Log("���� �÷��̱��� �����մϴ�.");
		}
	}

	private void OnClickLevel17Button(PointerEventData eventData)
	{
		if (Managers.Game.RunningPlayCount > 0)
		{
			Managers.Sound.Play("switch10", Sound.Effect);
			Managers.Game.RunningPlayCount--; // ���� �÷��̱� ����
			Managers.Scene.LoadScene(Scene.RunningScene);
			PlayerPrefs.SetInt("StageNumber", 17);
		}
		else
		{
			Debug.Log("���� �÷��̱��� �����մϴ�.");
		}
	}
	private void OnClickLevel18Button(PointerEventData eventData)
	{
		if (Managers.Game.RunningPlayCount > 0)
		{
			Managers.Sound.Play("switch10", Sound.Effect);
			Managers.Game.RunningPlayCount--; // ���� �÷��̱� ����
			Managers.Scene.LoadScene(Scene.RunningScene);
			PlayerPrefs.SetInt("StageNumber", 18);
		}
		else
		{
			Debug.Log("���� �÷��̱��� �����մϴ�.");
		}
	}
	private void OnClickLevel19Button(PointerEventData eventData)
	{
		if (Managers.Game.RunningPlayCount > 0)
		{
			Managers.Sound.Play("switch10", Sound.Effect);
			Managers.Game.RunningPlayCount--; // ���� �÷��̱� ����
			Managers.Scene.LoadScene(Scene.RunningScene);
			PlayerPrefs.SetInt("StageNumber", 19);
		}
		else
		{
			Debug.Log("���� �÷��̱��� �����մϴ�.");
		}
	}
	private void OnClickLevel20Button(PointerEventData eventData)
	{
		if (Managers.Game.RunningPlayCount > 0)
		{
			Managers.Sound.Play("switch10", Sound.Effect);
			Managers.Game.RunningPlayCount--; // ���� �÷��̱� ����
			Managers.Scene.LoadScene(Scene.RunningScene);
			PlayerPrefs.SetInt("StageNumber", 20);
		}
		else
		{
			Debug.Log("���� �÷��̱��� �����մϴ�.");
		}
	}
	private void OnClickLevel21Button(PointerEventData eventData)
	{
		if (Managers.Game.RunningPlayCount > 0)
		{
			Managers.Sound.Play("switch10", Sound.Effect);
			Managers.Game.RunningPlayCount--; // ���� �÷��̱� ����
			Managers.Scene.LoadScene(Scene.BossScene3);
			PlayerPrefs.SetInt("StageNumber", 21);
		}
		else
		{
			Debug.Log("���� �÷��̱��� �����մϴ�.");
		}
	}
}
