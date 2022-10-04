using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using static NK_PlayerBehavior;

public class NK_UIController : MonoBehaviour
{
    public GameObject behaviorUI;
    bool isCheckBehavior = false;

    public GameObject taskUI;
    bool isCheckTask = false;

    public GameObject waitUI;
    private float currentTime;

    public GameObject endUI;

    public GameObject StoreUI;
    bool isCheckStore = false;

    public GameObject EndImage;

    public GameObject ChattingUI;
    bool isCheckChatting = false;

    public Text behaviorText;
    public GameObject[] behaviorThumbs;
    public static bool isFinishWaiting = false;

    public Image progressBar;

    int prevBehaviorId = -1;

    // Start is called before the first frame update
    void Start()
    {
        behaviorUI.SetActive(isCheckBehavior);
        taskUI.SetActive(isCheckTask);
    }

    // Update is called once per frame
    void Update()
    {
        //print(isFinishWaiting);
        // Test용
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }


        if (Input.GetMouseButtonDown(1))
        {
            if (isCheckBehavior)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                isCheckBehavior = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                isCheckBehavior = true;
                GameManager.instance.myPlayerBehavior.behaviorState = NK_PlayerBehavior.PlayerBehaviorState.Idle;
            }
            behaviorUI.SetActive(isCheckBehavior);
 /*           Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            NK_PlayerBehavior.behaviorState = NK_PlayerBehavior.PlayerBehaviorState.Move;
            behaviorUI.SetActive(true);
        }
        else if(Input.GetMouseButtonUp(1))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            behaviorUI.SetActive(false);*/
        }

        if (Input.GetKeyDown(KeyCode.CapsLock))
        {
            if (isCheckStore)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                isCheckStore = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                isCheckStore = true;
                //GameManager.instance.myPlayerBehavior.behaviorState = NK_PlayerBehavior.PlayerBehaviorState.Idle;
            }
            StoreUI.SetActive(isCheckStore);
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

        if (Input.GetMouseButton(0) && GameManager.instance.myPlayerBehavior.isWaiting)
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

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (!isCheckChatting)
            {
                ChattingUI.SetActive(true);
                isCheckChatting = true;
            }
            else
            {
                ChattingUI.SetActive(false);
                isCheckChatting = false;
            }
        }

        if (/*progressBar.fillAmount > 0.7 &&*/ Input.GetKeyDown(KeyCode.Return))
        {
            endUI.SetActive(true);
        }

        if(EndImage.activeSelf)
        {
            Invoke("Connection", 3f);
        }
    }

    public void Connection()
    {
        PhotonNetwork.LoadLevel("ConnectionScene");

    }

    // 옮기기
    public void OnClickMove()
    {
        GameManager.instance.myPlayerBehavior.OnClickMove();
        SetBehaviorName("옮기기", 0);

    }

    // 청소하기
    public void OnClickClean()
    {
        GameManager.instance.myPlayerBehavior.OnClickClean();
        SetBehaviorName("청소하기", 1);
    }

    // 칠하기
    public void OnClickPaint()
    {
        GameManager.instance.myPlayerBehavior.OnClickPaint();
        SetBehaviorName("칠하기", 2);
    }

    // 팔기
    public void OnClickSell()
    {
        GameManager.instance.myPlayerBehavior.OnClickSell();
        SetBehaviorName("팔기", 3);
    }

    // 벽 올리기
    public void OnClickBuildWall()
    {
        GameManager.instance.myPlayerBehavior.OnClickBuildWall();
        //SetBehaviorName("벽 올리기", 4);
    }


    // 벽 철거하기
    public void OnClickDemolishWall()
    {
        GameManager.instance.myPlayerBehavior.OnClickDemolishWall();
        SetBehaviorName("철거하기", 4);
    }

    // 타일 및 패널작업하기
    public void OnClickWorkTileAndPanel()
    {
        GameManager.instance.myPlayerBehavior.OnClickWorkTileAndPanel();
        SetBehaviorName("패널 및 타일 작업하기", 5);
    }

    // 화염방사기
    public void OnClickFlame()
    {
        GameManager.instance.myPlayerBehavior.OnClickFlame();
        SetBehaviorName("화염방사기", 6);
    }
    void SetBehaviorName(string name, int id)
    {
        behaviorText.text = name;

        if (prevBehaviorId > -1)
        {
            behaviorThumbs[prevBehaviorId].SetActive(false);
        }

        behaviorThumbs[id].SetActive(true);

        prevBehaviorId = id;
    }
}
