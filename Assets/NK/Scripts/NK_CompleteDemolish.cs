using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

public class NK_CompleteDemolish : MonoBehaviourPun
{
    public string location;
    public GameObject[] walls;
    int currentCount;
    bool isComplete;

    // Start is called before the first frame update
    void Start()
    {
        
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
                if (count == walls.Length && !NK_QuestUI.completeQuest[location].Contains("º® Ã¶°ÅÇÏ±â"))
                {
                    NK_QuestUI.completeQuest[location].Add("º® Ã¶°ÅÇÏ±â");
                    isComplete = true;
                }
            }
        }
        NK_QuestUI.quests[location]["º® Ã¶°ÅÇÏ±â"] = 100 / walls.Length * currentCount;
    }
}
