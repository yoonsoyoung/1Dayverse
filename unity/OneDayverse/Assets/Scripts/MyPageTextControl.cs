using UnityEngine;
using System.Collections;

public class MyPageTextControl : MonoBehaviour
{
    public GameObject readText;
    public static MyPageTextControl instance;


    void Awake()
    {
        if (MyPageTextControl.instance == null)
            MyPageTextControl.instance = this;
    }
    // Use this for initialization
    void Start()
    {
        readText.SetActive(false);
        StartCoroutine(ShowReady());
    }

    IEnumerator ShowReady()
    {
        int count = 0;
        while (count < 7)
        {
            readText.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            readText.SetActive(false);
            yield return new WaitForSeconds(0.3f);
            count++;
        }
        readText.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {

    }

}
