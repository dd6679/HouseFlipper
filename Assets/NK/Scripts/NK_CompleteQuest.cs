using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NK_CompleteQuest : MonoBehaviour
{
    public string location;
    int questCount;
    int totalTrashPercent;
    int totalDustPercent;

    // Start is called before the first frame update
    void Start()
    {
        questCount = gameObject.transform.childCount;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.childCount == 0 && gameObject.CompareTag("Trash"))
        {
            NK_QuestUI.completeQuest[location].Add("������ ������");
            totalTrashPercent += (100 / questCount);
        }
        if (gameObject.transform.childCount == 0 && gameObject.CompareTag("Dust"))
        {
            NK_QuestUI.completeQuest[location].Add("���� �۱�");
            totalDustPercent += (100 / questCount);
        }
        if(gameObject.transform.childCount == 0 && gameObject.CompareTag("Weed"))
        {
            NK_QuestUI.completeQuest[location].Add("�ܵ� �����ϱ�");
            totalDustPercent -= (100 / questCount);
        }
    }
}
