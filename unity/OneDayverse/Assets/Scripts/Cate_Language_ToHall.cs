using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;

public class Cate_Language_ToHall : MonoBehaviour
{

    [DllImport("__Internal")]
    private static extern string GetUsername();

    GameObject Player;
    string currentUsername;
    GameObject LobbyTarget;
    public Button LoungeButton;

    void Start()
    {
        currentUsername = GetUsername();
        // currentUsername = "oxoxo";
        Player = GameObject.Find(currentUsername);
        LobbyTarget = GameObject.Find("LobbySpawnpoint");
        if (LoungeButton != null)
        {
            LoungeButton.onClick.AddListener(toLobby);
        }
    }

    private void toLobby()
    {
        Player.transform.position = LobbyTarget.transform.position;

    }
}
