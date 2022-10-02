using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NK_QuestUI : MonoBehaviourPun
{
    public Text location;
    public string locationName;
    public Text questFactory;
    public Image questParent;
    public static Dictionary<string, List<string>> quests;
    public static Dictionary<string, List<string>> completeQuest = new Dictionary<string, List<string>>();

    private void Start()
    {
        quests = new Dictionary<string, List<string>> {
        {"현관 14㎡", new List<string>{ "쓰레기 버리기" } },
        {"방 25㎡", new List<string>{"쓰레기 버리기", "먼지 닦기", "페인트 칠하기"} },
        {"방 18㎡", new List<string>{"쓰레기 버리기", "먼지 닦기", "페인트 칠하기"} },
        {"방 15㎡", new List<string>{"쓰레기 버리기", "먼지 닦기", "페인트 칠하기"} },
        {"방 45㎡", new List<string>{"쓰레기 버리기", "먼지 닦기", "페인트 칠하기"} },
        { "거실 45㎡", new List<string> { "쓰레기 버리기", "먼지 닦기", "벽 철거하기", "주황색으로 페인트 칠하기" } },
        { "거실 25㎡", new List<string> { "쓰레기 버리기", "먼지 닦기", "벽 철거하기", "연분홍색으로 페인트 칠하기" } },
        {"주방 22㎡", new List<string>{"쓰레기 버리기", "먼지 닦기" } },
        {"욕실 5㎡", new List<string>{"먼지 닦기", "벽 철거하기"} },
        {"욕실 10㎡", new List<string>{"먼지 닦기", "벽 철거하기"} },
        {"욕실 13㎡", new List<string>{"먼지 닦기", "벽 철거하기"} },
        {"욕실 8㎡", new List<string>{"먼지 닦기", "벽 철거하기"} },
        {"다용도실 8㎡", new List<string>{"쓰레기 버리기", "벽 철거하기"} },
        { "밖", new List<string> { "쓰레기 버리기", "먼지 닦기", "잔디 제거하기" } },
        { "복도 12㎡", new List<string> { "벽 철거하기", "먼지 닦기" } },
        {"차고 42㎡", new List<string>{"쓰레기 버리기", "페인트 칠하기" } }
        };

        completeQuest = new Dictionary<string, List<string>> {
        {"현관 14㎡", new List<string>() },
        {"방 25㎡", new List<string>() },
        {"방 18㎡", new List<string>() },
        {"방 15㎡", new List<string>() },
        {"방 45㎡", new List<string>() },
        { "거실 45㎡",new List<string>() },
        { "거실 25㎡", new List<string>() },
        {"주방 22㎡", new List<string>() },
        {"욕실 5㎡", new List<string>() },
        {"욕실 10㎡", new List<string>() },
        {"욕실 13㎡", new List<string>() },
        {"욕실 8㎡", new List<string>() },
        {"다용도실 8㎡", new List<string>() },
        { "밖", new List<string>() },
        { "복도 12㎡", new List<string>() },
        {"차고 42㎡", new List<string>() }
        };
    }

    private void Update()
    {
        Text[] allChild = questParent.GetComponentsInChildren<Text>();
        foreach (Text child in allChild)
        {
            if (child.text == "쓰레기 버리기")
            {
                //child.transform.GetChild(0).GetComponent<Text>().text = NK_CompleteQuest.instance.totalTrashPercent.ToString() + "%";
            }
            

            if (completeQuest[location.text].Contains(child.text))
            {
                PhotonNetwork.Destroy(child.gameObject);
            }
        }
        questParent.rectTransform.sizeDelta = new Vector2(500, 55 * allChild.Length);
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
                    PhotonNetwork.Destroy(childList[i].gameObject);
                }
            }

            location.text = locationName;

            for (int i = 0; i < quests[location.text].Count; i++)
            {
                GameObject quest = PhotonNetwork.Instantiate("quest", Vector3.zero, Quaternion.identity);
                quest.transform.SetParent(questParent.transform);
                Text questText = quest.GetComponent<Text>();
                questText.rectTransform.position = questParent.rectTransform.position + new Vector3(100, -12 + -50 * i, 0);
                questText.text = quests[location.text][i];
            }

            questParent.rectTransform.sizeDelta = new Vector2(500, 55 * quests[location.text].Count);
        }
    }
}
