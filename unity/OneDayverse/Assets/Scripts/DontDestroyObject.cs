using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class DontDestroyObject : MonoBehaviour
{
    public static DontDestroyObject instance;
    public string prevScene;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }

        else
        {
            instance = this;
            DontDestroyOnLoad(this);

        }

    }


    public void AfterMyPageBtnClick()
    {
        instance.prevScene = SceneManager.GetActiveScene().name;
        Debug.Log("현재페이지" + prevScene);
        SceneManager.LoadScene("MyPage");
    }

}
