using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class NK_CompleteDemolish : MonoBehaviourPun
{
    public string location;
    public GameObject[] walls;
    public Image progressBar;
    int currentCount;
    bool isComplete;

    private void Awake()
    {
        progressBar = GameObject.Find("progressBar").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isComplete)
            return;

        int count = 0;
        for (int i = 0; i < walls.Length; i++)
        {
            if (walls[i] == null)
            {
                count++;
            }

            if (i == walls.Length - 1)
            {
                currentCount = count;
                if (count == walls.Length && !NK_QuestUI.completeQuest[location].Contains("�� ö���ϱ�"))
                {
                    NK_QuestUI.completeQuest[location].Add("�� ö���ϱ�");
                    GameManager.instance.myChangeTool.photonView.RPC("RpcProgressBar", RpcTarget.All, progressBar.GetComponent<PhotonView>().ViewID);
                    isComplete = true;
                }
            }
        }
        NK_QuestUI.quests[location]["�� ö���ϱ�"] = 100 / walls.Length * currentCount;
    }
}
