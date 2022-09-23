using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DK_PlayerMove : MonoBehaviour
{
    CharacterController cc;
    public float moveSpeed = 5;
    //중력
    float gravity = -9.81f;
    //점프파워
    public float jumpPower = 5;
    //y방향 속력
    float yVelocity;

    private CollisionFlags m_CollisionFlags;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {

        if (Cursor.visible == false)
        {
            float h = Input.GetAxisRaw("Horizontal"); //A : -1, D : 1, 누르지 않으면 : 0
            float v = Input.GetAxisRaw("Vertical");

            Vector3 dir = transform.forward * v + transform.right * h; // new Vector3(h, 0, v);
                                                                       //방향의 크기를 1로한다.
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
            m_CollisionFlags = cc.Move(dir * Time.fixedDeltaTime);
        }



        //if (Input.GetKeyDown(KeyCode.Tab) || Input.GetMouseButton(1))
        //{
        //    Cursor.lockState = CursorLockMode.None;
        //    Cursor.visible = true;
        //}
        //if (Input.GetMouseButton(0))
        //{
        //    Cursor.lockState = CursorLockMode.Locked;
        //    Cursor.visible = false;
        //}
        //transform.position += dir * moveSpeed * Time.deltaTime;
    }

    //private void OnControllerColliderHit(ControllerColliderHit hit)
    //{
    //    Rigidbody body = hit.collider.attachedRigidbody;
    //    //dont move the rigidbody if the character is on top of it
    //    if (m_CollisionFlags == CollisionFlags.Below)
    //    {
    //        return;
    //    }

    //    if (body == null || body.isKinematic)
    //    {
    //        return;
    //    }
    //    body.AddForceAtPosition(cc.velocity * 0.1f, hit.point, ForceMode.Impulse);
    //}
}
