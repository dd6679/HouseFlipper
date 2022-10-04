using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Vehicles.Ball;

public class NK_CompleteWall : MonoBehaviourPun
{
    RaycastHit hit;

    public string location;
    public string color;
    public string colorCode;
    public GameObject[] walls;
    public Image progressBar;
    int currentCount;
    bool isComplete;
    string locationText;

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

        if(GameObject.Find("location") == null)
        {
            return;
        }

        if (location == GameObject.Find("location").GetComponent<Text>().text)
        {
            for (int i = 0; i < walls.Length; i++)
            {
                MeshRenderer mesh = walls[i].GetComponent<MeshRenderer>();
                if (mesh.material.name.Contains(colorCode))
                {
                    count++;
                }
                if (i == walls.Length - 1)
                {
                    currentCount = count;
                    if (count == walls.Length && !NK_QuestUI.completeQuest[location].Contains(color + "으로 페인트 칠하기"))
                    {
                        NK_QuestUI.completeQuest[location].Add(color + "으로 페인트 칠하기");
                        GameManager.instance.myChangeTool.photonView.RPC("RpcProgressBar", RpcTarget.All, progressBar.GetComponent<PhotonView>().ViewID);
                        isComplete = true;
                    }
                }
            }
            NK_QuestUI.quests[location][color + "으로 페인트 칠하기"] = 100 / walls.Length * currentCount;

        }
    }
}
