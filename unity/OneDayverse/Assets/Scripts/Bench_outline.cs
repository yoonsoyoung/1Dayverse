using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
public class Bench_outline : MonoBehaviour
{
    public Material Bench_outline_before;
    public Material Bench_outline_after;

    private void OnMouseEnter()
    {
        Material[] material = gameObject.GetComponent<MeshRenderer>().materials;
        material[0] = Bench_outline_after;
        gameObject.GetComponent<MeshRenderer>().materials = material;
    }

    private void OnMouseOver()
    {
        Material[] material = gameObject.GetComponent<MeshRenderer>().materials;
        material[0] = Bench_outline_after;
        gameObject.GetComponent<MeshRenderer>().materials = material;
    }

    private void OnMouseExit()
    {
        Material[] material = gameObject.GetComponent<MeshRenderer>().materials;
        material[0] = Bench_outline_before;
        gameObject.GetComponent<MeshRenderer>().materials = material;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.name == "Bench")
                {   
                    RoomOptions RO = new RoomOptions();
                    // SceneManager.LoadScene("Lounge");
                    PhotonNetwork.JoinOrCreateRoom("Lounge", RO, TypedLobby.Default);
                    PhotonNetwork.LoadLevel(6);
                    
                }

            }
        }
    }
}