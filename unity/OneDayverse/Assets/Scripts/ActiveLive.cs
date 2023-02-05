using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class ActiveLive : MonoBehaviour
{
    [DllImport("__Internal")] private static extern void goLive();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            #if UNITY_WEBGL
                goLive();
            #endif
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Room")
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                #if UNITY_WEBGL
                    goLive();
                #endif
            }
        }
    }
}
