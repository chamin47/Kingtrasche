using System.Collections;
using System.Collections.Generic;
using UGS;
using UnityEngine;

public class DataLoadTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        UnityGoogleSheet.LoadFromGoogle<int, GameBalance.PlayerData>((list, map) => { }, true);
    }

    // Update is called once per frame
    void Start()
    {
       
    }
}
