using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NK_Move : MonoBehaviourPun
{
    public Transform camPos;
    public float speed = 5f;
    GameObject objectHitPosition;
    RaycastHit hitRay, hitLayerMask;
    float yScroll;

    private void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel") * speed;
        yScroll += scroll;

        photonView.RPC("RpcRotate", RpcTarget.All, yScroll);
    }

    [PunRPC]
    private void RpcRotate(float yScroll)
    {
        gameObject.transform.localEulerAngles = new Vector3(0, yScroll, 0);

        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            speed = 180;
            //yScroll = 180 * yScroll / speed;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
        {
            speed = 5;
        }
    }

    private void OnMouseUp()
    {
        transform.parent = null;
        Destroy(objectHitPosition);
    }

    private void OnMouseDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hitRay))
        {
            photonView.RPC("RpcCreateHitPosition", RpcTarget.All, hitRay.point);
        }
    }

    [PunRPC]
    private void RpcCreateHitPosition(Vector3 point)
    {
        objectHitPosition = new GameObject("HitPosition");
        objectHitPosition.transform.position = point;
        transform.SetParent(objectHitPosition.transform);
    }

    private void OnMouseDrag()
    {
       Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 1000, Color.green);

        int layerMask = 1 << LayerMask.NameToLayer("Floor");

        if (Physics.Raycast(ray, out hitLayerMask, Mathf.Infinity, layerMask) /*&& NK_UIController.isFinishWaiting*/)
        {
            float H = Camera.main.transform.position.y;
            float h = objectHitPosition.transform.position.y;

            Vector3 newPos = (hitLayerMask.point * (H - h) + Camera.main.transform.position * h) / H;
            photonView.RPC("RpcMove", RpcTarget.AllBuffered, newPos);
        }
    }

    [PunRPC]
    private void RpcMove(Vector3 newPos)
    {
        objectHitPosition.transform.position = newPos;
    }
}
