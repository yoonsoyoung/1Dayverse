using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;
using TMPro;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine.UI;

public class MyPageData : MonoBehaviour
{
    private TextMeshProUGUI username;
    private TextMeshProUGUI birth_day;
    private TextMeshProUGUI phone_number;
    private TextMeshProUGUI character;
    private TextMeshProUGUI lecture_name0;
    private TextMeshProUGUI lecture_tutor0;
    private TextMeshProUGUI lecture_password0;
    private TextMeshProUGUI lecture_name1;
    private TextMeshProUGUI lecture_tutor1;
    private TextMeshProUGUI lecture_password1;
    private TextMeshProUGUI lecture_name2;
    private TextMeshProUGUI lecture_tutor2;
    private TextMeshProUGUI lecture_password2;
    private TextMeshProUGUI lecture_name3;
    private TextMeshProUGUI lecture_tutor3;
    private TextMeshProUGUI lecture_password3;
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

    [DllImport("__Internal")]
    private static extern string GetUsername();

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
      

    [Serializable]
    public class Lecture
    {
        public string id;
        public string name;
        public string title;
        public string description;
        public string password;
        public string validation;
    }

    [Serializable]
    public class Lectures
    {
        public Lecture[] lectures;
    }

    [Serializable]
    public class JsonData
    {
        public Profile profile;
        public Lectures lectures;
        public string character;
    }



    // Start is called before the first frame update
    void Start()
    {
        characterDic[1] = character1;
        characterDic[2] = character2;
        characterDic[3] = character3;
        characterDic[4] = character4;
        characterDic[5] = character5;


        username = GameObject.Find("name").GetComponent<TMPro.TextMeshProUGUI>();
        birth_day = GameObject.Find("birthday").GetComponent<TMPro.TextMeshProUGUI>();
        phone_number = GameObject.Find("phonenumber").GetComponent<TMPro.TextMeshProUGUI>();
        lecture_name0 = GameObject.Find("lecturename0").GetComponent<TMPro.TextMeshProUGUI>();
        lecture_tutor0 = GameObject.Find("lecturetutor0").GetComponent<TMPro.TextMeshProUGUI>();
        lecture_password0 = GameObject.Find("lecturepassword0").GetComponent<TMPro.TextMeshProUGUI>();
        lecture_name1 = GameObject.Find("lecturename1").GetComponent<TMPro.TextMeshProUGUI>();
        lecture_tutor1 = GameObject.Find("lecturetutor1").GetComponent<TMPro.TextMeshProUGUI>();
        lecture_password1 = GameObject.Find("lecturepassword1").GetComponent<TMPro.TextMeshProUGUI>();
        lecture_name2 = GameObject.Find("lecturename2").GetComponent<TMPro.TextMeshProUGUI>();
        lecture_tutor2 = GameObject.Find("lecturetutor2").GetComponent<TMPro.TextMeshProUGUI>();
        lecture_password2 = GameObject.Find("lecturepassword2").GetComponent<TMPro.TextMeshProUGUI>();
        lecture_name3 = GameObject.Find("lecturename3").GetComponent<TMPro.TextMeshProUGUI>();
        lecture_tutor3 = GameObject.Find("lecturetutor3").GetComponent<TMPro.TextMeshProUGUI>();
        lecture_password3 = GameObject.Find("lecturepassword3").GetComponent<TMPro.TextMeshProUGUI>();

        currentUsername = GetUsername();
        // currentUsername = "oxoxo";
        StartCoroutine(GetUserInfo());
        StartCoroutine(GetLectureInfo());
    }


    public IEnumerator GetUserInfo()
    {
        UnityWebRequest userInforequest;
        using (userInforequest = UnityWebRequest.Get("https://k5c202.p.ssafy.io/od/unitys/profile/" + currentUsername + "/"))
        {
            yield return userInforequest.SendWebRequest();

            Profile profileJson = JsonUtility.FromJson<Profile>(userInforequest.downloadHandler.text);
            username.text = profileJson.username;
            birth_day.text = profileJson.birth_day;
            phone_number.text = profileJson.phone_number;

            profileImage = GameObject.Find("CharacterImage").GetComponent<Image>();
            profileImage.sprite = characterDic[profileJson.character];
        }

    }

    public IEnumerator GetLectureInfo()
    {
        UnityWebRequest lectureInforequest;
        using (lectureInforequest = UnityWebRequest.Get("https://k5c202.p.ssafy.io/od/unitys/lecture/" + currentUsername + "/"))
        {
            yield return lectureInforequest.SendWebRequest();
            var response = lectureInforequest.downloadHandler.text;
            Lectures lectureInfo = JsonUtility.FromJson<Lectures>(response);
            //Debug.Log(lectureInfo.lectures[0].name);
            
            if (lectureInfo.lectures.Length >= 1)
            {
                lecture_tutor0.text = lectureInfo.lectures[0].name;
                lecture_name0.text = lectureInfo.lectures[0].title;
                lecture_password0.text = lectureInfo.lectures[0].password;
            } else
            {
                lecture_tutor0.text = "등록된 강의가 없습니다";
                lecture_name0.text = "";
                lecture_password0.text = "";
            }

            if (lectureInfo.lectures.Length >= 2)
            {
                lecture_tutor1.text = lectureInfo.lectures[1].name;
                lecture_name1.text = lectureInfo.lectures[1].title;
                lecture_password1.text = lectureInfo.lectures[1].password;
            }
            else
            {
                lecture_tutor1.text = "";
                lecture_name1.text = "";
                lecture_password1.text = "";
            }

            if (lectureInfo.lectures.Length >= 3)
            {
                lecture_tutor2.text = lectureInfo.lectures[2].name;
                lecture_name2.text = lectureInfo.lectures[2].title;
                lecture_password2.text = lectureInfo.lectures[2].password;
            }
            else
            {
                lecture_tutor2.text = "";
                lecture_name2.text = "";
                lecture_password2.text = "";
            }

            if (lectureInfo.lectures.Length >= 4)
            {
                lecture_tutor3.text = lectureInfo.lectures[3].name;
                lecture_name3.text = lectureInfo.lectures[3].title;
                lecture_password3.text = lectureInfo.lectures[3].password;
            }
            else
            {
                lecture_tutor3.text = "";
                lecture_name3.text = "";
                lecture_password3.text = "";
            }

        }

    }
}