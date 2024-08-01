using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager playerManager { get; private set; }
    private GameObject playerPrefab;
    private GameObject PlayerInstance;

    private void Awake()
    {
        if (playerManager == null)
        {
            playerManager = this;
            DontDestroyOnLoad(gameObject);
            //SpawnPlayer();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SpawnPlayer()
    {
        playerPrefab = Managers.Resource.Load<GameObject>("Player/Player");
        if (playerPrefab != null && PlayerInstance == null)
        {
            if (SceneManager.GetActiveScene().name == "LobbyScene")
            {
                Vector3 newPosition = new Vector3(0, -2, 0);
                PlayerInstance = Instantiate(playerPrefab, newPosition, Quaternion.identity);
            }
            else
            {
                PlayerInstance = Instantiate(playerPrefab);
            }
        }
    }

    public GameObject GetPlayer()
    {
        return PlayerInstance;
    }
}
