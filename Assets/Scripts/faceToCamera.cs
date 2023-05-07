using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class faceToCamera : MonoBehaviour
{
    public Transform _camera;

    void Update()
    {
        transform.forward = _camera.forward;
        //transform.LookAt(_camera.position);        
    }
}
