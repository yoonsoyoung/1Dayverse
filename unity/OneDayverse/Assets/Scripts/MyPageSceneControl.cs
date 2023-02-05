using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using System.Runtime.InteropServices;

public class MyPageSceneControl : MonoBehaviour
{

    public Button ShowCharacterBtn;
    public Button ShowPrevSceneBtn;
    public string prevSceneName;
    string currentUsername;

    [DllImport("__Internal")]
    private static extern string GetUsername();

    // Start is called before the first frame update
    void Start()
    {
        prevSceneName = DontDestroyObject.instance.prevScene;
        ShowCharacterBtn.onClick.AddListener(ShowCharacterBtnClick);
        ShowPrevSceneBtn.onClick.AddListener(ShowPrevSceneBtnClick);
    }

    public void ShowPrevSceneBtnClick()
    {
        currentUsername = GetUsername();
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("Main");
    }

    // Update is called once per frame
    private void ShowCharacterBtnClick()
    {
        GameObject.Find("ShowMyInfo").transform.Find("MyInfoCanvas").gameObject.SetActive(false);
        GameObject.Find("CharacterChange").transform.Find("CharacterCanvas").gameObject.SetActive(true);
        GameObject.Find("CharacterChange").transform.Find("ControlCharacters").gameObject.SetActive(true);
    }



}
