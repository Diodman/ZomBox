using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class rust_key : MonoBehaviour
{
    private bool isPickedUp = false; // ����, �����������, ���� �� ����� ����.

    private void OnTriggerEnter(Collider other)
    {
        // ���������, ��� ����� ������� � ����� � ��� �� ���� ���.
        if (other.CompareTag("Player") && !isPickedUp)
        {
            isPickedUp = true;
            // �������� ����� � ������� SceneSwitcher, ����� �������� � ������ �����.
            FindObjectOfType<TP>().OnKeyPickedUp();
        }
    }
}
