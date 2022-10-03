using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrashDelete : MonoBehaviourPun
{
    static List<int> deletedTrash = new List<int>();
    public AudioClip deleteSound;
    AudioSource audioSource;
    RaycastHit hit;
    Transform tr;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        for (int i = 0; i < deletedTrash.Count; i++)
        {
            photonView.RPC("RpcDestroyTrash", RpcTarget.All, deletedTrash[i]);
        }
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            if (Input.GetMouseButtonDown(0))
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
        PhotonView view = PhotonView.Find(viewId);
        if (view != null)
        {
            GameObject trash = view.gameObject;
            trash.transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
            if (trash.transform.localScale.x <= 0.1f)
            {
                Destroy(trash);
                deletedTrash.Add(viewId);
                audioSource.PlayOneShot(deleteSound);
            }

        }
    }
}
