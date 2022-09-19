using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NK_QuestUI : MonoBehaviour
{
    public Text location;
    public string locationName;
    public Text questFactory;
    public Image questParent;
    public static Dictionary<string, List<string>> quests;

    private void Start()
    {
        quests = new Dictionary<string, List<string>> {
        {"현관", new List<string>{"쓰레기 버리기", "먼지 닦기", "페인트 칠하기"} },
        {"방", new List<string>{"쓰레기 버리기", "먼지 닦기", "페인트 칠하기"} },
        { "거실", new List<string> { "쓰레기 버리기", "먼지 닦기", "벽 철거하기", "페인트 칠하기" } },
        {"주방", new List<string>{"쓰레기 버리기", "먼지 닦기" } },
        {"욕실", new List<string>{"먼지 닦기", "벽 철거하기"} },
        {"다용도실", new List<string>{"쓰레기 버리기", "벽 철거하기"} },
        { "밖", new List<string> { "쓰레기 버리기" } },
        {"차고", new List<string>{"쓰레기 버리기", "페인트 칠하기" } } };
    }

    private void Update()
    {
        Text[] allChild = questParent.GetComponentsInChildren<Text>();
        foreach(Text child in allChild)
        {
            if (!quests[location.text].Contains(child.text))
            {
                child.enabled = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Transform[] childList = questParent.GetComponentsInChildren<Transform>();

            if (childList != null)
            {
                for (int i = 1; i < childList.Length; i++)
                {
                    Destroy(childList[i].gameObject);
                }
            }

            location.text = locationName;

            for (int i = 0; i < quests[location.text].Count; i++)
            {
                Text quest = Instantiate(questFactory);
                quest.transform.SetParent(questParent.transform);
                quest.rectTransform.position = questParent.rectTransform.position + new Vector3(100, -12 + -50 * i, 0);
                quest.text = quests[location.text][i];
            }

            questParent.rectTransform.sizeDelta = new Vector2(500, 55 * quests[location.text].Count);
        }
    }
}
