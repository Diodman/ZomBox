using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltController : MonoBehaviour
{
    public Transform beltSlot;  // Место, куда можно класть вещи
    private GameObject currentHeldItem;

    // Метод для добавления вещи на пояс
    public void AddItemToBelt(GameObject item)
    {
        // Проверка наличия места на поясе
        if (currentHeldItem == null)
        {
            currentHeldItem = item;
            item.transform.SetParent(beltSlot);
            item.transform.localPosition = Vector3.zero;
            item.transform.localRotation = Quaternion.identity;
        }
    }

    // Метод для удаления вещи с пояса
    public void RemoveItemFromBelt()
    {
        if (currentHeldItem != null)
        {
            Destroy(currentHeldItem);
            currentHeldItem = null;
        }
    }
}