using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HPUI : MonoBehaviour
{
    private TextMeshProUGUI _field;
    private int _score = 100;

    private void Awake()
    {
        _field = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        _field.text = _score.ToString();
    }

    public void IncreaseScore()
    {
        _score -= 5;
        _field.text = _score.ToString();
    }
}
