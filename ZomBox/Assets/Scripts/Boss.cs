using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Valve.VR.InteractionSystem;

public class Boss : MonoBehaviour
{
    CapsuleCollider capsuleCollider;
    Animator animator;
    bool dead;
    NavMeshAgent navMeshAgent;
    Player player;
    public Transform barel;

    bool run = true;
    bool attack = true;

    public float stoppingDistance = 2f;

    private Dictionary<GameObject, int> hitCountDictionary;

    private float attackTimer = 0f;
    public float attackInterval = 2f;
    private bool canAttack = true; // Флаг, позволяющий атаковать

    public float health = 500;

    public GameObject droppedItemPrefab; // Префаб выпадающего ключа
    private Vector3 deathPosition; // Позиция смерти босса


    // Start is called before the first frame update
    void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
        animator = GetComponentInChildren<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<Player>();
        hitCountDictionary = new Dictionary<GameObject, int>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (dead)
        {
            // Проверка, если босс мертв, создаем ключ на позиции смерти
            Instantiate(droppedItemPrefab, deathPosition, Quaternion.identity); 
            return;
        }*/

        // Проверяем, достиг ли игрок счета 1000 для создания зомби-босса
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= stoppingDistance)
        {
            navMeshAgent.isStopped = true; // Останавливаем NavMeshAgent
            animator.SetBool("isRunning", false);
            if (canAttack == true)
            {
                animator.SetTrigger("attack");
            }

        }
        else
        {
            navMeshAgent.isStopped = false; // Возобновляем движение, если расстояние больше stoppingDistance
            navMeshAgent.SetDestination(player.transform.position);
            animator.SetBool("isRunning", true);
        }

        if (ScoreManeger.score <= 1000)
        {
            return;
        }
        navMeshAgent.SetDestination(player.transform.position);
        attackTimer += Time.deltaTime;
        if (canAttack)
        {
            StartCoroutine(AttackDelay());
        }
        if (attackTimer >= attackInterval)
        {
            Shot();
            attackTimer = 0f;
        }
    }
    // Убийство
    public void Kill()
    {
        if (!dead)
        {
            dead = true;
            Destroy(capsuleCollider);
            Destroy(navMeshAgent);
            animator.SetTrigger("die");
            deathPosition = transform.position; // Сохранение позиции смерти босса
            Vector3 keyDropPosition = deathPosition - Vector3.up; // Позиция, где ключ выпадет ниже босса
            Instantiate(droppedItemPrefab, keyDropPosition, Quaternion.identity); // Создание ключа
            Instantiate(droppedItemPrefab, deathPosition, Quaternion.identity);
            Destroy(gameObject, 3);
        }
    }

    // Задержка атаки 
    private IEnumerator AttackDelay()
    {
        canAttack = false; // Запрещаем атаку
        yield return new WaitForSeconds(attackInterval); // Ждем заданное время
        canAttack = true; // Разрешаем атаку
        Shot();
    }


    public void Shot()
    {
        RaycastHit hit;
        if (Physics.Raycast(barel.position, barel.forward, out hit, 2)) // Изменена дистанция на 2 (У зомби 3)
        {
            if (hit.transform != null)
            {
                var player = hit.transform.GetComponent<Player>();
                if (player != null)
                {
                    player.GameOverPlayer();
                    HPManeger.score -= 40;
                }
            }
        }
    }

    // Нанесение урона боссу
    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Kill();
        }
    }
}
