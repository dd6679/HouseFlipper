using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTool : MonoBehaviourPun
{
    public GameObject Flames;
    public GameObject Sound;
    public GameObject Light;
    bool isFire = false;

    RaycastHit hit;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        FireShot();
    }

    void FireShot()
    {
        if (Cursor.visible == false && Input.GetMouseButton(0))
        {
            Flames.SetActive(true);
            Sound.SetActive(true);
            Light.SetActive(true);
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                if (Vector3.Distance(hit.collider.gameObject.transform.position, transform.position) <= 5.5f && hit.collider.gameObject.tag.Contains("Weed"))
                {
                    if (hit.collider.gameObject.GetComponent<PhotonView>() != null)
                        photonView.RPC("RpcDestroyWeed", RpcTarget.AllBuffered, hit.collider.gameObject.GetComponent<PhotonView>().ViewID);
                }
            }
        }
            else
            {
                Flames.SetActive(false);
                Sound.SetActive(false);
                Light.SetActive(false);
            }
    }

    [PunRPC]
    private void RpcDestroyWeed(int viewId)
    {
        PhotonView view = PhotonView.Find(viewId);
        if(view != null)
        {
            Destroy(view.gameObject, 0.1f);
        }
    }
}
