using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TP : MonoBehaviour
{
    private bool hasKey = false; // ����������, ������� ����������, ���� �� � ������ ����

    private void OnTriggerEnter(Collider other)
    {
        if (hasKey) // ���������, ���� �� ���� � ������
        {
            SceneManager.LoadScene("loadingScene");
        }
        else
        {
            Debug.Log("��� �����!"); // ������� ���������, ��� � ������ ��� �����
        }
    }

    // �������� �����, ������� �������� ���������� ������� ����� � ������
    public void SetHasKey(bool value)
    {
        hasKey = value;
    }
}