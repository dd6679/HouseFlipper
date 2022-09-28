using Photon.Pun;
using Photon.Voice.PUN;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NK_ChangeTool : MonoBehaviourPun
{
    public enum ToolState
    {
        CleanTool,
        PaintTool,
        SellTool,
        DemolishTool,
        Move,
        FlameTool
    }

    public GameObject[] tools;
    public GameObject[] graps;
    public int index = -1;
    public bool isMoving = false;

    NK_IKControl iKControl;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        InitializeTool();
        iKControl = transform.GetChild(0).GetComponent<NK_IKControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine)
            return;

        if (Cursor.visible == false)
        {
            if (index >= 0)
            {
                if (tools[index].GetComponent<Animator>() != null)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        photonView.RPC("RpcMoveTool", RpcTarget.All, index);
                        isMoving = true;
                    }
                    else if (Input.GetMouseButtonUp(0))
                    {
                        photonView.RPC("RpcStopTool", RpcTarget.All, index);
                        isMoving = false;
                    }
                    
                }
            }
        }
    }

    [PunRPC]
    private void RpcMoveTool(int index)
    {
        anim = tools[index].GetComponent<Animator>();
        anim.SetBool("isMove", true);
    }

    [PunRPC]
    private void RpcStopTool(int index)
    {
        anim = tools[index].GetComponent<Animator>();
        anim.SetBool("isMove", false);
    }

    private void InitializeTool()
    {
        for (int i = 0; i < tools.Length; i++)
        {
            tools[i].SetActive(false);
        }
    }

    public void SwitchTool(int newIndex)
    {
        if (index == newIndex)
        {
            return;
        }
        index = newIndex;
        for (int i = 0; i < tools.Length; i++)
        {
            tools[(i)].SetActive(false);
        }
        tools[index].SetActive(true);
        if (index != 4)
        {
            iKControl.ikActive = true;
            iKControl.rightHandObj = graps[index].transform;
        }
        else
            iKControl.ikActive = false;
    }
}
