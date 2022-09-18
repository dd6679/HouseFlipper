using ShatterToolkit.Helpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NK_MouseSetterControl : MonoBehaviour
{
    MouseShatter mouseSetter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<MouseShatter>() != null)
            mouseSetter = GetComponent<MouseShatter>();

        if (NK_ChangeTool.instance.index == (int)NK_ChangeTool.ToolState.DemolishTool && NK_ChangeTool.instance.isMoving)
        {
            mouseSetter.enabled = true;
        }
        else
        {
            mouseSetter.enabled = false;
        }
    }
}
