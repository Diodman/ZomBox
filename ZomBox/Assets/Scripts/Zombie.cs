using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Valve.VR.InteractionSystem;

public class Zombie : MonoBehaviour
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
    private bool canAttack = true;
    private bool canSpawn = true;
    public GameObject spawn1;
    public GameObject spawn2;
    public GameObject spawn3;

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
        if(dead || (canSpawn == false))
        {
            return;
        }

        if (ScoreManeger.score >= 10)
        {
            canSpawn = false;
            Kill();
            spawn1.SetActive(false);
            spawn2.SetActive(false);
            spawn3.SetActive(false);
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= stoppingDistance)
        {
            navMeshAgent.isStopped = true; // Останавливаем NavMeshAgent
            animator.SetBool("isRunning", false);
        }
        else
        {
            navMeshAgent.isStopped = false; // Возобновляем движение, если расстояние больше stoppingDistance
            navMeshAgent.SetDestination(player.transform.position);
            animator.SetBool("isRunning", true);
        }

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
    public void Kill()
    {
            if (!dead)
            {
                dead = true;
                Destroy(capsuleCollider);
                Destroy(navMeshAgent);
                animator.SetTrigger("die");
                Destroy(gameObject, 5);
            }
    }

    private IEnumerator AttackDelay()
    {
        canAttack = false; // Запрещаем атаку
        yield return new WaitForSeconds(attackInterval); // Ждем заданное время
        canAttack = true; // Разрешаем атаку
        Shot();
    }
    public void Shot()
    {
        animator.SetTrigger("attack");
        RaycastHit hit;
        if (Physics.Raycast(barel.position, barel.forward, out hit, 1))
        {
            if (hit.transform != null)
            {
                var player = hit.transform.GetComponent<Player>();
                if (player != null)
                {
                    player.GameOverPlayer();
                    HPManeger.score -= 25;
                }
            }
        }
    }
}
