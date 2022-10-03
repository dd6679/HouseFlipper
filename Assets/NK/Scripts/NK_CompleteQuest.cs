using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NK_CompleteQuest : MonoBehaviourPun
{
    public string location;
    public Image progressBar;
    int questCount;
    int childCount;

    private void Awake()
    {
        progressBar = GameObject.Find("progressBar").GetComponent<Image>();
    }

    // Start is called before the first frame update
    void Start()
    {
        questCount = gameObject.transform.childCount;
        childCount = gameObject.transform.childCount;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.CompareTag("Trash"))
        {
            AddQuest("쓰레기 버리기");
        }
        if (gameObject.CompareTag("Dust"))
        {
            AddQuest("먼지 닦기");
        }
        if (gameObject.CompareTag("Weed"))
        {
            AddQuest("잔디 제거하기");
        }
    }

    private void AddQuest(string quest)
    {
        if (gameObject.transform.childCount == 0 && !NK_QuestUI.completeQuest[location].Contains(quest))
        {
            NK_QuestUI.completeQuest[location].Add(quest);
            GameManager.instance.myChangeTool.photonView.RPC("RpcProgressBar", RpcTarget.All, progressBar.GetComponent<PhotonView>().ViewID);
        }
        if (childCount != gameObject.transform.childCount)
        {
            childCount = gameObject.transform.childCount;
            NK_QuestUI.quests[location][quest] = 100 / questCount * (questCount - gameObject.transform.childCount);
        }
    }
}
