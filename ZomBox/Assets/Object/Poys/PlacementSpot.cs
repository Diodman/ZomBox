using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSpot : MonoBehaviour
{
    public GameObject highlightedObject; // ������ ��� ���������, ����� ������� �����

    private bool isOccupied = false; // ����������, ������������, ������ �� ����� ��� ����������

    private void OnTriggerEnter(Collider other)
    {
        if (!isOccupied && other.CompareTag("Item"))
        {
            // �������� ��������� �����, ���� ����� �������
            highlightedObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!isOccupied && other.CompareTag("Item"))
        {
            // ��������� ���������, ����� ������� ������ �� ����
            highlightedObject.SetActive(false);
        }
    }

    public bool TryPlaceItem(GameObject item)
    {
        if (!isOccupied)
        {
            // ��������� �������
            item.transform.position = transform.position;
            item.transform.rotation = transform.rotation;
            item.GetComponent<ItemScript>().SetPlaced(true); // ��������� ��������� ��������
            isOccupied = true;
            highlightedObject.SetActive(false); // ������� ���������
            return true;
        }
        return false;
    }
}

public class ItemScript : MonoBehaviour
{
    private bool isPlaced = false; // ����������, ������������, �������� �� �������

    public void SetPlaced(bool placed)
    {
        isPlaced = placed;
    }

    public void PickUp()
    {
        if (!isPlaced)
        {
            // ��������� ������ �������� ��������
            // ��������, ������������� ��� ������� � ���� ������
        }
    }
}
