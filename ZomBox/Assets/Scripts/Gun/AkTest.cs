using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class AkTest : MonoBehaviour
{
    public AudioClip fireClip;
    public AudioSource audioSource;
    private Interactable interactable;
    public SteamVR_Action_Boolean fireAction;
    public Transform barrel;
    public ParticleSystem muzzleFlash;

    public Boss boss;

    private Dictionary<GameObject, int> hitCountDictionary;
    private bool isShooting = false;
    public float fireRate = 0.1f; // Время задержки между выстрелами

    private void Start()
    {
        interactable = GetComponent<Interactable>();
        hitCountDictionary = new Dictionary<GameObject, int>();
    }

    private void Update()
    {
        if (interactable.attachedToHand != null)
        {
            SteamVR_Input_Sources hand = interactable.attachedToHand.handType;

            if (fireAction[hand].state && !isShooting) // Изменено условие на fireAction[hand].state и проверка isShooting
            {
                StartShooting();
            }
            else if (!fireAction[hand].state && isShooting) // Изменено условие на !fireAction[hand].state и проверка isShooting
            {
                StopShooting();
            }
        }
    }

    private void StartShooting()
    {
        isShooting = true; // Устанавливаем флаг стрельбы
        StartCoroutine(Shoot()); // Запускаем стрельбу
    }

    private void StopShooting()
    {
        isShooting = false; // Сбрасываем флаг стрельбы
    }

    private IEnumerator Shoot()
    {
        while (isShooting) // Проверяем флаг стрельбы
        {
            if (interactable.attachedToHand != null) // Проверяем, что оружие все еще находится в руке
            {
                Fire(); // Стреляем
            }

            yield return new WaitForSeconds(fireRate); // Ждем заданное время
        }
    }

    private void Fire()
    {
        muzzleFlash.Play();
        audioSource.Play();
        RaycastHit hit;
        if (Physics.Raycast(barrel.position, barrel.forward, out hit, 300))
        {
            if (hit.transform != null)
            {
                var zombie = hit.transform.GetComponent<Zombie>();
                if (zombie != null)
                {
                    if (!hitCountDictionary.ContainsKey(hit.transform.gameObject))
                    {
                        hitCountDictionary.Add(hit.transform.gameObject, 1);
                    }
                    else
                    {
                        hitCountDictionary[hit.transform.gameObject]++;
                        if (hitCountDictionary[hit.transform.gameObject] >= 2)
                        {
                            zombie.Kill();
                            ScoreManeger.score += 10;
                            Destroy(hit.transform.gameObject, 5);
                        }
                        else
                        {
                            var boss = hit.transform.GetComponent<Boss>();
                            if (boss != null)
                            {
                                // Наносим урон боссу
                                boss.TakeDamage(50f); // Пример фиксированного урона по боссу
                                // Проверка если у него хп и вызываем метод килл
                                if (boss.health <= 0)
                                {
                                    boss.Kill();
                                }
                            }
                        }
                    }
                }
            }
        }
    }

}
