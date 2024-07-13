using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx
{
	private Dictionary<Scene, string> sceneNameMap = new Dictionary<Scene, string>()
	{
		{ Scene.Unknown, "UnknownScene" },
		{ Scene.Title, "Title Scene" },
		{ Scene.Stage, "HAY Scene" },
		{ Scene.Game, "HAY Scene" }
	};

	public BaseScene CurrentScene { get { return GameObject.FindAnyObjectByType<BaseScene>(); } }  

    public void LoadScene(Scene type)
    {
		Managers.Clear();

        SceneManager.LoadScene(GetSceneName(type));
    }

	string GetSceneName(Scene type)
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
