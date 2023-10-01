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
    public float fireRate = 0.1f; // ����� �������� ����� ����������

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

            if (fireAction[hand].state && !isShooting) // �������� ������� �� fireAction[hand].state � �������� isShooting
            {
                StartShooting();
            }
            else if (!fireAction[hand].state && isShooting) // �������� ������� �� !fireAction[hand].state � �������� isShooting
            {
                StopShooting();
            }
        }
    }

    private void StartShooting()
    {
        isShooting = true; // ������������� ���� ��������
        StartCoroutine(Shoot()); // ��������� ��������
    }

    private void StopShooting()
    {
        isShooting = false; // ���������� ���� ��������
    }

    private IEnumerator Shoot()
    {
        while (isShooting) // ��������� ���� ��������
        {
            if (interactable.attachedToHand != null) // ���������, ��� ������ ��� ��� ��������� � ����
            {
                Fire(); // ��������
            }

            yield return new WaitForSeconds(fireRate); // ���� �������� �����
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
                                // ������� ���� �����
                                boss.TakeDamage(50f); // ������ �������������� ����� �� �����
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
        }
    }

}
