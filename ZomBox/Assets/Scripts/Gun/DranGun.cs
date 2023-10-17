using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
using UnityEngine;

public class DranGun : MonoBehaviour
{
    public Transform barel;
    public int damageAmount = 10;  // ����, ������� ��������� ��������

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
                    // ������� ���� �����
                    boss.TakeDamage(80f); // ������ �������������� ����� �� �����
                                          // �������� ���� � ���� �� � �������� ����� ����
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
        // ���������, �������� �� ������ �������������� ������ ����� ��� ��������� �����
        if (other.CompareTag("Enemy"))
        {
            // �������� ��������� �������� �������, �� ������� ��������� ����
            EnemyController enemyController = other.GetComponent<EnemyController>();

            // ���������, ���� �� ��������� ���������� ������
            if (enemyController != null)
            {
                // �������� ����� ��������� ����� � �����
                enemyController.TakeDamage(damageAmount);
            }
        }
    }
}
