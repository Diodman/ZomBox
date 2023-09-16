using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class DranGun : MonoBehaviour
{
    public Transform barel;
    private Interactable interactable;

    private void Start()
    {
        interactable = GetComponent<Interactable>();
    }

    private void Update()
    {
                Fire();
    }

    private void Fire()
    {
        RaycastHit hit;
        if (Physics.Raycast(barel.position, barel.forward, out hit, 25))
        {
            if (hit.transform != null)
            {
                var zombie = hit.transform.GetComponent<Zombie>();
                if (zombie != null)
                {
                    zombie.Kill();
                    ScoreManeger.score += 5;
                }
            }
        }
    }
}
