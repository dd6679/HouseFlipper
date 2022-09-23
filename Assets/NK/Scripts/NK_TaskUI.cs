using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NK_TaskUI : MonoBehaviour
{
    //public GameObject tasks;
    public GameObject taskFactory;
    Text taskName;
    Text locationName;

    private void Update()
    {
        Text[] allChild = GameObject.Find("Tasks").transform.GetComponentsInChildren<Text>();
        foreach (Text child in allChild)
        {
            if (NK_QuestUI.completeQuest[locationName.text].Contains(child.text))
            {
                //Destroy(child.gameObject);
                child.color = Color.black;
            }
        }
    }

    public void OnClickLocation()
    {
        Transform[] childList = GameObject.Find("Tasks").transform.GetComponentsInChildren<Transform>();

        if (childList != null)
        {
            for (int i = 1; i < childList.Length; i++)
            {
                Destroy(childList[i].gameObject);
            }
        }

        GameObject clickBtn = EventSystem.current.currentSelectedGameObject;
        locationName = clickBtn.GetComponentInChildren<Text>();
        for (int i = 0; i < NK_QuestUI.quests[locationName.text].Count; i++)
        {
            GameObject task = Instantiate(taskFactory);
            task.transform.SetParent(GameObject.Find("Tasks").transform);
            taskName = task.GetComponentInChildren<Text>();
            taskName.text = NK_QuestUI.quests[locationName.text][i];
        }
    }
}
