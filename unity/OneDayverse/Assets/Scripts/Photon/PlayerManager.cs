 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine.Networking;

public class PlayerManager : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern string GetUsername();

    PhotonView PV;

    [Serializable]
    public class Profile
    {
        public string id;
        public string username;
        public string birth_day;
        public string phone_number;
        public int character;
        public string profile_image;
        public string teachable;
    }

    public int charnum;
    public string currentUsername;

    GameObject controller;
    // Start is called before the first frame update
    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    void Start()
    {
        currentUsername = GetUsername();
        // currentUsername = "oxoxo";
        StartCoroutine(GetUserInfo());
    }

    public IEnumerator GetUserInfo()
    {
        UnityWebRequest userInforequest;
        using (userInforequest = UnityWebRequest.Get("https://k5c202.p.ssafy.io/od/unitys/profile/" + currentUsername + "/"))
        {
            yield return userInforequest.SendWebRequest();

            Profile profileJson = JsonUtility.FromJson<Profile>(userInforequest.downloadHandler.text);
        
            charnum = profileJson.character;
        }
        if (PV.IsMine)
        {
            CreateController();
        }
    }
    // Update is called once per frame
    void CreateController()
    {   

        // Debug.Log("플레이어 컨트롤러 생성");
        Transform spawnpoint = SpawnManager.Instance.GetSpawnpoint(4);
        // PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController"), Vector3.zero, Quaternion.identity);
        controller = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", charnum.ToString()), spawnpoint.position,  spawnpoint.rotation, 0, new object[] {PV.ViewID});

    }

    void CreateLargeClassController()
    {       
        // Debug.Log("플레이어 컨트롤러 생성");
        Transform spawnpoint =SpawnManager.Instance.GetSpawnpoint(4);
        // PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController"), Vector3.zero, Quaternion.identity);
        controller = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController"), spawnpoint.position,  spawnpoint.rotation, 0, new object[] {PV.ViewID});

    }

    public void Die()
    {
        PhotonNetwork.Destroy(controller);
        CreateController();
    }

    public void goToLargeClass()
    {
        PhotonNetwork.Destroy(controller);
        CreateLargeClassController();
    }
}
