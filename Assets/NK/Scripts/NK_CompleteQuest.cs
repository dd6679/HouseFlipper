using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NK_CompleteQuest : MonoBehaviour
{
    public string location;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.childCount == 0 && gameObject.CompareTag("Trash"))
        {
            NK_QuestUI.quests[location].Remove("������ ������");
        }
        if (gameObject.transform.childCount == 0 && gameObject.CompareTag("Dust"))
        {
            NK_QuestUI.quests[location].Remove("���� �۱�");
        }
    }
}
