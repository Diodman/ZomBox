using UnityEngine;
using UnityEngine.SceneManagement;

public class TP : MonoBehaviour
{
    private string loadingScene; // ��� ������� �����, ���� ����� ����� ���������.
    private bool hasKey = false; // ����, �����������, ���� �� ���� � ���� ������.

    private void Update()
    {
        // ��������� ������� ������ � ���������� "Teleporting" � ������ �����.
        if (IsPlayerInTeleportingCollider())
        {
            // �������� ����� OnTriggerEnter, ���� ����� ��������� � ���������� "Teleporting".
            OnTriggerEnter();
        }
    }

    // �����, ���������� �� ������� Key ��� �������� �����.
    public void OnKeyPickedUp()
    {
        hasKey = true;
    }

    // �����, ���������� ��� ���������� ������ � �������� "Teleport".
    private void OnTriggerEnter()
    {
        // ���������, ��� � ������ ���� ���� � ��� ������� ����� �������.
        if (hasKey && !string.IsNullOrEmpty(loadingScene))
        {
            SwitchScene();
        }
    }

    private bool IsPlayerInTeleportingCollider()
    {
        // ����������� Physics.Raycast, ����� ���������, ���� �� ����� ������ ���������� "Teleporting".
        Ray ray = new Ray(transform.position, Vector3.down); // ������� ���, ������������ ���� �� ������� ����� �������.
        float rayLength = 2.0f; // ����� ����, ������� ����� ��������� ��� ������ ������ ����������.
        return Physics.Raycast(ray, rayLength, LayerMask.GetMask("Teleporting")); // �������� "Teleporting" �� ��� ������ ���� ����������.
    }

    private void SwitchScene()
    {
        // ��������� ������� �����.
        SceneManager.LoadScene(loadingScene);
    }
}
