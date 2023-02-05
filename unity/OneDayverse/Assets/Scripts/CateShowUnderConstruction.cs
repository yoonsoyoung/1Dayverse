using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CateShowUnderConstruction : MonoBehaviour
{
    public Canvas UnderConstructionText;
    private void OnMouseEnter()
    {
        UnderConstructionText.enabled = true;
    }

    private void OnMouseOver()
    {
        UnderConstructionText.enabled = true;
    }

    private void OnMouseExit()
    {
        UnderConstructionText.enabled = false;
    }
}
