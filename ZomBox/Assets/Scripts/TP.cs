using UnityEngine;
using UnityEngine.SceneManagement;

public class TP : MonoBehaviour
{
    private string loadingScene; // Имя целевой сцены, куда игрок будет перемещен.
    private bool hasKey = false; // Флаг, указывающий, есть ли ключ в руке игрока.

    private void Update()
    {
        // Проверяем наличие игрока в коллайдере "Teleporting" в каждом кадре.
        if (IsPlayerInTeleportingCollider())
        {
            // Вызываем метод OnTriggerEnter, если игрок находится в коллайдере "Teleporting".
            OnTriggerEnter();
        }
    }

    // Метод, вызываемый из скрипта Key при поднятии ключа.
    public void OnKeyPickedUp()
    {
        hasKey = true;
    }

    // Метод, вызываемый при нахождении игрока в триггере "Teleport".
    private void OnTriggerEnter()
    {
        // Проверяем, что у игрока есть ключ и имя целевой сцены указано.
        if (hasKey && !string.IsNullOrEmpty(loadingScene))
        {
            SwitchScene();
        }
    }

    private bool IsPlayerInTeleportingCollider()
    {
        // Используйте Physics.Raycast, чтобы проверить, есть ли игрок внутри коллайдера "Teleporting".
        Ray ray = new Ray(transform.position, Vector3.down); // Создаем луч, направленный вниз от позиции этого объекта.
        float rayLength = 2.0f; // Длина луча, которую можно настроить под размер вашего коллайдера.
        return Physics.Raycast(ray, rayLength, LayerMask.GetMask("Teleporting")); // Замените "Teleporting" на имя вашего слоя коллайдера.
    }

    private void SwitchScene()
    {
        // Загружаем целевую сцену.
        SceneManager.LoadScene(loadingScene);
    }
}
