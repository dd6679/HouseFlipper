using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NK_QuestUI : MonoBehaviour
{
    public Text location;
    public string locationName;
    public Text questFactory;
    public List<string> questNames = new List<string>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            location.text = locationName;

            foreach(string s in questNames)
            {
                Text quest = Instantiate(questFactory);
                quest.transform.SetParent(GameObject.Find("quests").transform);
                quest.text = s;
                //quest.transform
            }
        }
    }
}
