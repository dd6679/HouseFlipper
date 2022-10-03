using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintTool : MonoBehaviourPun
{
    RaycastHit hit;

    public Material[] material;
    public int x;
    Renderer ren;

    public AudioClip paintSound;
    AudioSource audioSource;
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();

        x = -1;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                if (hit.collider.gameObject.tag.Contains("Paint"))
                {
                    audioSource.PlayOneShot(paintSound);

                    int index;
                    Int32.TryParse(hit.collider.gameObject.name.Replace("(Clone)", String.Empty), out index);
                    x = index - 1;
                }

                if (hit.collider.gameObject.tag.Contains("Wall") && GameManager.instance.myChangeTool.index == (int)NK_ChangeTool.ToolState.PaintTool && GameManager.instance.myChangeTool.isMoving)
                {
                    audioSource.PlayOneShot(paintSound);
                    //audioSource.Play();
                    photonView.RPC("RpcChangeColor", RpcTarget.All, hit.collider.gameObject.GetComponent<PhotonView>().ViewID, x);
                }
            }
        }
    }

    [PunRPC]
    private void RpcChangeColor(int viewId, int x)
    {
        PhotonView view = PhotonView.Find(viewId);

        if (view != null && 0 <= x)
        {
            //Ŭ���� ������Ʈ�� ���� ��������
            Renderer ren = view.gameObject.GetComponent<Renderer>();
            //�Ž������� ����
            ren.enabled = true;
            //����Ʈ���� x���� ������Ʈ�� ������ ��
            ren.sharedMaterial = material[x];
        }
    }
}
