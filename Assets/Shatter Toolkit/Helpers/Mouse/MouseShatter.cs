// Shatter Toolkit
// Copyright 2015 Gustav Olsson
using Photon.Pun;
using UnityEngine;

namespace ShatterToolkit.Helpers
{
    public class MouseShatter : MonoBehaviourPun
    {
        public void Update()
        {
            if (!photonView.IsMine)
                return;

            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
                {
                    if (Vector3.Distance(hit.collider.gameObject.transform.position, transform.position) <= 1.8f && hit.collider.gameObject.tag.Contains("Wall"))
                    {
                        DestroyShatter(hit.collider.gameObject.GetComponent<PhotonView>().ViewID, hit.point);

                    }
                }
            }
        }

        private void DestroyShatter(int viewId, Vector3 point)
        {
            photonView.RPC("RpcDestroyShatter", RpcTarget.All, viewId, point);
        }

        [PunRPC]
        private void RpcDestroyShatter(int viewId, Vector3 point)
        {
            GameObject shatter = PhotonView.Find(viewId).gameObject;
            Rigidbody rig = shatter.GetComponent<Rigidbody>();
            rig.isKinematic = false;
            shatter.SendMessage("Shatter", point, SendMessageOptions.DontRequireReceiver);
        }
    }
}