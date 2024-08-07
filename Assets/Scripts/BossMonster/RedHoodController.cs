using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RedHoodController : MonoBehaviour, IBossController
{
    public BossState currentState = BossState.Idle;
    public int currentHealth { get; set; }
    public int maxHealth { get; private set; }
    private GameObject basicBulletPrefab;
    private GameObject redAppleIndicatorPrefab;
    private GameObject greenAppleIndicatorPrefab;
    private GameObject redApplePrefab;
    private GameObject greenApplePrefab;
    private GameObject basketPrefab;
    private GameObject laserPrefab;
    private GameObject platformPrefab;

    private Transform player;
    private Animator anim;

    private List<Skill> skillList = new List<Skill>();
    private int currentSkillIndex = 0;
    private int currentSkillCount = 0;
    private float skillInterval = 0f; // 스킬 실행 간격
    private float skillTimer = 0.0f;
    private bool isSkillExecuting = false;
    private bool facingRight = true;

    public event Action OnHealthChanged;

    private void Awake()
    {
        maxHealth = 500;
    }

    private void Start()
    {
        currentHealth = maxHealth;
        currentState = BossState.Phase1;
        player = GameObject.FindWithTag("Player").transform;
        anim = GetComponent<Animator>();

        // 스킬 프리팹 로드
        basicBulletPrefab = Managers.Resource.Load<GameObject>("BossSkill/RedHoodBoss/RedBullet");
        redAppleIndicatorPrefab = Managers.Resource.Load<GameObject>("BossSkill/RedHoodBoss/RedAppleIndicator");
        greenAppleIndicatorPrefab = Managers.Resource.Load<GameObject>("BossSkill/RedHoodBoss/GreenAppleIndicator");
        redApplePrefab = Managers.Resource.Load<GameObject>("BossSkill/RedHoodBoss/RedApple");
        greenApplePrefab = Managers.Resource.Load<GameObject>("BossSkill/RedHoodBoss/GreenApple");
        basketPrefab = Managers.Resource.Load<GameObject>("BossSkill/RedHoodBoss/Basket");
        platformPrefab = Managers.Resource.Load<GameObject>("BossSkill/RedHoodBoss/Platform");
        laserPrefab = Managers.Resource.Load<GameObject>("BossSkill/RedHoodBoss/Laser");

        // 초기 스킬 설정
        Phase1();
    }

    private void Update()
    {
        if (player.position.x < transform.position.x)
        {
            facingRight = false;
            transform.rotation = Quaternion.Euler(0, 180, 0); // 왼쪽
        }
        else
        {
            facingRight = true;
            transform.rotation = Quaternion.Euler(0, 0, 0); // 오른쪽
        }

        if (isSkillExecuting)
        {
            return;
        }

        skillTimer -= Time.deltaTime;
        if (skillTimer <= 0)
        {
            Skill currentSkill = skillList[currentSkillIndex];
            StartCoroutine(ExecuteSkill(currentSkill.Action));
            skillTimer = skillInterval;
            currentSkillCount++;

            if (currentSkillCount >= currentSkill.Count)
            {
                currentSkillIndex = (currentSkillIndex + 1) % skillList.Count;
                currentSkillCount = 0;
            }
        }

        UpdatePhaseTransition();
    }

    private IEnumerator ExecuteSkill(System.Func<IEnumerator> skill)
    {
        isSkillExecuting = true;
        yield return StartCoroutine(skill());
        isSkillExecuting = false;
    }

    private void UpdatePhaseTransition()
    {
        switch (currentState)
        {
            case BossState.Idle:
                break;
            case BossState.Phase1:
                if (currentHealth <= maxHealth * 0.66f)
                {
                    currentState = BossState.Phase2;
                    Phase2();
                }
                break;
            case BossState.Phase2:
                if (currentHealth <= maxHealth * 0.33f)
                {
                    currentState = BossState.Phase3;
                    Phase3();
                }
                break;
            case BossState.Phase3:
                if (currentHealth <= 0)
                {
                    currentState = BossState.Dead;
                }
                break;
            case BossState.Dead:
                break;
        }
    }

    private IEnumerator BasicAttack()
    {
        yield return new WaitForSeconds(0f);
        //anim.SetTrigger("isAttack");
        Vector2 direction = player.position.x > transform.position.x ? Vector2.right : Vector2.left;
        GameObject fishbone = Instantiate(basicBulletPrefab, transform.position, Quaternion.identity);
        fishbone.GetComponent<RedBullet>().direction = direction; // 발사 로직 예시
        yield return new WaitForSeconds(1.5f);
    }

    private IEnumerator BasicAttackCType()
    {
        yield return new WaitForSeconds(0f);
        Vector2 direction = facingRight ? Vector2.right : Vector2.left;

        //anim.SetTrigger("isAttack");
        float[] angles = new float[] { -20f, 0f, 20f };
        foreach (float angle in angles)
        {
            float radian = angle * Mathf.Deg2Rad;
            Vector2 rotatedDirection = new Vector2(
                direction.x * Mathf.Cos(radian) - direction.y * Mathf.Sin(radian),
                direction.x * Mathf.Sin(radian) + direction.y * Mathf.Cos(radian)
            );

            GameObject fishbone = Instantiate(basicBulletPrefab, transform.position, Quaternion.identity);
            fishbone.GetComponent<RedBullet>().direction = rotatedDirection;
        }

        yield return new WaitForSeconds(1.5f);
    }

    private IEnumerator AppleDrop()
    {
        for (int i = 0; i < 3; i++)
        {
            // 빨간 사과 떨어질 곳 표시
            Vector3 redApplePosition = new Vector3(Random.Range(-8f, 8f), 0.7291f, 0);
            GameObject redAppleIndicator = Instantiate(redAppleIndicatorPrefab, redApplePosition, Quaternion.identity); // 빨간 사과 표시

            yield return new WaitForSeconds(2f);
            Destroy(redAppleIndicator);

            // 빨간 사과 떨어짐
            Instantiate(redApplePrefab, new Vector3(redApplePosition.x, 5f, 0f), Quaternion.identity);
            yield return new WaitForSeconds(1f);

            // 초록 사과 떨어질 곳 표시
            Vector3 greenApplePosition = new Vector3(Random.Range(-8f, 8f), 0.7291f, 0);
            GameObject greenAppleIndicator = Instantiate(greenAppleIndicatorPrefab, greenApplePosition, Quaternion.identity); // 초록 사과 표시

            yield return new WaitForSeconds(2f);
            Destroy(greenAppleIndicator);

            // 초록 사과 떨어짐
            Instantiate(greenApplePrefab, new Vector3(greenApplePosition.x, 5f, 0f), Quaternion.identity);
            yield return new WaitForSeconds(1f);
        }
    }

    private IEnumerator BasketThrow()
    {
        for (int i = 0; i < 3; i++)
        {
            // 바구니 던질 위치 설정 (랜덤)
            Vector3 basketPosition = new Vector3(Random.Range(transform.position.x, transform.position.x - 3f), Random.Range(transform.position.y, transform.position.y + 2f), 0);
            GameObject basket = Instantiate(basketPrefab, basketPosition, Quaternion.identity);

            // 45도 각도로 던지기
            basket.GetComponent<Basket>().initialForce = 15f;

            yield return new WaitForSeconds(1f);
        }
    }

    private IEnumerator LaserAttack()
    {
        // 점멸 및 레이저 준비
        for (int i = 0; i < 3; i++)
        {
            // 점멸 효과
            gameObject.GetComponent<SpriteRenderer>().enabled = !gameObject.GetComponent<SpriteRenderer>().enabled;
            yield return new WaitForSeconds(1f);
        }

        // 레이저 발사
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
        laser.GetComponent<Laser>().SetDirection(facingRight ? Vector2.right : Vector2.left);

        // 랜덤 발판 생성
        Vector3 platformPosition = new Vector3(Random.Range(-5.5f, 1.1f), Random.Range(0.5f, 1.2f), 0);
        GameObject platform = Instantiate(platformPrefab, platformPosition, Quaternion.identity);

        // 화면 흔들림 효과
        StartCoroutine(ShakeCamera(1.0f, 0.1f));

        yield return new WaitForSeconds(3f);

        // 레이저 제거
        Destroy(laser);

        yield return new WaitForSeconds(1f);

        // 발판 제거
        Destroy(platform);
    }

    private IEnumerator ShakeCamera(float duration, float magnitude)
    {
        Vector3 originalPos = Camera.main.transform.localPosition;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            Camera.main.transform.localPosition = new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        Camera.main.transform.localPosition = originalPos;
    }


    private void Phase1()
    {
        skillList.Clear();
        skillList.Add(new Skill(() => BasicAttack(), 0f, 3));
        skillList.Add(new Skill(() => BasicAttackCType(), 0f, 3));
        skillList.Add(new Skill(() => AppleDrop(), 0f, 1));
    }

    private void Phase2()
    {
        skillList.Clear();
        skillList.Add(new Skill(() => BasicAttack(), 0f, 3));
        skillList.Add(new Skill(() => BasicAttackCType(), 0f, 3));
        skillList.Add(new Skill(() => AppleDrop(), 0f, 1));
        skillList.Add(new Skill(() => BasketThrow(), 0f, 1));
    }

    private void Phase3()
    {
        skillList.Clear();

        List<Skill> possibleSkills = new List<Skill>
        {
            new Skill(() => BasicAttack(), 0f, 1),
            new Skill(() => BasicAttackCType(), 0f, 1),
            new Skill(() => AppleDrop(), 0f, 1),
            new Skill(() => BasketThrow(), 0f, 1),
            new Skill(() => LaserAttack(), 0f, 1),
        };

        // 가능한 스킬 목록에서 랜덤하게 선택하여 스킬 리스트를 만듭니다.
        while (possibleSkills.Count > 0)
        {
            int index = Random.Range(0, possibleSkills.Count);
            skillList.Add(possibleSkills[index]);
            possibleSkills.RemoveAt(index);
        }
    }

    public void TakeDamage(int damage)
    {
        if (currentState == BossState.Dead)
            return;

        Managers.Sound.Play("7", Sound.Effect);
        currentHealth -= damage;
        OnHealthChanged?.Invoke();
        if (currentHealth <= 0)
        {
            int bossLevel = PlayerPrefs.GetInt("BossLevel");
            bossLevel += 1;
            PlayerPrefs.SetInt("BossLevel", bossLevel);
            PlayerPrefs.Save();

            currentHealth = 0;
            Managers.Game.GameClear();
            Destroy(gameObject);
        }
    }

    private class Skill
    {
        public Func<IEnumerator> Action { get; private set; }
        public float Cooldown { get; private set; }
        public int Count { get; private set; }

        public Skill(Func<IEnumerator> action, float cooldown, int count)
        {
            Action = action;
            Cooldown = cooldown;
            Count = count;
        }
    }
}
