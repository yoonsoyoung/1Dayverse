using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;
using UnityEngine.Networking;



public class CharacterImage : MonoBehaviour
{

    [DllImport("__Internal")]
    private static extern string GetUsername();

    private string currentUsername;

    private Dictionary<int, Sprite> characterDic = new Dictionary<int, Sprite>();
    public Image profileImage;

    [SerializeField]
    private Sprite character1;
    [SerializeField]
    private Sprite character2;
    [SerializeField]
    private Sprite character3;
    [SerializeField]
    private Sprite character4;
    [SerializeField]
    private Sprite character5;


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
    // Start is called before the first frame update
    void Awake()
    {
        characterDic[1] = character1;
        characterDic[2] = character2;
        characterDic[3] = character3;
        characterDic[4] = character4;
        characterDic[5] = character5;
        currentUsername = GetUsername();
        StartCoroutine(GetUserImageInfo());
    }

    private void OnEnable()
    {
        StartCoroutine(GetUserImageInfo());

    }

    public IEnumerator GetUserImageInfo()
    {
        UnityWebRequest userInforequest;
        using (userInforequest = UnityWebRequest.Get("https://k5c202.p.ssafy.io/od/unitys/profile/" + currentUsername + "/"))
        {
            yield return userInforequest.SendWebRequest();

            Profile profileJson = JsonUtility.FromJson<Profile>(userInforequest.downloadHandler.text);

            profileImage = GameObject.Find("CharacterImage").GetComponent<Image>();
            profileImage.sprite = characterDic[profileJson.character];
        }

    }
}
