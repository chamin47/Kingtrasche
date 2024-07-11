using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx
{
	private Dictionary<Define.Scene, string> sceneNameMap = new Dictionary<Define.Scene, string>()
	{
		{ Define.Scene.Unknown, "UnknownScene" },
		{ Define.Scene.Title, "Title Scene" },
		{ Define.Scene.Lobby, "Lobby Scene" },
		{ Define.Scene.Game, "HAY Scene" }
	};

	public BaseScene CurrentScene { get { return GameObject.FindAnyObjectByType<BaseScene>(); } }  

    public void LoadScene(Define.Scene type)
    {
		Managers.Clear();

        SceneManager.LoadScene(GetSceneName(type));
    }

	string GetSceneName(Define.Scene type)
	{
		if (sceneNameMap.TryGetValue(type, out string name))
		{
			return name;
		}
		return null;
	}

	public void Clear()
	{
		CurrentScene.Clear();
	}

	//string GetSceneName(Define.Scene type)
 //   {
 //       string name = System.Enum.GetName(typeof(Define.Scene), type);
 //       return name;
 //   }
}
