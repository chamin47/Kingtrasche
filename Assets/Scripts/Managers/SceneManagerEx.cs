using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx
{
    private Dictionary<Scene, string> sceneNameMap = new Dictionary<Scene, string>()
    {
        { Scene.Unknown, "UnknownScene" },
        { Scene.Title, "Title Scene" },
        { Scene.StageSelect, "StageScene" },
        { Scene.Game, "RunningScene" },
        { Scene.Boss, "BossScene1" },
        { Scene.Boss2, "BossScene2" },
        { Scene.Infinity, "InfinityRunningScene" },
        { Scene.Tutorial, "RunningTutorialScene" }
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
        CurrentScene?.Clear();
    }

    //string GetSceneName(Define.Scene type)
    //   {
    //       string name = System.Enum.GetName(typeof(Define.Scene), type);
    //       return name;
    //   }
}
