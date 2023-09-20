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

    private Dictionary<GameObject, int> hitCountDictionary;

    private float attackTimer = 0f;
    public float attackInterval = 2f;
    private bool canAttack = true; // Флаг, позволяющий атаковать

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
        if (dead)
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
    public void Kill()
    {
            if (!dead)
            {
                dead = true;
                Destroy(capsuleCollider);
                Destroy(navMeshAgent);
                animator.SetTrigger("dead");
                Destroy(gameObject, 5);
            }
    }

    //private IEnumerator Prov()
    //{
    //        yield return new WaitForSeconds(fireRate); // Ждем заданное время
    //}
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
        if (Physics.Raycast(barel.position, barel.forward, out hit, 3)) // Попробовать поменять 1 на что-то другое, т.к. это луч, и если луч не попадает в игрока, урон не регистрирует.
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
