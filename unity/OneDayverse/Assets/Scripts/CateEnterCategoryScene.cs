using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class CateEnterCategoryScene : MonoBehaviour
{
    GameObject CateCanvas;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RoomOptions RO = new RoomOptions();
            RO.MaxPlayers = 30;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;


            if (Physics.Raycast(ray, out hit))
            {
                CateCanvas = GameObject.Find("CateCanvas");              
                if (hit.transform.gameObject.name == "Category1")
                {   
                    Transform ExerciseEnterModal = CateCanvas.transform.Find("ExerciseEnterModal"); 
                    ExerciseEnterModal.gameObject.SetActive(true);
                    PhotonNetwork.JoinOrCreateRoom("Exercise", RO, TypedLobby.Default);
                } else if (hit.transform.gameObject.name == "Category2")
                {
                    Transform ArtEnterModal = CateCanvas.transform.Find("ArtEnterModal"); 
                    ArtEnterModal.gameObject.SetActive(true);
                    PhotonNetwork.JoinOrCreateRoom("Art", RO, TypedLobby.Default);
                }
                else if (hit.transform.gameObject.name == "Category3")
                {
                    Transform LanguageEnterModal = CateCanvas.transform.Find("LanguageEnterModal"); 
                    LanguageEnterModal.gameObject.SetActive(true);
                    PhotonNetwork.JoinOrCreateRoom("Language", RO, TypedLobby.Default);
                }               
                else if (hit.transform.gameObject.name=="Fountain")
                {   
                    Transform LanguageEnterModal = CateCanvas.transform.Find("LoungeEnterModal"); 
                    LanguageEnterModal.gameObject.SetActive(true);
                    PhotonNetwork.JoinOrCreateRoom("Lounge", RO, TypedLobby.Default);
                    
                    
                }


            }
        }
    }
}
