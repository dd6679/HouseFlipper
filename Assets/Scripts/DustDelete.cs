using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustDelete : MonoBehaviourPun
{
    // �����ð�
    static List<int> deletedDust = new List<int>();
    float currentTime = 1f;
    RaycastHit hit;

    void Start()
    {
        for (int i = 0; i < deletedDust.Count; i++)
        {
            photonView.RPC("RpcDestoryDust", RpcTarget.All, deletedDust[i]);
        }
    }


    void Update()
    {
        if (!photonView.IsMine)
            return;

        if (Input.GetMouseButton(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                if (hit.collider.gameObject.tag.Contains("Dust"))
                {
                    if(hit.collider.gameObject.GetComponent<PhotonView>() != null)
                        DestroyDust(hit.collider.gameObject.GetComponent<PhotonView>().ViewID);
                }
            }
        }
    }

    private void DestroyDust(int viewId)
    {
        photonView.RPC("RpcDestoryDust", RpcTarget.All, viewId);
    }

    [PunRPC]
    private void RpcDestoryDust(int viewId)
    {
        GameObject dust = PhotonView.Find(viewId).gameObject;
        if (dust != null)
        {
            Renderer ren = dust.GetComponent<Renderer>();
            currentTime -= Time.deltaTime * 1 / 2;
            Color color = ren.material.color;
            color.a = currentTime;
            ren.material.color = color;
            if (color.a < 0.55f)
            {
                Destroy(dust);
                deletedDust.Add(viewId);
                currentTime = 1;
            }
        }
    }
}
