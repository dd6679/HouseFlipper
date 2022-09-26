using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NK_PlayerRotate : MonoBehaviourPun
{
    public Transform camPos;
    public float rotateSpeed = 200f;

    // ȸ���� ���� ����
    float rotX;
    float rotY;

    // Start is called before the first frame update
    void Awake()
    {
        if (photonView.IsMine)
        {
            camPos.GetComponent<Camera>().enabled = true;
            camPos.GetComponent<AudioListener>().enabled = true;
            camPos.GetComponent<SphereCollider>().enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine)
            return;

        if (Cursor.visible == false)
        {
            //1. ���콺�� �������� �޴´�.
            float mx = Input.GetAxis("Mouse X");
            float my = Input.GetAxis("Mouse Y");

            rotY = Mathf.Clamp(rotY, -70, 70);

            //2. ���콺�� �����Ӱ����� ȸ������ ������Ų��.
            rotX += mx * rotateSpeed * Time.deltaTime;
            rotY += my * rotateSpeed * Time.deltaTime;

            //3. �÷����� ȸ�� y���� �����Ѵ�.
            transform.localEulerAngles = new Vector3(0, rotX, 0);
            //4. CamPos�� ȸ�� x���� �����Ѵ�.
            camPos.localEulerAngles = new Vector3(-rotY, 0, 0);
        }
    }
}
