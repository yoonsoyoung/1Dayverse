using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class GetUserData : MonoBehaviour
{
    public string currentUsername;
    [DllImport("__Internal")]
    private static extern string GetUsername();

    [DllImport("__Internal")]
    private static extern void Hello();


    private void Awake()
    {
        // currentUsername = GetUsername();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
