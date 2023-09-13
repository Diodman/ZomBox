using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using Valve.VR.InteractionSystem;
using Valve.VR;
using System.Collections;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Color normalColor = Color.white; // Нормальный цвет кнопки
    private Color hoverColor = Color.blue; // Цвет при наведении на кнопку
    private Color pressedColor = Color.green; // Цвет при нажатии на кнопку

    private bool isHovering = false; // Флаг для отслеживания наведения на кнопку

    private Hand hand; // Контроллер

    private Image buttonImage; // Изображение кнопки

    public GameObject player;


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
        yield return new WaitForSeconds(0f);

        // Проверяем, наведен ли курсор на кнопку
        if (isHovering)
        {
            // Выполняем нажатие кнопки
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
            Destroy(player);
            Debug.Log("Restart");
        }
    }
}