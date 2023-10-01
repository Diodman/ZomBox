using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PistolGun : MonoBehaviour
{
    public AudioClip fireClip;
    public AudioSource audioSource;
    private Interactable interactable;
    public SteamVR_Action_Boolean fireAction;
    public Transform barel;
    public ParticleSystem muzleFlash;

    public Boss boss;
    // private float bossDamage = 33; // Фиксированный урон по боссу
    private Dictionary<GameObject, int> hitCountDictionary; // Zombie


    // Нужно добавить еще один объект босса в каждый скрипт 

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

            if (fireAction[hand].stateDown)
            {
                Fire();
            }
        }
    }

    private void Fire()
    {
        muzleFlash.Play();
        audioSource.Play();
        RaycastHit hit;
        if (Physics.Raycast(barel.position, barel.forward, out hit, 300))
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
                        hitCountDictionary[hit.transform.gameObject]++; // Zombie
                        if (hitCountDictionary[hit.transform.gameObject] >= 3)
                        {
                            zombie.Kill();
                            ScoreManeger.score += 15;
                            Destroy(hit.transform.gameObject, 5);
                        }
                        else
                        {
                            var boss = hit.transform.GetComponent<Boss>();
                            if (boss != null)
                            {
                                // Наносим урон боссу
                                boss.TakeDamage(33f); // Пример фиксированного урона по боссу
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


