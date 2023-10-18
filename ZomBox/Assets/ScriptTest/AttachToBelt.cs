using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachToBelt : MonoBehaviour
{
    public string beltTag = "Gun"; // ��� ������� �����
    public float attachDistance = 0.2f; // ���������� ��� ��������� ������� � �����

    private Transform beltTransform; // ��������� ������� �����
    private Rigidbody rb; // Rigidbody �������, ������� ����� �������

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
        // ����� ������ ����� �� ����
        GameObject belt = GameObject.FindGameObjectWithTag(beltTag);

        if (belt != null)
        {
            beltTransform = belt.transform;

            // ��������� ���������� ����� �������� � ������
            float distance = Vector3.Distance(transform.position, beltTransform.position);

            if (distance <= attachDistance)
            {
                // ������ ������ � �����
                AttachToBeltPosition();
            }
        }
    }

    void AttachToBeltPosition()
    {
        // ������������� ������� � �������� ������� �� ��������� � �����
        transform.position = beltTransform.position;
        transform.rotation = beltTransform.rotation;

        // ��������� ���������� �������� �������
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true; // ������ ������ �������������� (�� ������������ ������)
    }
}
