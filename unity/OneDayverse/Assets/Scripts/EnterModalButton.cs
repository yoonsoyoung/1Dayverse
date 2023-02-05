using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class EnterModalButton : MonoBehaviourPunCallbacks
{
    GameObject CateCanvas;

    public void LeaveExerciseRoom()
    {   
        CateCanvas = GameObject.Find("CateCanvas");
        Transform ExerciseEnterModal = CateCanvas.transform.Find("ExerciseEnterModal"); 
        ExerciseEnterModal.gameObject.SetActive(false);
        PhotonNetwork.LeaveRoom();
    }
    public void StartExerciseRoom()
    {
        PhotonNetwork.LoadLevel(2);
    }

    public void LeaveArtRoom()
    {   
        CateCanvas = GameObject.Find("CateCanvas");
        Transform ArtEnterModal = CateCanvas.transform.Find("ArtEnterModal"); 
        ArtEnterModal.gameObject.SetActive(false);
        PhotonNetwork.LeaveRoom();
    }
    public void StartArtRoom()
    {
        PhotonNetwork.LoadLevel(3);
    }
    public void LeaveLanguageRoom()
    {   
        CateCanvas = GameObject.Find("CateCanvas");
        Transform LanguageEnterModal = CateCanvas.transform.Find("LanguageEnterModal"); 
        LanguageEnterModal.gameObject.SetActive(false);
        PhotonNetwork.LeaveRoom();
    }
    public void StartLanguageRoom()
    {
        PhotonNetwork.LoadLevel(4);
    }

    public void LeaveLoungeRoom()
    {   
        CateCanvas = GameObject.Find("CateCanvas");
        Transform LoungeEnterModal = CateCanvas.transform.Find("LoungeEnterModal"); 
        LoungeEnterModal.gameObject.SetActive(false);
        PhotonNetwork.LeaveRoom();
    }
    public void StartLoungeRoom()
    {
        PhotonNetwork.LoadLevel(6);
    }
}
