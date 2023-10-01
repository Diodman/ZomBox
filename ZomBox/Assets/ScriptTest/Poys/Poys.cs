using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltController : MonoBehaviour
{
    public Transform beltSlot;  // �����, ���� ����� ������ ����
    private GameObject currentHeldItem;

    // ����� ��� ���������� ���� �� ����
    public void AddItemToBelt(GameObject item)
    {
        // �������� ������� ����� �� �����
        if (currentHeldItem == null)
        {
            currentHeldItem = item;
            item.transform.SetParent(beltSlot);
            item.transform.localPosition = Vector3.zero;
            item.transform.localRotation = Quaternion.identity;
        }
    }

    // ����� ��� �������� ���� � �����
    public void RemoveItemFromBelt()
    {
        if (currentHeldItem != null)
        {
            Destroy(currentHeldItem);
            currentHeldItem = null;
        }
    }
}