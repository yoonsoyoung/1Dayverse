using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using System.Runtime.InteropServices;

public class PhotonCreate : MonoBehaviourPunCallbacks
{   
    public static PhotonCreate Instance;
    [SerializeField] TMP_InputField roomNameInputField;
    [SerializeField] TMP_Text errorText;
    [SerializeField] TMP_Text roomNameText;
    [SerializeField] Transform roomListContent;
    [SerializeField] GameObject roomListItemPrefab;
    [SerializeField] Transform playerListContent;
    [SerializeField] GameObject PlayerListItemPrefab;
    [SerializeField] GameObject startGameButton;

    GameObject loadingText;

        
    [DllImport("__Internal")]
    private static extern string GetUsername();    

    void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {      
        if(PhotonNetwork.IsConnected)
        {   
            Debug.Log("연결 끊기");
            PhotonNetwork.Disconnect();
        }
        Debug.Log("연결 시도");
        PhotonNetwork.ConnectUsingSettings();
        loadingText = GameObject.Find("Loading");
    }

    public override void OnConnectedToMaster()
    {   
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
        Debug.Log("연결 완료");
    }

    public override void OnJoinedLobby()
    {   
        Debug.Log("로비 접속");

        // MenuManager.Instance.OpenMenu("title");
        // PhotonNetwork.NickName = "ox1212";
        PhotonNetwork.NickName = GetUsername();
        Debug.Log(PhotonNetwork.NickName);

        loadingText.SetActive(false);
    }

    // Update is called once per frame
    public void CreateRoom()
    {   
        // if (string.IsNullOrEmpty(roomNameInputField.text)) // 안 써놓으면 룸 네임이 랜덤으로 생성
        // {
        //     return;
        // }
        PhotonNetwork.CreateRoom("Exercise"); 
        // MenuManager.Instance.OpenMenu("loading");
    }
    public override void OnJoinedRoom()
    {
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        // base.OnMasterClientSwitched(newMasterClient);
        // startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {   
        // errorText.text = "방 만들기 실패" + message;
        // MenuManager.Instance.OpenMenu("error");
    }
    public void StartGame()
    {
        PhotonNetwork.LoadLevel(2);
    }
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        // MenuManager.Instance.OpenMenu("loading");
    }

    public void JoinRoom(RoomInfo info)
    {   
        // RoomOptions RO = new RoomOptions ();
        // RO.MaxPlayers = byte.Parse("5");
        // PhotonNetwork.JoinOrCreateRoom("k", RO,TypedLobby.Default);

        PhotonNetwork.JoinRoom(info.Name);
        // MenuManager.Instance.OpenMenu("loading");
    }
    public override void OnLeftRoom()
    {
        // MenuManager.Instance.OpenMenu("title");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {   
        // foreach(Transform trans in roomListContent)
        // {
        //     Destroy(trans.gameObject);
        // }
        // for (int i = 0; i < roomList.Count; i++)
        // {
        //     if(roomList[i].RemovedFromList)
        //         continue;
        //     Instantiate(roomListItemPrefab, roomListContent).GetComponent<RoomListItem>().SetUp(roomList[i]);
        // }
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        // Instantiate(PlayerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
    }

    public void CreateExerciseRoom()
    {   
        RoomOptions ros = new RoomOptions();
        ros.MaxPlayers = 15;

        PhotonNetwork.CreateRoom("Exercise", ros);
    }

    public void StartExerciseRoom()
    {
        PhotonNetwork.LoadLevel(2);
    }
    public void CreateArtRoom()
    {        RoomOptions ros = new RoomOptions();
        ros.MaxPlayers = 15;
        PhotonNetwork.CreateRoom("Art", ros);
    }

    public void StartArtRoom()
    {
        PhotonNetwork.LoadLevel(3);
    }
    public void CreateLanguageRoom()
    {
        RoomOptions ros = new RoomOptions();
        ros.MaxPlayers = 15;
        PhotonNetwork.CreateRoom("Language", ros);
    }

    public void StartLanguageRoom()
    {
        PhotonNetwork.LoadLevel(4);
    }

    public void CreateLoungeRoom()
    {
        RoomOptions ros = new RoomOptions();
        ros.MaxPlayers = 30;
        PhotonNetwork.CreateRoom("Lounge", ros);
    }

    public void StartLoungeRoom()
    {
        PhotonNetwork.LoadLevel(6);
    }
    
}
