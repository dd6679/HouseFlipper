using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NK_UIController : MonoBehaviour
{
    public GameObject behaviorUI;

    public GameObject taskUI;
    bool isCheckTask = false;

    public GameObject waitUI;
    private float currentTime;

    public static bool isFinishWaiting = false;

    // Start is called before the first frame update
    void Start()
    {
        behaviorUI.SetActive(false);
        taskUI.SetActive(isCheckTask);
    }

    // Update is called once per frame
    void Update()
    {
        // Test¿ë
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }


        if (Input.GetMouseButtonDown(1))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            NK_PlayerBehavior.behaviorState = NK_PlayerBehavior.PlayerBehaviorState.Move;
            behaviorUI.SetActive(true);
        }
        else if(Input.GetMouseButtonUp(1))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            behaviorUI.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if(isCheckTask)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                isCheckTask = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                isCheckTask =true;
                
            }
            taskUI.SetActive(isCheckTask);
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
