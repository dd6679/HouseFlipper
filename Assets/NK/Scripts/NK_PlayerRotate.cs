using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NK_PlayerRotate : MonoBehaviourPun
{
    public Transform camPos;
    public float rotateSpeed = 200f;

    // 회전값 누적 변수
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
            //1. 마우스의 움직임을 받는다.
            float mx = Input.GetAxis("Mouse X");
            float my = Input.GetAxis("Mouse Y");

            rotY = Mathf.Clamp(rotY, -70, 70);

            //2. 마우스이 움직임값으로 회전값을 누적시킨다.
            rotX += mx * rotateSpeed * Time.deltaTime;
            rotY += my * rotateSpeed * Time.deltaTime;

            //3. 플레어의 회전 y값을 셋팅한다.
            transform.localEulerAngles = new Vector3(0, rotX, 0);
            //4. CamPos의 회전 x값을 셋팅한다.
            camPos.localEulerAngles = new Vector3(-rotY, 0, 0);
        }
    }
}
