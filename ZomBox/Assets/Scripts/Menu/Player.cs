using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
using UnityEngine.XR;

public class Player : MonoBehaviour
{
    public GameObject Game;
    public GameObject PlayerMenu;
    public GameObject Pointer;
    public RectTransform canvasToDisplay;
    bool dead;
    // Start is called before the first frame update
    void Start()
    {
        Game.gameObject.SetActive(false);
        PlayerMenu.SetActive(true);
        Pointer.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        GameOverPlayer();
    }

    public void GameOverPlayer()
    {
        if (HPManeger.score < 25)
        {
            Game.SetActive(true);
            PlayerMenu.SetActive(false);
            Time.timeScale = 0f;
            Pointer.SetActive(true);
        }
    }
}
