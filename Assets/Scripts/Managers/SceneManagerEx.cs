using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx
{
	public BaseScene CurrentScene { get { return GameObject.FindAnyObjectByType<BaseScene>(); } }

	public void LoadScene(Scene type)
	{
		Managers.Clear();

		SceneManager.LoadScene(GetSceneName(type));
	}

	public void Clear()
	{
		CurrentScene?.Clear();
	}

	string GetSceneName(Scene type)
	{
		string name = System.Enum.GetName(typeof(Scene), type);
		return name;
	}
}
