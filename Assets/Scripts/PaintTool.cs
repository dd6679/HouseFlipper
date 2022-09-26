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
    void Start()
    {
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
                    int index;
                    Int32.TryParse(hit.collider.gameObject.name.Replace("(Clone)", String.Empty), out index);
                    x = index - 1;
                }

                if (hit.collider.gameObject.tag.Contains("Wall") && GameManager.instance.myChangeTool.index == (int)NK_ChangeTool.ToolState.PaintTool && GameManager.instance.myChangeTool.isMoving)
                {
                    //Ŭ���� ������Ʈ�� ���� ��������
                    Renderer ren = hit.collider.gameObject.GetComponent<Renderer>();

                    //�Ž������� ����
                    ren.enabled = true;

                    //����Ʈ���� x���� ������Ʈ�� ������ ��
                    ren.sharedMaterial = material[x];

                    //x�� ����
                    //ChangeColor();
                }
            }
        }
    }

    [PunRPC]
    private void RpcChangeColor()
    {

    }
}
