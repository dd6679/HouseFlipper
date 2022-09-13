using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NK_UIController : MonoBehaviour
{
    GameObject behaviorUI;
    bool isCheckBehavior= false;
    // Start is called before the first frame update
    void Start()
    {
        behaviorUI = transform.GetChild(0).gameObject;
        behaviorUI.SetActive(isCheckBehavior);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (isCheckBehavior)
            {
                isCheckBehavior = false;
            }
            else
            {
                isCheckBehavior = true;
            }
            behaviorUI.SetActive(isCheckBehavior);
        }
    }
}
