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
    // private float bossDamage = 33; // ������������� ���� �� �����
    private Dictionary<GameObject, int> hitCountDictionary; // Zombie


    // ����� �������� ��� ���� ������ ����� � ������ ������ 

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
                var boss = hit.transform.GetComponent<Boss>();
                var player = hit.transform.GetComponent<Player>();
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
                    }
                }
                else if (player != null)
                {
                    player.GameOverPlayer();
                    HPManeger.score -= 15;
                }
                else if (boss != null)
                {
                    // ������� ���� �����
                    boss.TakeDamage(33f); // ������ �������������� ����� �� �����
                                          // �������� ���� � ���� �� � �������� ����� ����
                    if (boss.health <= 0)
                    {
                        boss.Kill();
                    }
                }
            }
        }
    }
}


