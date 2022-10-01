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
        {"���� 14��", new List<string>{ "������ ������" } },
        {"�� 25��", new List<string>{"������ ������", "���� �۱�", "����Ʈ ĥ�ϱ�"} },
        {"�� 18��", new List<string>{"������ ������", "���� �۱�", "����Ʈ ĥ�ϱ�"} },
        {"�� 15��", new List<string>{"������ ������", "���� �۱�", "����Ʈ ĥ�ϱ�"} },
        {"�� 45��", new List<string>{"������ ������", "���� �۱�", "����Ʈ ĥ�ϱ�"} },
        { "�Ž� 45��", new List<string> { "������ ������", "���� �۱�", "�� ö���ϱ�", "��Ȳ������ ����Ʈ ĥ�ϱ�" } },
        { "�Ž� 25��", new List<string> { "������ ������", "���� �۱�", "�� ö���ϱ�", "����ȫ������ ����Ʈ ĥ�ϱ�" } },
        {"�ֹ� 22��", new List<string>{"������ ������", "���� �۱�" } },
        {"��� 5��", new List<string>{"���� �۱�", "�� ö���ϱ�"} },
        {"��� 10��", new List<string>{"���� �۱�", "�� ö���ϱ�"} },
        {"��� 13��", new List<string>{"���� �۱�", "�� ö���ϱ�"} },
        {"��� 8��", new List<string>{"���� �۱�", "�� ö���ϱ�"} },
        {"�ٿ뵵�� 8��", new List<string>{"������ ������", "�� ö���ϱ�"} },
        { "��", new List<string> { "������ ������", "���� �۱�", "�ܵ� �����ϱ�" } },
        { "���� 12��", new List<string> { "�� ö���ϱ�", "���� �۱�" } },
        {"���� 42��", new List<string>{"������ ������", "����Ʈ ĥ�ϱ�" } }
        };

        completeQuest = new Dictionary<string, List<string>> {
        {"���� 14��", new List<string>() },
        {"�� 25��", new List<string>() },
        {"�� 18��", new List<string>() },
        {"�� 15��", new List<string>() },
        {"�� 45��", new List<string>() },
        { "�Ž� 45��",new List<string>() },
        { "�Ž� 25��", new List<string>() },
        {"�ֹ� 22��", new List<string>() },
        {"��� 5��", new List<string>() },
        {"��� 10��", new List<string>() },
        {"��� 13��", new List<string>() },
        {"��� 8��", new List<string>() },
        {"�ٿ뵵�� 8��", new List<string>() },
        { "��", new List<string>() },
        { "���� 12��", new List<string>() },
        {"���� 42��", new List<string>() }
        };
    }

    private void Update()
    {
        Text[] allChild = questParent.GetComponentsInChildren<Text>();
        foreach (Text child in allChild)
        {
            if (child.text == "������ ������")
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
