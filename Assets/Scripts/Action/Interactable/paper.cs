using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class paper : MonoBehaviour, IInteractable
{
    bool locked = true;
    public int cost;
    public GameObject show;
    public GameObject lockObject;
    public TextMeshProUGUI costUI;
    public AudioSource sound;
    
    void Start()
    {
        costUI.text = cost.ToString();
    }

    public void Interact()
    {
        if(locked)
        {
            if(GameStatus.instance.rocks - cost < 0)
                return;
            locked = false;
            GameStatus.instance.rocks -= cost;
            show.SetActive(true);
            Destroy(lockObject);
            GameStatus.instance.clearAltar();
            sound.Play();

            return;
        }

        sound.Play();
        show.SetActive(true);
    }
}
