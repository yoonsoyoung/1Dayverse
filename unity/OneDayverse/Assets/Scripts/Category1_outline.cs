using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Category1_outline : MonoBehaviour
{
    public Material Category1_outline_before;
    public Material Category1_outline_after;

    private void OnMouseEnter()
    {
        Material[] material = gameObject.GetComponent<MeshRenderer>().materials;
        material[2] = Category1_outline_after;
        gameObject.GetComponent<MeshRenderer>().materials = material;
    }

    private void OnMouseOver()
    {
        Material[] material = gameObject.GetComponent<MeshRenderer>().materials;
        material[2] = Category1_outline_after;
        gameObject.GetComponent<MeshRenderer>().materials = material;
    }

    private void OnMouseExit()
    {
        Material[] material = gameObject.GetComponent<MeshRenderer>().materials;
        material[2] = Category1_outline_before;
        gameObject.GetComponent<MeshRenderer>().materials = material;
    }
}