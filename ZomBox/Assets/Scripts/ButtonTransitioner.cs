using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Valve.VR.InteractionSystem;
using Valve.VR;
using System.Collections;

public class ButtonTransitioner : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Color normalColor = Color.white; // ���������� ���� ������
    private Color hoverColor = Color.blue; // ���� ��� ��������� �� ������
    private Color pressedColor = Color.green; // ���� ��� ������� �� ������
    public GameObject objectToHide;
    public GameObject objectToShow;
    private bool isHovering = false; // ���� ��� ������������ ��������� �� ������

    private Hand hand; // ����������

    private Image buttonImage; // ����������� ������

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
        yield return new WaitForSeconds(3f);

        // ���������, ������� �� ������ �� ������
        if (isHovering)
        {
            // ��������� ������� ������
            objectToHide.SetActive(false);
            // ������ ������ToShow ��������
            objectToShow.SetActive(true);
            Debug.Log("Developers");
        }
    }
}