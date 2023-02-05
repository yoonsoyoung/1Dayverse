using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;

public class CharacterPick : MonoBehaviour
{
    public string onScreen = "start";
    public string username;
    public int character;
    public Button StopBtn;
    public Button GoBtn;
    private string currentUsername;
    private Dictionary<string, int> characterDic = new Dictionary<string, int>();
   
  
    [DllImport("__Internal")]
    private static extern string GetUsername();


    // Start is called before the first frame update
    void Start()
    {
        currentUsername = GetUsername();
        StopBtn.onClick.AddListener(StopBtnClick);
        GoBtn.onClick.AddListener(GoBtnClick);

        characterDic["SchoolBoy"] = 1;
        characterDic["GamerGirl"] = 2;
        characterDic["Gangster"] = 3;
        characterDic["Jock"] = 4;
        characterDic["SchoolGirl"] = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.transform.gameObject.name);
                Debug.Log("onscreen" + onScreen);
                if (onScreen != hit.transform.gameObject.name)
                {
                    if (onScreen != "start") {
                        GameObject.Find("OnePick").transform.Find($"{onScreen}").gameObject.SetActive(false);
                    }
                    
                    GameObject.Find("OnePick").transform.Find($"{hit.transform.gameObject.name}").gameObject.SetActive(true);
                    onScreen = hit.transform.gameObject.name;
                }
               
            }
        }
    }

    private void StopBtnClick()
    {
        if (onScreen != "start")
        {
            GameObject.Find("OnePick").transform.Find($"{onScreen}").gameObject.SetActive(false);
        }               
        GameObject.Find("ShowMyInfo").transform.Find("MyInfoCanvas").gameObject.SetActive(true);
        GameObject.Find("CharacterChange").transform.Find("CharacterCanvas").gameObject.SetActive(false);
        GameObject.Find("CharacterChange").transform.Find("ControlCharacters").gameObject.SetActive(false);
    }

    public IEnumerator SendInfo(string username, int character)
    {
        WWWForm form = new WWWForm();
        form.AddField("nickname", username);
        form.AddField("character", character);

        UnityWebRequest request = UnityWebRequest.Post("https://k5c202.p.ssafy.io/od/unitys/character/", form);
        yield return request.SendWebRequest();
        Debug.Log(request.result);


    }

    private void GoBtnClick()
    {
        if (onScreen != "start")
        {
            GameObject.Find("OnePick").transform.Find($"{onScreen}").gameObject.SetActive(false);
        }

        username = currentUsername;
        character = characterDic[onScreen];
        

        StartCoroutine(SendInfo(username, character));
        SceneManager.LoadScene("MyPage");

        //GameObject.Find("ShowMyInfo").transform.Find("MyInfoCanvas").gameObject.SetActive(true);
        //GameObject.Find("CharacterChange").transform.Find("CharacterCanvas").gameObject.SetActive(false);
        //GameObject.Find("CharacterChange").transform.Find("ControlCharacters").gameObject.SetActive(false);

    }
}
