using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using static NK_PlayerBehavior;

public class NK_PlayerMove : MonoBehaviourPun, IPunObservable
{
    //�ӷ�
    public float moveSpeed = 5;
    //characterController ���� ����
    CharacterController cc;
    public Animator anim;

    //�߷�
    float gravity = -9.81f;
    //�����Ŀ�
    public float jumpPower = 5;
    //y���� �ӷ�
    float yVelocity;

    //�г��� UI
    public Text nickName;

    //���� ��ġ
    Vector3 receivePos;
    //ȸ���Ǿ� �ϴ� ��
    Quaternion receiveRot;
    //���� �ӷ�
    public float lerpSpeed = 100;

    private CollisionFlags m_CollisionFlags;

    public GameObject Light;
    bool isCheckLight = false;
    public bool isWalk = false;

    void Start()
    {
        //characterController �� ����
        cc = GetComponent<CharacterController>();
        //anim = transform.GetChild(0).GetComponent<Animator>(); 
        //����ü���� �ִ�ü������ ����
        //�г��� ����
        nickName.text = photonView.Owner.NickName;
        //GameManager���� ���� PhotonView�� ����
        GameManager.instance.AddPlayer(photonView);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

       
    }

    void Update()
    {
        //���࿡ �����̶��
        if (photonView.IsMine)
        {
            //if (Cursor.visible == false)
            {
                // WSAD�� ������ ��,��,��,��� �̵�
                //1. WSAD�� ��ȣ�� ����.
                float h = Input.GetAxisRaw("Horizontal"); //A : -1, D : 1, ������ ������ : 0
                float v = Input.GetAxisRaw("Vertical");

                if (v != 0) isWalk = true;
                else isWalk = false;

                photonView.RPC("RpcWalk", RpcTarget.All, isWalk);


                //2. ���� ��ȣ�� ������ �����.
                Vector3 dir = transform.forward * v + transform.right * h; // new Vector3(h, 0, v);
                                                                           //������ ũ�⸦ 1���Ѵ�.
                dir.Normalize();

                //���࿡ �ٴڿ� ����ִٸ� yVelocity�� 0���� ����
                if (cc.isGrounded)
                {
                    yVelocity = 0;
                }

                //���࿡ �����̹�(Jump)�� ������
                if (Input.GetButtonDown("Jump"))
                {
                    //yVelocity�� jumpPower�� ����
                    yVelocity = jumpPower;
                }

                //yVelocity���� �߷����� ���ҽ�Ų��.
                yVelocity += gravity * Time.deltaTime;

                //dir.y�� yVelocity���� ����
                dir.y = yVelocity;

                //3. �� �������� ��������.
                //P = P0 + vt
                cc.Move(dir * moveSpeed * Time.deltaTime);

                //������ Ű�����
                if (Input.GetKeyDown(KeyCode.Y))
                {
                    if (isCheckLight)
                    {
                        isCheckLight = false;
                    }
                    else isCheckLight = true;

                    Light.SetActive(isCheckLight);
                }
            }
        }
        //������ �ƴ϶��
        else
        {
            //Lerp�� �̿��ؼ� ������, ����������� �̵� �� ȸ��
            transform.position = Vector3.Lerp(transform.position, receivePos, lerpSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, receiveRot, lerpSpeed * Time.deltaTime);
        }
    }

    [PunRPC]
    private void RpcWalk(bool isWalk)
    {
        if (isWalk)
            anim.SetBool("isWalk", true);
        else anim.SetBool("isWalk", false);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //������ ������
        if (stream.IsWriting) // isMine == true
        {
            //position, rotation
            stream.SendNext(transform.rotation);
            stream.SendNext(transform.position);
        }
        //������ �ޱ�
        else if (stream.IsReading) // ismMine == false
        {
            receiveRot = (Quaternion)stream.ReceiveNext();
            receivePos = (Vector3)stream.ReceiveNext();
        }
    }
}
