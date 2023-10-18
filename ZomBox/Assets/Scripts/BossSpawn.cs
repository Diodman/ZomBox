using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawn : MonoBehaviour
{
    public float Period;
    public GameObject BossEnemy;
    int k = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (ScoreManeger.score >= 1000 && k == 0)
        {
            Instantiate(BossEnemy, transform.position, transform.rotation);
            k = k + 1;
        }
        // if (TimeUntilNextSpawn <= 0.0f && ScoreManeger.score >= 100)
        //{
        //  TimeUntilNextSpawn = Period;
        // Instantiate(BossEnemy, transform.position, transform.rotation);
        //}
    }
}