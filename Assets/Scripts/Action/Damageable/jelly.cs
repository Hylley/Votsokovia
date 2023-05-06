using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jelly : MonoBehaviour, IDamageable
{
    public int health;

    public void TakeDamage(int damage)
    {
        Debug.Log("Health: " + health);
        
        if(health - damage <= 0)
        {
            Die();
            return;
        }

        health -= damage;
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
