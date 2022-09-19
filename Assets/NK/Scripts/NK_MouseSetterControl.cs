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
        if (NK_ChangeTool.instance.index == (int)NK_ChangeTool.ToolState.DemolishTool && NK_ChangeTool.instance.isMoving)
        {
            mouseSetter.enabled = true;
        }
        else
        {
            mouseSetter.enabled = false;
        }
        if (NK_ChangeTool.instance.index == (int)NK_ChangeTool.ToolState.CleanTool && NK_ChangeTool.instance.isMoving)
        {
            dustDelete.enabled = true;
        }
        else
        {
            dustDelete.enabled = false;
        }
    }
}
