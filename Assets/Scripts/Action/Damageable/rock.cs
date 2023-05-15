using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rock : MonoBehaviour, IDamageable
{
    Animator anim;

    public int health;
    public healthWorldSpaceUI healthBar;
    public AudioSource damageSound;

    void Start()
    {
        anim = GetComponent<Animator>();

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
        anim.Play("hit");
        damageSound.Play();
    }

    void Die()
    {
        GameStatus.instance.rocks++;
        Destroy(gameObject);
    }
}
