using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class MenuController : MonoBehaviour
{

    public void StartBtn()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
