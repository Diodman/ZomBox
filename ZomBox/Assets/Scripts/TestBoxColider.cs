using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBoxColider : MonoBehaviour
{
    public float damage = 50;
    public float fireRate = 1;
    public float range = 15;
    public GameObject muzzleFlash;
    public AudioClip shotSFX;
    public AudioSource _audioSource;

    public Camera _cam;


    // Update is called once per frame
    void Update()
    {
            Shoot();
    }

    void Shoot()
    {
        RaycastHit hit;

        if (Physics.Raycast(_cam.transform.position, _cam.transform.forward, out hit, range))
        {
            if (hit.transform != null)
            {
                var zombie = hit.transform.GetComponent<Zombie>();
                if (zombie != null)
                    zombie.Kill();
            }
        }
    }
}
