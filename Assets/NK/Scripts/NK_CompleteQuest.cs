using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NK_CompleteQuest : MonoBehaviourPun
{
    public string location;
    public Image questParent;
    int questCount;
    int childCount;
    int count = 0;

    PhotonView photonView;

    // Start is called before the first frame update
    void Start()
    {
        questCount = gameObject.transform.childCount;
        childCount = gameObject.transform.childCount;
        photonView = GameManager.instance.photonView;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.CompareTag("Trash"))
        {
            if (gameObject.transform.childCount == 0 && !NK_QuestUI.completeQuest[location].Contains("쓰레기 버리기"))
                NK_QuestUI.completeQuest[location].Add("쓰레기 버리기");
            if (childCount != gameObject.transform.childCount)
            {
                childCount = gameObject.transform.childCount;
                NK_QuestUI.quests[location]["쓰레기 버리기"] = 100 / questCount * (questCount - gameObject.transform.childCount);
                //photonView.RPC("RpcUpdatePercent", RpcTarget.AllBuffered, totalTrashPercent.ToString());
            }
        }
        if (gameObject.CompareTag("Dust"))
        {
            if (gameObject.transform.childCount == 0 && !NK_QuestUI.completeQuest[location].Contains("먼지 닦기"))
                NK_QuestUI.completeQuest[location].Add("먼지 닦기");
            NK_QuestUI.quests[location]["먼지 닦기"] = 100 / questCount * (questCount - gameObject.transform.childCount);
        }
        if (gameObject.CompareTag("Weed"))
        {
            if (gameObject.transform.childCount == 0 && !NK_QuestUI.completeQuest[location].Contains("잔디 제거하기"))
                NK_QuestUI.completeQuest[location].Add("잔디 제거하기");
            NK_QuestUI.quests[location]["잔디 제거하기"] = 100 / questCount * (questCount - gameObject.transform.childCount);
        }
    }
}
