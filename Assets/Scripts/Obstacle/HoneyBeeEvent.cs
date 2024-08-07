using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HoneyBeeEvent : MonoBehaviour
{
    private GameObject player;
    private PlayerController playerController;
    private Transform[] childTransform;
    private GameObject honeyBeePrefab;
    private GameObject beeQuizPopup;

    private float tempSpeed;
    private float eventDistance = 7f;
    private int randomBeeNumbers;
    private string beePath = "Puzzle/HoneyBee";

    void Start()
    {
        //player = GameObject.FindWithTag("Player");
        player = PlayerManager.playerManager.GetPlayer();
        childTransform = GetComponentsInChildren<Transform>();
        playerController = player.GetComponent<PlayerController>();
        tempSpeed = playerController.moveSpeed;
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance <= eventDistance && honeyBeePrefab == null)
        {
            SpawnBee();
        }
    }

    private void QuizPopup()
    {
        beeQuizPopup = Managers.Resource.Load<GameObject>("UI/Popup/UI_BeeQuizPopup");
        GameObject go = Instantiate(beeQuizPopup);
        UI_BeeQuizPopup popupScript = go.GetComponent<UI_BeeQuizPopup>();
        popupScript.SetCorrectOrIncorrectBeeNumber(randomBeeNumbers);
        popupScript.OnEndEvent += EndEvent;
    }

    private void SpawnBee()
    {
        playerController.moveSpeed = 0f;
        playerController.isPuzzlOn = true;
        honeyBeePrefab = Managers.Resource.Load<GameObject>(beePath);

        if (SceneManager.GetActiveScene().name == "RunningTutorialScene")
        {
            RunningTutorialManager.Instance.fourthTutorial.SetActive(true);
            Invoke("InstantiateGO", 2f);
        }
        else
        {
            InstantiateGO();
        }
    }

    private void InstantiateGO()
    {
        if (SceneManager.GetActiveScene().name == "RunningTutorialScene")
        {
            RunningTutorialManager.Instance.fourthTutorial.SetActive(false);
        }

        randomBeeNumbers = Random.Range(5, 10);
        for (int i = 0; i < randomBeeNumbers; i++)
        {
            GameObject honeyBee = Instantiate(honeyBeePrefab, childTransform[3]);
            StartCoroutine(MoveAndReturn(honeyBee));
        }
        Debug.Log($"{randomBeeNumbers}마리 벌 생성");
        Invoke("QuizPopup", 3.5f);
    }

    private IEnumerator MoveAndReturn(GameObject honeyBee)
    {
        Vector3 originalPosition = honeyBee.transform.position;
        Vector3 randomPosition = GetRandomFlyPosition();

        float moveTime = 1f; // 이동하는 데 걸리는 시간
        float time = 0f; //흐르는 시간

        while (time < moveTime)
        {
            honeyBee.transform.position = Vector3.Lerp(originalPosition, randomPosition, time / moveTime);
            time += Time.deltaTime;
            yield return null;
        }
        honeyBee.transform.position = randomPosition;

        yield return new WaitForSeconds(1f); //1초동안 보여줌

        time = 0f;
        while (time < moveTime)
        {
            honeyBee.transform.position = Vector3.Lerp(randomPosition, originalPosition, time / moveTime);
            time += Time.deltaTime;
            yield return null;
        }
        honeyBee.transform.position = originalPosition;
    }

    private Vector3 GetRandomFlyPosition()
    {
        // BeeSpawnRange의 범위 계산
        Transform BeeSpawnRange = childTransform[2];
        RectTransform rangeTransform = BeeSpawnRange.GetComponent<RectTransform>();
        float halfwidth = rangeTransform.rect.width / 2;
        float halfHeight = rangeTransform.rect.height / 2;

        Vector3 randomPosition = new Vector3(Random.Range(-halfwidth, halfwidth), Random.Range(-halfHeight, halfHeight), 0);

        return BeeSpawnRange.position + randomPosition;
    }

    private void EndEvent()
    {
        Managers.Sound.Play("harp strum 1", Sound.Effect);
        playerController.moveSpeed = tempSpeed;
        playerController.isPuzzlOn = false;

        int beeLevel = PlayerPrefs.GetInt("BeeLevel");
        beeLevel += 1;
        PlayerPrefs.SetInt("BeeLevel", beeLevel);
        PlayerPrefs.Save();

        Destroy(this.gameObject);
    }
}
