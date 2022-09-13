using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NK_PlayerMove : MonoBehaviourPun, IPunObservable
{
    // �ӷ�, �����Ŀ�
    public float moveSpeed = 5;
    public float jumpPower = 5;
    // �߷�
    float gravity = -9.8f;
    float yVelocity;
    
    CharacterController cc;
    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            Vector3 dir = transform.forward * v + transform.right * h;

            dir.Normalize();

            if (cc.isGrounded)
            {
                yVelocity = 0;
            }

            if (Input.GetButtonDown("Jump"))
            {
                yVelocity = jumpPower;
            }

            yVelocity += gravity * Time.deltaTime;

            dir.y = yVelocity;

            cc.Move(dir * moveSpeed * Time.deltaTime);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // ������ ������
        if (stream.IsWriting)
        {

        }
        // ������ �ޱ�
        else
        {

        }
    }
}
