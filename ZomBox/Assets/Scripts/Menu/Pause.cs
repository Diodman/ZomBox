using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
using UnityEngine.XR;
using UnityEngine.InputSystem;

public class Pause : MonoBehaviour
{
    public Camera playerCamera;
    public GameObject Record;
    public GameObject HP;
    public GameObject Pointer;
    public GameObject canvasObject;
    private Transform player;
    public float distance;


    // Update is called once per frame
    public SteamVR_Action_Boolean pauseAction; // Кнопка для паузы

    private bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        Record.SetActive(true);
        HP.SetActive(true);
        canvasObject.SetActive(false);
        Pointer.SetActive(false);
    }
    private void Update()
    {
        Vector3 canvasPosition = playerCamera.transform.position + playerCamera.transform.forward * distance; // distance - желаемое расстояние от камеры до канваса
        canvasObject.transform.position = canvasPosition;
        if (pauseAction.GetStateDown(SteamVR_Input_Sources.Any))
        {
            TogglePause();
        }
    }

    private void TogglePause()
    {
        canvasObject.SetActive(!canvasObject.activeSelf);
        isPaused = !isPaused;
        if (isPaused)
        {
            Time.timeScale = 0f; // Остановить время
            Record.SetActive(false);
            HP.SetActive(false);
            canvasObject.SetActive(true);
            Pointer.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            Record.SetActive(true);
            HP.SetActive(true);
            canvasObject.SetActive(false);
            Pointer.SetActive(false);// Возобновить время
        }
    }
}
