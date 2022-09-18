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
        Text locationName = clickBtn.GetComponentInChildren<Text>();
        for (int i = 0; i < NK_QuestUI.quests[locationName.text].Count; i++)
        {
            GameObject task = Instantiate(taskFactory);
            task.transform.SetParent(GameObject.Find("Tasks").transform);
            taskName = task.GetComponentInChildren<Text>();
            taskName.text = NK_QuestUI.quests[locationName.text][i];
        }
    }
}
