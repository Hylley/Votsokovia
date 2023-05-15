using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disableAtKeyPress : MonoBehaviour
{
    public KeyCode disableKey;

    void Update()
    {
        if(!Input.GetKeyDown(disableKey))
            return;
        
        gameObject.SetActive(false);
    }
}
