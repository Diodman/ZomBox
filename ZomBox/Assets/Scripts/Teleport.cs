using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TP : MonoBehaviour
{
    private bool hasKey = false; // Переменная, которая показывает, есть ли у игрока ключ

    private void OnTriggerEnter(Collider other)
    {
        if (hasKey) // Проверяем, есть ли ключ у игрока
        {
            SceneManager.LoadScene("loadingScene");
        }
        else
        {
            Debug.Log("Нет ключа!"); // Выводим сообщение, что у игрока нет ключа
        }
    }

    // Добавьте метод, который позволит установить наличие ключа у игрока
    public void SetHasKey(bool value)
    {
        hasKey = value;
    }
}