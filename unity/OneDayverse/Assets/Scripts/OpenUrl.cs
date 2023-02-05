using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenUrl : MonoBehaviour
{
    void OnMouseDown()
    {
        Application.OpenURL("https://iogames.space/skribbl-io");
    }
}
