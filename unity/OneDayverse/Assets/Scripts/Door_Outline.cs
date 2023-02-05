using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Outline : MonoBehaviour
{
    public Material Door_outline_before;
    public Material Door_outline_after;

    private void OnMouseEnter()
    {
        Material[] material = gameObject.GetComponent<MeshRenderer>().materials;
        material[0] = Door_outline_after;
        gameObject.GetComponent<MeshRenderer>().materials = material;
    }

    private void OnMouseOver()
    {
        Material[] material = gameObject.GetComponent<MeshRenderer>().materials;
        material[0] = Door_outline_after;
        gameObject.GetComponent<MeshRenderer>().materials = material;
    }

    private void OnMouseExit()
    {
        Material[] material = gameObject.GetComponent<MeshRenderer>().materials;
        material[0] = Door_outline_before;
        gameObject.GetComponent<MeshRenderer>().materials = material;
    }
}