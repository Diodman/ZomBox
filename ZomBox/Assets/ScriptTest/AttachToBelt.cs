using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachToBelt : MonoBehaviour
{
    public string beltTag = "Gun"; // Тег объекта пояса
    public float attachDistance = 0.2f; // Расстояние для крепления объекта к поясу

    private Transform beltTransform; // Трансформ объекта пояса
    private Rigidbody rb; // Rigidbody объекта, который нужно крепить

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        AttachObjectToBelt();
    }

    void AttachObjectToBelt()
    {
        // Найти объект пояса по тегу
        GameObject belt = GameObject.FindGameObjectWithTag(beltTag);

        if (belt != null)
        {
            beltTransform = belt.transform;

            // Проверить расстояние между объектом и поясом
            float distance = Vector3.Distance(transform.position, beltTransform.position);

            if (distance <= attachDistance)
            {
                // Крепим объект к поясу
                AttachToBeltPosition();
            }
        }
    }

    void AttachToBeltPosition()
    {
        // Устанавливаем позицию и вращение объекта по отношению к поясу
        transform.position = beltTransform.position;
        transform.rotation = beltTransform.rotation;

        // Отключаем физические свойства объекта
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true; // Делаем объект кинематическим (не подверженным физике)
    }
}
