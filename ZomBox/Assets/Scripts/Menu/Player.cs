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

    }

    public void GameOverPlayer()
    {
        if (HPManeger.score < 25)
        {
            Game.SetActive(true);
            PlayerMenu.SetActive(false);
            Time.timeScale = 0f;
            Pointer.SetActive(true);
            // ѕолучаем текущее положение и направление взгл€да игрока
            Vector3 playerPosition = Camera.main.transform.position;
            Quaternion playerRotation = InputTracking.GetLocalRotation(XRNode.Head); float displayDistance = 3f; Vector3 canvasPosition = playerPosition + playerRotation * Vector3.forward * displayDistance;
            Quaternion canvasRotation = playerRotation * Quaternion.Euler(0f, 180f, 0f); // ѕоворот на 180 градусов
            canvasToDisplay.transform.position = canvasPosition; 
            canvasToDisplay.transform.rotation = canvasRotation * playerRotation;
            // ƒополнительные действи€ дл€ паузы, например, отображение меню паузы

        }
    }
}
