using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
using UnityEngine;

public class DranGun : MonoBehaviour
{
    public Transform barel;
    public int damageAmount = 10;  // Урон, который наносится объектам

    private void Start()
    {

    }

    private void Update()
    {
        //Fire();
    }

    private void Fire()
    {
        RaycastHit hit;
        if (Physics.Raycast(barel.position, barel.forward, out hit, 25))
        {
            if (hit.transform != null)
            {
                var zombie = hit.transform.GetComponent<Zombie>();
                var boss = hit.transform.GetComponent<Boss>();
                var player = hit.transform.GetComponent<Player>();
                if (zombie != null)
                {
                    zombie.Kill();
                    ScoreManeger.score += 5;
                }
                else if (boss != null)
                {
                    // Наносим урон боссу
                    boss.TakeDamage(80f); // Пример фиксированного урона по боссу
                                          // Проверка если у него хп и вызываем метод килл
                    if (boss.health <= 0)
                    {
                        boss.Kill();
                    }
                }
                else if (player != null)
                {
                    player.GameOverPlayer();
                    HPManeger.score -= 50;
                }
            }
        }
    }

    
    private void OnTriggerEnter(Collider other)
    {
        // Проверяем, является ли объект сталкивающийся объект целью для нанесения урона
        if (other.CompareTag("Enemy"))
        {
            // Получаем компонент сценария объекта, на который наносится урон
            EnemyController enemyController = other.GetComponent<EnemyController>();

            // Проверяем, есть ли компонент управления врагом
            if (enemyController != null)
            {
                // Вызываем метод нанесения урона у врага
                enemyController.TakeDamage(damageAmount);
            }
        }
    }
}
