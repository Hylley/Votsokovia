using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class faceToCamera : MonoBehaviour
{
    public Transform target;

    void Update()
    {
        // transform.forward = target.position;

        transform.LookAt(target.position);        
    }
}
