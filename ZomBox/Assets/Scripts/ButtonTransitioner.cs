using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Valve.VR.InteractionSystem;
using Valve.VR;
using System.Collections;

public class ButtonTransitioner : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Color normalColor = Color.white; // Нормальный цвет кнопки
    private Color hoverColor = Color.blue; // Цвет при наведении на кнопку
    private Color pressedColor = Color.green; // Цвет при нажатии на кнопку
    public GameObject objectToHide;
    public GameObject objectToShow;
    private bool isHovering = false; // Флаг для отслеживания наведения на кнопку

    private Hand hand; // Контроллер

    private Image buttonImage; // Изображение кнопки

    private void Start()
    {
        buttonImage = GetComponent<Image>();

        hand = GetComponentInParent<Hand>(); // Получаем контроллер из родительского объекта
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Изменяем цвет кнопки при наведении на неё
        isHovering = true;
        buttonImage.color = hoverColor;

        // Запускаем корутину для задержки нажатия кнопки
        StartCoroutine(ButtonDelay());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Возвращаем цвет кнопки к нормальному цвету, если мышь покидает область кнопки
        isHovering = false;
        buttonImage.color = normalColor;

        // Останавливаем выполнение корутины, если курсор покинул область кнопки
        StopCoroutine(ButtonDelay());
    }

    private IEnumerator ButtonDelay()
    {
        // Ждем три секунды
        yield return new WaitForSeconds(3f);

        // Проверяем, наведен ли курсор на кнопку
        if (isHovering)
        {
            // Выполняем нажатие кнопки
            objectToHide.SetActive(false);
            // Делаем объектToShow активным
            objectToShow.SetActive(true);
            Debug.Log("Developers");
        }
    }
}