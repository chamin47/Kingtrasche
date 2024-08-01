using UnityEngine;

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
            PlayerInstance = Instantiate(playerPrefab);
        }
    }

    public GameObject GetPlayer()
    {
        return PlayerInstance;
    }
}
