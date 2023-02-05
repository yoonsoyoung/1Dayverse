using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayVolume : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
