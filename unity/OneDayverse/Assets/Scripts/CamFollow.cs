using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class CamFollow : MonoBehaviour
{

    GameObject target;
    Vector3 target_transform;
    public Vector3 offset;
    string currentUsername;

    [DllImport("__Internal")]
    private static extern string GetUsername();

    // Update is called once per frame
    void Update()
    {
        currentUsername = GetUsername();
        // currentUsername = "oxoxo";
        target = GameObject.Find(currentUsername);
       

        if (target != null)
        {
            target_transform = target.transform.position;
            transform.position = Vector3.Lerp(transform.position, (target_transform + offset), 5f);
            transform.LookAt(target.transform);
            transform.Rotate(-20, 0, 0);
        }
    }
}
