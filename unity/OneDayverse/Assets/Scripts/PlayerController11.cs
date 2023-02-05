//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PlayerController : MonoBehaviour
//{

//    public float moveSpeed = 7f;
//    //ĳ���� ��Ʈ�ѷ� ���� �߰�
//    CharacterController cc;

//    // �߷� ����
//    float gravity = -5f;
//    //���� �ӷ� ����
//    float yVelocity = 0;
//    // ������ ����
//    public float jumpPower = 10f;
//    // ���� ���� ���� : bool ->> true / false
//    public bool isJumping = false;

//    private Vector3 moveVec;

//    private Vector3 v_movement;


//    Animator anim;


//    // Start is called before the first frame update
//    void Start()
//    {
//        cc = GetComponent<CharacterController>();
//        anim = transform.GetComponent<Animator>();
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        float h = Input.GetAxis("Horizontal");  // x��
//        float v = Input.GetAxis("Vertical");    // z��

//        Vector3 dir = new Vector3(h, 0, v);
//        dir = dir.normalized;
//        moveVec = new Vector3(h, 0, v).normalized;


//        // ���� ���� ���̾���, �ٽ� �ٴڿ� �����ߴٸ�
//        if (isJumping && cc.collisionFlags == CollisionFlags.Below)
//        {
//            yVelocity = 0;
//            anim.SetBool("isJump", false);
//            isJumping = false;
//        }

//        // ���� Ű���� �����̽� �ٸ� �Է��߰�, ������ ���� ���� ���¶��
//        if (Input.GetButtonDown("Jump") && !isJumping)
//        {
//            // ĳ���� ���� �ӵ��� �������� �����ϰ� ���� ���·� ����
//            yVelocity = jumpPower;
//            anim.SetBool("isJump", true);
//            isJumping = true;
//            print("jump");
//        }

//        // ĳ���� ���� �ӵ��� �߷� �� ����
//        yVelocity += gravity * Time.deltaTime;

//        // yVelocity = yVelocity + gravity * Time.deltaTime;

//        dir.y = yVelocity;

//        cc.Move(dir * moveSpeed * Time.deltaTime);

//        // when moving forward
//        if (h == 0 && v == 0)
//        {
//            anim.SetBool("isMove", false);
//        }
//        else
//        {
//            anim.SetBool("isMove", true);
//        }

//        transform.LookAt(transform.position + moveVec);
//    }

//}