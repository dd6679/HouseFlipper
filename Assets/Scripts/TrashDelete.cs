using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrashDelete : MonoBehaviourPun
{
    public AudioClip deleteSound;
    AudioSource audioSource;
    RaycastHit hit;
    Transform tr;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            if (Input.GetMouseButton(0))
            {
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
                {
                    if (hit.collider.gameObject.tag.Contains("Trash"))
                    {
                        tr = hit.collider.gameObject.transform;
                    }
                }
            }
            if (tr != null)
            {
                DestroyTrash(tr.GetComponent<PhotonView>().ViewID);
            }
        }

    }

    private void DestroyTrash(int viewId)
    {
        photonView.RPC("RpcDestroyTrash", RpcTarget.All, viewId);
    }

    [PunRPC]
    private void RpcDestroyTrash(int viewId)
    {
        GameObject trash = PhotonView.Find(viewId).gameObject;
        trash.transform.localScale -= new Vector3(0.05f, 0.05f, 0.05f);
        if (trash.transform.localScale.x <= 0.1f)
        {
            Destroy(trash);
            audioSource.PlayOneShot(deleteSound);
        }
    }
}
