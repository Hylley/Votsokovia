using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sickleCollision : MonoBehaviour
{
    public sickle parent;

    void OnTriggerEnter(Collider hit)
    {
        parent.HandleCollision(hit.gameObject);
    }
}
