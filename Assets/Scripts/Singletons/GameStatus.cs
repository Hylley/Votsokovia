using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatus : MonoBehaviour
{
    public static GameStatus instance { get; private set; }
    
    // Status
    public int health = 100;
    public int sickles;
    public int jellies;
    public float distance;

    void Start()
    {
        if(instance != null)
        {
            Destroy(this);
            return;        
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void TakeDamage(int damage)
    {
        if(health - damage <= 0)
        {
            GameOver();
            return;
        }

        health -= damage;
    }

    void GameOver()
    {
        GameStatus.instance.jellies++;
        Debug.Log("Game over");
    }
}
