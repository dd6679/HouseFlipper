using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class NK_PlayerMove : MonoBehaviourPun, IPunObservable
{
    //�ӷ�
    public float moveSpeed = 5;
    //characterController ���� ����
    CharacterController cc;

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

    void Start()
    {
        //characterController �� ����
        cc = GetComponent<CharacterController>();
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
