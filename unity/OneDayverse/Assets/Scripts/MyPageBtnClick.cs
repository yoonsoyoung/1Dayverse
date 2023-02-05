using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyPageBtnClick : MonoBehaviour
{
    public Button ShowMyPageBtn;
    // Start is called before the first frame update
    void Start()
    {
        ShowMyPageBtn.onClick.AddListener(ShowMyPageBtnClick);
    }

    // Update is called once per frame
    private void ShowMyPageBtnClick()
    {
        DontDestroyObject.instance.AfterMyPageBtnClick();
    }
}
