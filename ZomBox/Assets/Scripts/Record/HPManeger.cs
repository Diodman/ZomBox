using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPManeger : MonoBehaviour
{
    [SerializeField] Text ScoreText;

    public static float score;
    int hp;

    void Start()
    {
        score = 100;
    }

    void Update()
    {
        if (Time.timeScale != 0)
        {
            hp = (int)score;
            ScoreText.text = "HP: " + hp.ToString();
        }
    }
}
