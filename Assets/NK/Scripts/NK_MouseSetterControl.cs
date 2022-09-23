using ShatterToolkit.Helpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NK_MouseSetterControl : MonoBehaviour
{
    MouseShatter mouseSetter;
    DustDelete dustDelete;
    TrashDelete trashDelete;
    // Start is called before the first frame update
    void Start()
    {
        mouseSetter = GetComponent<MouseShatter>();
        dustDelete = GetComponent<DustDelete>();
        trashDelete = GetComponent<TrashDelete>();  
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.myChangeTool.index == (int)NK_ChangeTool.ToolState.DemolishTool)
        {
            mouseSetter.enabled = true;
        }
        else
        {
            mouseSetter.enabled = false;
        }
        if (GameManager.instance.myChangeTool.index == (int)NK_ChangeTool.ToolState.CleanTool && GameManager.instance.myChangeTool.isMoving)
        {
            dustDelete.enabled = true;
        }
        else
        {
            dustDelete.enabled = false;
        }
    }
}
