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
    public List<string> kList = new List<string>();
    public static Dictionary<string, Dictionary<string, int>> quests;
    public static Dictionary<string, List<string>> completeQuest = new Dictionary<string, List<string>>();

    private void Start()
    {
        quests = new Dictionary<string, Dictionary<string, int>> {
        {"현관 14㎡", new Dictionary<string, int>{ {"쓰레기 버리기", 0 } } },
        {"방 25㎡", new Dictionary<string, int>{ { "쓰레기 버리기", 0 }, { "페인트 칠하기", 0} } },
        {"방 18㎡", new Dictionary<string, int>{ { "쓰레기 버리기", 0 }, { "먼지 닦기", 0 }, { "페인트 칠하기", 0} } },
        {"방 15㎡", new Dictionary<string, int>{ { "쓰레기 버리기", 0 }, { "먼지 닦기", 0 }, { "페인트 칠하기", 0 } } },
        {"방 45㎡", new Dictionary<string, int>{ { "쓰레기 버리기", 0 }, { "먼지 닦기", 0 }, { "페인트 칠하기", 0 } } },
        { "거실 45㎡", new Dictionary<string, int>{ { "쓰레기 버리기", 0 }, { "먼지 닦기", 0 }, { "노란색으로 페인트 칠하기", 0 }, { "찻창 놓기", 0 }, {"식탁 놓기", 0 }, { "의자 놓기", 0 } } },
        { "거실 25㎡", new Dictionary<string, int>{ {"쓰레기 버리기", 0 }, { "벽 철거하기", 0 } } },
        {"주방 22㎡", new Dictionary<string, int>{ { "쓰레기 버리기", 0 } } },
        {"욕실 5㎡", new Dictionary<string, int>{ { "벽 철거하기", 0 } } },
        {"욕실 10㎡", new Dictionary<string, int>{ { "벽 철거하기", 0 } } },
        {"욕실 13㎡", new Dictionary<string, int>{ { "벽 철거하기", 0 } } },
        {"욕실 8㎡", new Dictionary<string, int>{ { "벽 철거하기", 0 } } },
        {"다용도실 8㎡", new Dictionary<string, int>{{"쓰레기 버리기", 0 }, } },
        { "밖", new Dictionary<string, int>{ { "쓰레기 버리기", 0 }, { "먼지 닦기", 0 }, { "잔디 제거하기", 0 } } },
        { "복도 12㎡", new Dictionary<string, int>{ { "먼지 닦기", 0 } } },
        {"차고 42㎡", new Dictionary<string, int>{ { "쓰레기 버리기", 0 } } }
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
            if (kList.Contains(child.text))
            {
                child.transform.GetChild(0).GetComponent<Text>().text = quests[location.text][child.text].ToString() + "%";
            }

            if (completeQuest[location.text].Contains(child.text))
            {
                Destroy(child.gameObject);
            }
        }
        questParent.rectTransform.sizeDelta = new Vector2(500, 55 * allChild.Length / 2);
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
            kList = new List<string>(quests[location.text].Keys);

            for (int i = 0; i < quests[location.text].Count; i++)
            {
                Text quest = Instantiate(questFactory);
                quest.transform.SetParent(questParent.transform);
                quest.rectTransform.position = questParent.rectTransform.position + new Vector3(100, -12 + -50 * i, 0);
                quest.text = kList[i];
            }

            //questParent.rectTransform.sizeDelta = new Vector2(500, 55 * quests[location.text].Count);
        }
    }

    [PunRPC]
    private void RpcUpdatePercent(string percent)
    {

    }
}
