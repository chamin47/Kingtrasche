using UGS;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance;
    static Managers Instance { get { Init(); return s_instance; } }

    // Contents
    GameManager _game = new GameManager();
    DialogueManager _dialogue = new DialogueManager();
    TimeManager _time;

    // Core
    SoundManager _sound = new SoundManager();
    UIManager _ui = new UIManager();
    ResourceManager _resource = new ResourceManager();
    SceneManagerEx _scene = new SceneManagerEx();

    //Player
    PlayerManager _player;

    public static GameManager Game { get { return Instance._game; } }
    public static TimeManager Time { get { return Instance._time; } }
    public static SoundManager Sound { get { return Instance._sound; } }
    public static UIManager UI { get { return Instance._ui; } }
    public static ResourceManager Resource { get { return Instance._resource; } }
    public static SceneManagerEx Scene { get { return Instance._scene; } }
    public static PlayerManager Player { get { return Instance._player; } }
    public static DialogueManager Dialogue { get { return Instance._dialogue; } }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void InitializeOnLoad()
    {
        UnityGoogleSheet.LoadAllData();
        Init();
    }

    static void Init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");

            if (go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }

            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();

            s_instance._game.Init();
            s_instance._sound.Init();
            s_instance._dialogue.Init();
            s_instance._time = go.AddComponent<TimeManager>();

            s_instance._player = go.AddComponent<PlayerManager>();
        }
    }

    public static void Clear()
    {
        Sound?.Clear();
        Scene?.Clear();
        UI?.Clear();
    }
}
