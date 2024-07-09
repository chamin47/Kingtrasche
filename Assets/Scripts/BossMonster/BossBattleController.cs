using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattleController : MonoBehaviour
{
    private bool bossActive;

    public GameObject blockers;

    public Transform theBoss;
    public float bossGrowSpeed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (bossActive == true)
        {
            if(theBoss.localScale != Vector3.one)
            {
                theBoss.localScale = Vector3.MoveTowards(
                    theBoss.localScale,
                    Vector3.one,
                    bossGrowSpeed * Time.deltaTime);
            }
        }
    }

    public void ActivateBattle()
    {
        bossActive = true;

        blockers.SetActive(true);
    }
}
