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
	SoundManager _sound = new SoundManager();
	UIManager _ui = new UIManager();
	ResourceManager _resource = new ResourceManager();
	SceneManagerEx _scene = new SceneManagerEx();

	public static GameManager Game { get { return Instance._game; } }
	public static SoundManager Sound { get { return Instance._sound; } }
	public static UIManager UI { get { return Instance._ui; } }
	public static ResourceManager Resource { get { return Instance._resource; } }
	public static SceneManagerEx Scene { get { return Instance._scene; } }

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
