using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{   
    Camera cam;
    // private Transform cam;
    void Update()
    {
        if(cam == null)
            cam = FindObjectOfType<Camera>();
        if (cam == null)
            return;
        
        transform.LookAt(cam.transform);
        transform.Rotate(Vector3.up * 180);
    }

    // void Start()
    // {
    //     cam = Camera.main.transform;
    // }

    // void Update()
    // {
    //     transform.LookAt(transform.position);
    //     transform.Rotate(0,180,0);
    // }


}
