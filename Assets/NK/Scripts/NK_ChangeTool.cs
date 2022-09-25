using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NK_ChangeTool : MonoBehaviour
{
    public enum ToolState
    {
        CleanTool,
        PaintTool,
        SellTool,
        DemolishTool,
        Move,
    }

    public GameObject[] tools;
    public GameObject[] graps;
    public int index = -1;
    public bool isMoving = false;

    NK_IKControl iKControl;

    // Start is called before the first frame update
    void Start()
    {
        InitializeTool();
        iKControl = transform.GetChild(0).GetComponent<NK_IKControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Cursor.visible == false)
        {
            if (index >= 0)
            {
                if (tools[index].GetComponent<Animator>() != null)
                {
                    Animator anim = tools[index].GetComponent<Animator>();
                    if (Input.GetMouseButtonDown(0))
                    {
                        anim.SetBool("isMove", true);
                        isMoving = true;
                    }
                    else if (Input.GetMouseButtonUp(0))
                    {
                        anim.SetBool("isMove", false);
                        isMoving = false;
                    }
                }
            }
        }
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
        iKControl.lookObj = tools[index].transform;
        if (index != 4)
        {
            iKControl.ikActive = true;
            iKControl.rightHandObj = graps[index].transform;
        }
        else
            iKControl.ikActive = false;
    }
}
