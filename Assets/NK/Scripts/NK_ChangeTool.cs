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
    }

    public GameObject[] tools;

    public static NK_ChangeTool instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        InitializeTool();
    }

    // Update is called once per frame
    void Update()
    {
        
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
        for (int i = 0; i < tools.Length; i++)
        {
            tools[(i)].SetActive(false);
        }
        tools[(newIndex)].SetActive(true);
    }
}
