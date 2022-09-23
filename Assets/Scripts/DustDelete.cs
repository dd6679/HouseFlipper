using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustDelete : MonoBehaviourPun
{
    // 삭제시간
    float currentTime = 1f;
    RaycastHit hit;

    void Start()
    {

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
                currentTime = 1;
            }
        }
    }
}