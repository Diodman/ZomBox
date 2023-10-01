using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Valve.VR.InteractionSystem;

public class ZombieAI : MonoBehaviour
{
    public Transform barel;

    // Переменные для скорости движения и поворота зомби
    public float moveSpeed = 2.0f;
    public float rotationSpeed = 2.0f;

    // Расстояния для обнаружения игрока, атаки и спрятывания
    public float detectionRange = 10.0f;
    public float attackRange = 2.0f;
    public float hideCooldown = 5.0f;
    public float hideDistance = 5.0f;

    // Слой, на котором находятся препятствия
    public LayerMask obstacleMask;

    // Цель (игрок)
    private Transform target;

    // Флаг, показывающий, находится ли зомби в укрытии
    private bool isHiding = false;

    // Время следующей попытки спрятаться
    private float nextHideTime;

    private Animator animator;  // Ссылка на компонент Animator
    private CharacterController characterController;

    private void Start()
    {
        // Находим игрока в сцене
        GameObject player = GameObject.FindGameObjectWithTag("Player1");

        if (player != null)
        {
            target = player.transform;
        }
        // Получаем компонент Animator
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (target == null)
            return;

        float distance = Vector3.Distance(transform.position, target.position);
        Vector3 dir = target.position - transform.position;
        dir.y = 0;

        if (distance <= detectionRange)
        {
            // Если зомби спрятано, оно не атакует и не двигается
            if (isHiding)
                return;

            //Vector3 dir = target.position - transform.position;
            //dir.y = 0;
            Quaternion newRotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, rotationSpeed * Time.deltaTime);

            if (distance > attackRange)
            {
                transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            }
            else
            {
                // Атака игрока (вы можете добавить свою логику атаки)
                animator.SetTrigger("attack");
                Shot();
            }
        }
        else if (!isHiding && Time.time > nextHideTime)
        {
            // Зомби не видит игрока и не прячется, пытается спрятаться
            TryHide();
        }
        if (distance > attackRange)
        {
            // Поворот к цели
            Quaternion newRotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, rotationSpeed * Time.deltaTime);

            // Проверка наличия препятствий перед зомби
            if (!isHiding && !HasObstacleInFront())
            {
                // Запускаем анимацию бега
                animator.SetBool("isRunning", true);

                // Перемещаем зомби
                characterController.Move(transform.forward * moveSpeed * Time.deltaTime);
            }
            else
            {
                // Останавливаем анимацию бега
                animator.SetBool("isRunning", false);
            }
        }
        else
        {
            // Останавливаем анимацию бега
            animator.SetBool("isRunning", false);

            // Атака игрока (вы можете добавить свою логику атаки)
            if (!isHiding)
            {
                // Запускаем анимацию атаки
                animator.SetTrigger("attack");
                Shot();
            }
        }
    }

    private bool HasObstacleInFront()
    {
        RaycastHit hit;
        Vector3 rayDirection = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, rayDirection, out hit, 1.5f, obstacleMask))
        {
            return true;
        }

        return false;
    }

    public void Shot()
    {
        RaycastHit hit;
        if (Physics.Raycast(barel.position, barel.forward, out hit, 3))
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

    private void TryHide()
    {
        // Генерируем случайную позицию для спрятывания
        Vector3 randomDirection = Random.insideUnitSphere * hideDistance;
        randomDirection += transform.position;
        randomDirection.y = transform.position.y;

        RaycastHit hit;

        // Проверяем, есть ли препятствие на пути к укрытию
        if (Physics.Raycast(transform.position, randomDirection - transform.position, out hit, hideDistance, obstacleMask))
        {
            return;
        }

        // Зомби перемещается к укрытию
        

        // Устанавливаем задержку перед следующей попыткой спрятаться
        nextHideTime = Time.time + hideCooldown;
        isHiding = true;

        // После некоторого времени зомби выходит из укрытия
        Invoke("StopHiding", hideCooldown);
    }

    private void StopHiding()
    {
        isHiding = false;
    }

    private void Die()
    {
        // Запускаем анимацию смерти
        animator.SetTrigger("die");

        // Реакция на смерть (например, уничтожение объекта)
        Destroy(gameObject, 3.0f);
    }
}
