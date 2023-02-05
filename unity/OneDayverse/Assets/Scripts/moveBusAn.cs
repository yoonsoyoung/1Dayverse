using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class moveBusAn : MonoBehaviour
{
    GameObject bus; // 오브젝트
    Transform target; // 위치 정보

    public float speed = 150;

    public float start_point = 50;
    public float end_point = -164;
    public float stop_point = -12;

    private float timer = 0.0f;
    private float waiting = 3.0f;


    void Start()
    {
        bus = GameObject.Find("SchoolBus"); // 해당 이름의 오브젝트를 찾아 연결
        target = bus.GetComponent<Transform>();

        target.position = new Vector3(176, -83, 135); // bus 오브젝트를 초기 위치
        Invoke("Wait5Sec", 5f);
        
    }

    void Wait5Sec()
    {
        SceneManager.LoadScene("Category");
    }
    // Update is called once per frame
    void Update()
    {
        // 고정 좌표 이동
        InvokeRepeating("Update_Moving", 0.5f, 6f); // 해당 함수를 1초 뒤 실행 시킨 후 5초 간격으로 반복 실행
        
        
    }

    private void Update_Moving()
    {
        if (target.position.x > -12)
        {
            target.position = Vector3.MoveTowards(target.position, new Vector3(stop_point, -83, 135), Time.deltaTime * speed);
        } else if(target.position.x <= -12)
        {
            GameObject.Find("LogoCanvas").transform.Find("Panel").gameObject.SetActive(true);
            GameObject.Find("LogoCanvas").transform.Find("1DAYVERSE").gameObject.SetActive(true);
            timer += Time.deltaTime;
            if (timer > waiting)
                 target.position = Vector3.MoveTowards(target.position, new Vector3(end_point, -83, 135), Time.deltaTime * speed);
        }

        if(target.position.x == end_point)
        {
            Debug.Log("사라져");
            Destroy(this);
            InvokeRepeating("Instan_bus", 2f, 2f);
        }
    }

    private void Instan_bus()
    {
        Debug.Log("나타나라");
        Instantiate(bus);
        // Update_Moving();
    }
}
