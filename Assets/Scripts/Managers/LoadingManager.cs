using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    public static string nextScene;
    public Slider loadingBar;

    void Start()
    {
        StartCoroutine(LoadScene());
    }

    public static void LoadScene(string type)
    {
        nextScene = type;
        SceneManager.LoadScene("LoadingScene");
    }

    private IEnumerator LoadScene()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(nextScene);

        while (!operation.isDone)
        {
            if (loadingBar != null)
            {
                loadingBar.value = operation.progress;
            }

            yield return null;
        }
    }
}
