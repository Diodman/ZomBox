using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public void TakeDamage(int damage)
    {
        var zombie = GetComponent<Zombie>();
        zombie.Kill();
        ScoreManeger.score += 5;
    }
}
