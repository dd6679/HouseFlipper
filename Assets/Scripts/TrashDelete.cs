using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrashDelete : MonoBehaviourPun
{
    AudioSource audioSource;
    public AudioClip deleteSound;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }


    RaycastHit hit;
    Transform tr;

    void Update()
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
            tr.localScale -= new Vector3(0.05f, 0.05f, 0.05f);
            if (tr.localScale.x <= 0.1f)
            {
                PhotonNetwork.Destroy(tr.gameObject);
                audioSource.PlayOneShot(deleteSound);
            }
        }
    }


}
