using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class moveBusAn : MonoBehaviour
{
    GameObject bus; // ������Ʈ
    Transform target; // ��ġ ����

    public float speed = 150;

    public float start_point = 50;
    public float end_point = -164;
    public float stop_point = -12;

    private float timer = 0.0f;
    private float waiting = 3.0f;


    void Start()
    {
        bus = GameObject.Find("SchoolBus"); // �ش� �̸��� ������Ʈ�� ã�� ����
        target = bus.GetComponent<Transform>();

        target.position = new Vector3(176, -83, 135); // bus ������Ʈ�� �ʱ� ��ġ
        Invoke("Wait5Sec", 5f);
        
    }

    void Wait5Sec()
    {
        SceneManager.LoadScene("Category");
    }
    // Update is called once per frame
    void Update()
    {
        // ���� ��ǥ �̵�
        InvokeRepeating("Update_Moving", 0.5f, 6f); // �ش� �Լ��� 1�� �� ���� ��Ų �� 5�� �������� �ݺ� ����
        
        
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
            Debug.Log("�����");
            Destroy(this);
            InvokeRepeating("Instan_bus", 2f, 2f);
        }
    }

    private void Instan_bus()
    {
        Debug.Log("��Ÿ����");
        Instantiate(bus);
        // Update_Moving();
    }
}
