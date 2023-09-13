using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RaycastController : MonoBehaviour
{
    public LayerMask interactableLayer; // ����, �� ������� ��������� ������
    private Camera vrCamera; // ������, ������������ ��� ������������ ����

    void Start()
    {
        vrCamera = Camera.main; // �������� ������� ������ �����
    }

    void Update()
    {
        // ������� ��� �� ��������� � ����������� �����������
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        // ���������, ������������ �� ��� � �������� �� ���� interactableLayer
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, interactableLayer))
        {
            // ���������, �������� �� ������������ � �������� UI (�������)
            if (hit.collider.GetComponent<Button>())
            {
                // ��������� ������� �� ������ � ������� ������� PointerClick
                ExecuteEvents.Execute(hit.collider.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
            }
        }
    }
}
