using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NK_UIController : MonoBehaviour
{
    public GameObject behaviorUI;
    bool isCheckBehavior = false;

    public GameObject taskUI;
    bool isCheckTask = false;

    public GameObject waitUI;
    private float currentTime;

    public static bool isFinishWaiting = false;

    // Start is called before the first frame update
    void Start()
    {
        behaviorUI.SetActive(isCheckBehavior);
        taskUI.SetActive(isCheckTask);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
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
            if(isCheckBehavior == true)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if(isCheckTask)
            {
                isCheckTask = false;
            }
            else
            {
                isCheckTask =true;
                
            }
            taskUI.SetActive(isCheckTask);
            if (isCheckTask == true)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = true;
            }
        }

        if (Input.GetMouseButton(0) && NK_PlayerBehavior.isWaiting && NK_PlayerBehavior.behaviorState == NK_PlayerBehavior.PlayerBehaviorState.Move)
        {
            currentTime += Time.deltaTime;
            waitUI.transform.GetChild(1).GetComponent<Image>().fillAmount = currentTime;
            waitUI.SetActive(true);
            if (currentTime >= 1)
            {
                isFinishWaiting = true;
            }
        }
        else
        {
            waitUI.SetActive(false);
            currentTime = 0;
            //isFinishWaiting = false;
        }
    }
}
