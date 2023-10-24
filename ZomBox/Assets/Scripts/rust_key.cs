using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class rust_key : MonoBehaviour
{
    private bool hasKey = false; // Флаг, указывающий, взял ли игрок ключ.
    public void SetHasKey(bool value)
    {
        hasKey = value;
    }
    public bool HasKey()
    {
        return hasKey;
    }
}
