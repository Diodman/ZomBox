using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tel : MonoBehaviour
{
    private rust_key rust_key;

    private void Start()
    {
        rust_key = GameObject.FindObjectOfType<rust_key>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Проверьте, есть ли ключ в руке игрока.
        if (rust_key != null && rust_key.HasKey())
        {
            SceneManager.LoadScene("loadingScene");
        }
    }
}