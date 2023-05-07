using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jelly : MonoBehaviour, IDamageable
{
    public int health;
    public healthWorldSpaceUI healthBar;

    void Start()
    {
        healthBar.slider.maxValue = health;
        healthBar.slider.value = health;
    }

    public void TakeDamage(int damage)
    {   
        if(health - damage <= 0)
        {
            Die();
            return;
        }

        health -= damage;
        healthBar.slider.value = health;
        healthBar.Shake();
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
