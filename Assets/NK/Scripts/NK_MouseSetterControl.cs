using ShatterToolkit.Helpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NK_MouseSetterControl : MonoBehaviour
{
    MouseShatter mouseSetter;
    DustDelete dustDelete;
    TrashDelete trashDelete;
    PaintTool paintTool;
    FireTool fireTool;
    // Start is called before the first frame update
    void Start()
    {
        mouseSetter = GetComponent<MouseShatter>();
        dustDelete = GetComponent<DustDelete>();
        trashDelete = GetComponent<TrashDelete>();  
        paintTool = GetComponent<PaintTool>();
        fireTool = GetComponent<FireTool>();
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
        if(GameManager.instance.myChangeTool.index == (int)NK_ChangeTool.ToolState.PaintTool)
        {
            paintTool.enabled = true;
        }
        else
        {
            paintTool.enabled = false;
        }
        if(GameManager.instance.myChangeTool.index == (int)NK_ChangeTool.ToolState.FlameTool)
        {
            fireTool.enabled = true;
        }
        else
        {
            fireTool.enabled = false;
        }
    }
}
