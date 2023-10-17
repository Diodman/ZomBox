using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSpot : MonoBehaviour
{
    public GameObject highlightedObject; // Объект для подсветки, когда предмет рядом

    private bool isOccupied = false; // Переменная, показывающая, занято ли место для размещения

    private void OnTriggerEnter(Collider other)
    {
        if (!isOccupied && other.CompareTag("Item"))
        {
            // Включаем подсветку места, если рядом предмет
            highlightedObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!isOccupied && other.CompareTag("Item"))
        {
            // Отключаем подсветку, когда предмет уходит из зоны
            highlightedObject.SetActive(false);
        }
    }

    public bool TryPlaceItem(GameObject item)
    {
        if (!isOccupied)
        {
            // Размещаем предмет
            item.transform.position = transform.position;
            item.transform.rotation = transform.rotation;
            item.GetComponent<ItemScript>().SetPlaced(true); // Обновляем состояние предмета
            isOccupied = true;
            highlightedObject.SetActive(false); // Убираем подсветку
            return true;
        }
        return false;
    }
}

public class ItemScript : MonoBehaviour
{
    private bool isPlaced = false; // Переменная, показывающая, размещен ли предмет

    public void SetPlaced(bool placed)
    {
        isPlaced = placed;
    }

    public void PickUp()
    {
        if (!isPlaced)
        {
            // Реализуем логику поднятия предмета
            // Например, устанавливаем его позицию в руку игрока
        }
    }
}
