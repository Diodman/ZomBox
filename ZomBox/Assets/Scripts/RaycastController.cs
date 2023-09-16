using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RaycastController : MonoBehaviour
{
    public LayerMask interactableLayer; // Слой, на котором находятся кнопки
    private Camera vrCamera; // Камера, используемая для отслеживания луча

    void Start()
    {
        vrCamera = Camera.main; // Получаем главную камеру сцены
    }

    void Update()
    {
        // Создаем луч от положения и направления контроллера
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        // Проверяем, сталкивается ли луч с объектом на слое interactableLayer
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, interactableLayer))
        {
            // Проверяем, является ли столкновение с объектом UI (кнопкой)
            if (hit.collider.GetComponent<Button>())
            {
                // Выполняем нажатие на кнопку с помощью события PointerClick
                ExecuteEvents.Execute(hit.collider.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
            }
        }
    }
}
