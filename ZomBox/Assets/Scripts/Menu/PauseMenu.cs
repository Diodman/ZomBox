using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PauseMenu : MonoBehaviour
{

    public void StartBtn()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
