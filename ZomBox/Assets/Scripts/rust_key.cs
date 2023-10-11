using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class rust_key : MonoBehaviour
{
    private bool isPickedUp = false; // Флаг, указывающий, взял ли игрок ключ.

    private void OnTriggerEnter(Collider other)
    {
        // Проверяем, что игрок подошел к ключу и еще не взял его.
        if (other.CompareTag("Player") && !isPickedUp)
        {
            isPickedUp = true;
            // Вызываем метод в скрипте SceneSwitcher, чтобы сообщить о взятии ключа.
            FindObjectOfType<TP>().OnKeyPickedUp();
        }
    }
}
