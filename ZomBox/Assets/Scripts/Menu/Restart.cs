using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using Valve.VR.InteractionSystem;
using Valve.VR;
using System.Collections;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Color normalColor = Color.white; // ���������� ���� ������
    private Color hoverColor = Color.blue; // ���� ��� ��������� �� ������
    private Color pressedColor = Color.green; // ���� ��� ������� �� ������

    private bool isHovering = false; // ���� ��� ������������ ��������� �� ������

    private Hand hand; // ����������

    private Image buttonImage; // ����������� ������

    public GameObject player;


    private void Start()
    {
        buttonImage = GetComponent<Image>();

        hand = GetComponentInParent<Hand>(); // �������� ���������� �� ������������� �������
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // �������� ���� ������ ��� ��������� �� ��
        isHovering = true;
        buttonImage.color = hoverColor;

        // ��������� �������� ��� �������� ������� ������
        StartCoroutine(ButtonDelay());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // ���������� ���� ������ � ����������� �����, ���� ���� �������� ������� ������
        isHovering = false;
        buttonImage.color = normalColor;

        // ������������� ���������� ��������, ���� ������ ������� ������� ������
        StopCoroutine(ButtonDelay());
    }

    private IEnumerator ButtonDelay()
    {
        // ���� ��� �������
        yield return new WaitForSeconds(0f);

        // ���������, ������� �� ������ �� ������
        if (isHovering)
        {
            // ��������� ������� ������
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
            Destroy(player);
            Debug.Log("Restart");
        }
    }
}