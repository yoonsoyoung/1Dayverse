using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using System.Runtime.InteropServices;

public class Network : MonoBehaviour
{

    [DllImport("__Internal")]
    private static extern string GetUsername();

    GameObject Player;
    string currentUsername;
    GameObject LobbyTarget;

    private void Start()
    {
        currentUsername = GetUsername();
        // currentUsername = "oxoxo";
        
        LobbyTarget = GameObject.Find("LobbySpawnpoint");
    }
    public void ToLobby()
    {
        Player.transform.position = LobbyTarget.transform.position;

    }
    public void ToCate()
    {
        PhotonNetwork.LeaveRoom();
        Player = GameObject.Find(currentUsername);
        Destroy(Player.transform.parent.gameObject);
        SceneManager.LoadScene("Main");
    }
}
