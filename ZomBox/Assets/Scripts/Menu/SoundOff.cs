using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Valve.VR.InteractionSystem;
using Valve.VR;
using System.Collections;
using UnityEngine.SceneManagement;

public class SoundOff : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Color normalColor = Color.white; // ���������� ���� ������
    private Color hoverColor = Color.blue; // ���� ��� ��������� �� ������
    private Color pressedColor = Color.green; // ���� ��� ������� �� ������

    private bool isHovering = false; // ���� ��� ������������ ��������� �� ������

    private Hand hand; // ����������

    private Image buttonImage; // ����������� ������

    public AudioSource AK;
    public AudioSource Pistol;
    public AudioSource Zombie;
    public GameObject SoundON;
    public GameObject SoundOFF;
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
            AK.mute = false;
            Pistol.mute = false;
            Zombie.mute = false;
            SoundON.SetActive(true);
            SoundOFF.SetActive(false);
            Debug.Log("SoundON");
        }
    }

}