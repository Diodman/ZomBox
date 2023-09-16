using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
using UnityEngine.XR;

public class Pause : MonoBehaviour
{
    public GameObject Record;
    public GameObject HP;
    public GameObject Pause1;
    public GameObject Pointer;
    public RectTransform canvasToDisplay;


    // Update is called once per frame
    public SteamVR_Action_Boolean pauseAction; // Кнопка для паузы

    private bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        Record.SetActive(true);
        HP.SetActive(true);
        Pause1.SetActive(false);
        Pointer.SetActive(false);
    }
    private void Update()
    {
        if (pauseAction.GetStateDown(SteamVR_Input_Sources.Any))
        {
            TogglePause();
        }
    }

    private void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f; // Остановить время
            Record.SetActive(false);
            HP.SetActive(false);
            Pause1.SetActive(true);
            Pointer.SetActive(true);
            // Получаем текущее положение и направление взгляда игрока
            Vector3 playerPosition = Camera.main.transform.position;
            Quaternion playerRotation = InputTracking.GetLocalRotation(XRNode.Head); float displayDistance = 3f; Vector3 canvasPosition = playerPosition + playerRotation * Vector3.forward * displayDistance;
            Quaternion canvasRotation = playerRotation * Quaternion.Euler(0f, 0f, 0f);
              // Поворот на 180 градусов
            canvasToDisplay.transform.position = canvasPosition; 
            canvasToDisplay.transform.rotation = canvasRotation * playerRotation;
            // Дополнительные действия для паузы, например, отображение меню паузы
        }
        else
        {
            Time.timeScale = 1f;
            Record.SetActive(true);
            HP.SetActive(true);
            Pause1.SetActive(false);
            Pointer.SetActive(false);// Возобновить время
            // Дополнительные действия для возобновления игры
        }
    }
}
