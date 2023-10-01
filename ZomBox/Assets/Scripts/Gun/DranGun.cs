using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class DranGun : MonoBehaviour
{
    public Transform barel;
    private Interactable interactable;

    private void Start()
    {
        interactable = GetComponent<Interactable>();
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

    private void OnCollisionEnter(Collision collision)
    {
        // Получаем объект, с которым произошла коллизия
        GameObject collisionObject = collision.gameObject;

        // Проверяем, что объект имеет компонент для получения урона
        DamageReceiver damageReceiver = collisionObject.GetComponent<DamageReceiver>();

        if (damageReceiver != null)
        {
            var player = GetComponent<Player>();
            // Вызываем метод для нанесения урона объекту
            if (player != null)
            {
                player.GameOverPlayer();
                HPManeger.score -= 50;
            }
        }
    }
}
