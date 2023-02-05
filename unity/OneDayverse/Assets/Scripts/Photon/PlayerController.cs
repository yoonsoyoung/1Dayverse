using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using System.IO;
using System.Runtime.InteropServices;

// using static GameController;//게임 컨트롤러 사용
public class PlayerController : MonoBehaviourPunCallbacks, IDamageable
{
    [SerializeField] GameObject cameraHolder;
    [SerializeField] float mouseSensitivity, sprintSpeed, walkSpeed, jumpForce, smoothTime;
    // private Animator anim;
    [SerializeField] private Animator anim;

    [DllImport("__Internal")]
    private static extern string GetUsername();

    [DllImport("__Internal")] private static extern void goLive();

    float verticalLookRotation;
    bool grounded;
    Vector3 smoothMoveVelocity;
    Vector3 moveAmount;
    Rigidbody rb;
    PhotonView PV;
    // GameObject transform;
    GameObject player;

    CharacterController cc;
    private Vector3 moveVec;
    float gravity = -5f;
    float yVelocity = 0;
    public float moveSpeed = 7f;

    public Transform target;
    public Vector3 offset;
    string currentUsername;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        PV = GetComponent<PhotonView>();
        player = transform.gameObject;
    }
    void Start()
    {
        cc = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

        if (PV.IsMine)
        {
            // EquipItem(0);
            currentUsername = GetUsername();
            // currentUsername = "oxoxo";
            this.gameObject.name = currentUsername;
        }
        else
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
            Destroy(rb);
        }
    }
    void Update()
    {
        if (!PV.IsMine)
            return;
        Move();
        Jump();
    }


    void Move()
    {
        float h = Input.GetAxis("Horizontal");  // x��
        float v = Input.GetAxis("Vertical");    // z��

        Vector3 dir = new Vector3(h, 0, v);
        dir = dir.normalized;
        moveVec = new Vector3(h, 0, v).normalized;

        if (grounded && cc.collisionFlags == CollisionFlags.Below)
        {
            yVelocity = 0;
            anim.SetBool("isJump", false);
            grounded = false;
        }

        if (Input.GetButtonDown("Jump") && !grounded)
        {
            yVelocity = jumpForce;
            anim.SetBool("isJump", true);
            grounded = true;
        }

        yVelocity += gravity * Time.deltaTime;

        dir.y = yVelocity;

        cc.Move(dir * moveSpeed * Time.deltaTime);

        // when moving forward
        if (h == 0 && v == 0)
        {
            anim.SetBool("isMove", false);
        }
        else
        {
            anim.SetBool("isMove", true);
        }

        transform.LookAt(transform.position + moveVec);


    }
    void Jump()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            rb.AddForce(transform.up * jumpForce);
            anim.SetBool("isJump", true);
        }
        else
        {
            anim.SetBool("isJump", false);
        }

    }
    public void SetGroundedState(bool _grounded)
    {
        grounded = _grounded;
    }

    public void TakeDamage(float damage)
    {
        PV.RPC("RPC_Takedamage", RpcTarget.All, damage);
    }

    [PunRPC]
    void RPC_TakeDamage(float damage)
    {
        if (!PV.IsMine)
            return;

        Debug.Log("피해를 입다." + damage);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag.Equals("Desk"))
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                #if UNITY_WEBGL
                    goLive();
                #endif
            }
        }
    }
}