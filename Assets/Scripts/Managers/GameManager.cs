using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
	public void GameOver()
	{
		Managers.UI.ShowPopupUI<UI_GameOverPopup>();
		Time.timeScale = 0;
	}
}
