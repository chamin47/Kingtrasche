using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance;
    static Managers Instance { get { Init(); return s_instance; } }

	// Contents
	GameManager _game = new GameManager();

	// Core
	UIManager _ui = new UIManager();
	ResourceManager _resource = new ResourceManager();

	public static GameManager Game { get { return Instance._game; } }
	public static UIManager UI { get { return Instance._ui; } }
	public static ResourceManager Resource { get { return Instance._resource; } }

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	private static void InitializeOnLoad()
	{
		Init();
	}

	static void Init()
	{
		if (s_instance == null)
		{
			GameObject go= GameObject.Find("@Managers");

			if (go == null)
			{
				go = new GameObject { name = "@Managers" };
				go.AddComponent<Managers>();
			}

			DontDestroyOnLoad(go);
			s_instance = go.GetComponent<Managers>();
		}
	}
}
