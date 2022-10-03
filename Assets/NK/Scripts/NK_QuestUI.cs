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
    public GameObject completeText;
    public List<string> kList = new List<string>();
    public static Dictionary<string, Dictionary<string, int>> quests;
    public static Dictionary<string, List<string>> completeQuest = new Dictionary<string, List<string>>();

    private void Awake()
    {
        completeText = GameObject.Find("completeText");
    }

    private void Start()
    {
        quests = new Dictionary<string, Dictionary<string, int>> {
        {"���� 14��", new Dictionary<string, int>{ {"������ ������", 0 } } },
        {"�� 25��", new Dictionary<string, int>{ { "������ ������", 0 }, { "����ȫ������ ����Ʈ ĥ�ϱ�", 0} } },
        {"�� 18��", new Dictionary<string, int>{ { "������ ������", 0 }, { "���� �۱�", 0 }, { "����Ʈ ĥ�ϱ�", 0} } },
        {"�� 15��", new Dictionary<string, int>{ { "������ ������", 0 }, { "���� �۱�", 0 }, { "����Ʈ ĥ�ϱ�", 0 } } },
        {"�� 45��", new Dictionary<string, int>{ { "������ ������", 0 }, { "���� �۱�", 0 }, { "����Ʈ ĥ�ϱ�", 0 } } },
        { "�Ž� 45��", new Dictionary<string, int>{ { "������ ������", 0 }, { "���� �۱�", 0 }, { "�Ͼ������ ����Ʈ ĥ�ϱ�", 0 }, { "���� ����", 0 }, {"��Ź ����", 0 }, { "���� ����", 0 } } },
        { "�Ž� 25��", new Dictionary<string, int>{ {"������ ������", 0 }, { "�� ö���ϱ�", 0 }, { "���� ���� ����", 0 } } },
        {"�ֹ� 22��", new Dictionary<string, int>{ { "������ ������", 0 } } },
        {"��� 5��", new Dictionary<string, int>{ { "�� ö���ϱ�", 0 } } },
        {"��� 10��", new Dictionary<string, int>{ { "�� ö���ϱ�", 0 } } },
        {"��� 13��", new Dictionary<string, int>{ { "�� ö���ϱ�", 0 } } },
        {"��� 8��", new Dictionary<string, int>{ { "�� ö���ϱ�", 0 } } },
        {"�ٿ뵵�� 8��", new Dictionary<string, int>{{"�� ö���ϱ�", 0 }, } },
        { "��", new Dictionary<string, int>{ { "������ ������", 0 }, { "���� �۱�", 0 }, { "�ܵ� �����ϱ�", 0 } } },
        { "���� 12��", new Dictionary<string, int>{ { "���� �۱�", 0 } } },
        {"���� 42��", new Dictionary<string, int>{ { "������ ������", 0 } } }
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
            if (kList.Contains(child.text))
            {
                child.transform.GetChild(0).GetComponent<Text>().text = quests[location.text][child.text].ToString() + "%";
            }

            if (completeQuest[location.text].Contains(child.text) && child.gameObject != null)
            {
                Destroy(child.gameObject);
            }
        }

        if(allChild.Length == 0)
        {
            completeText.SetActive(true);
        }
        else
        {
            completeText.SetActive(false);
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
                GameObject quest = PhotonNetwork.Instantiate("quest", Vector3.zero, Quaternion.identity);
                quest.transform.SetParent(questParent.transform);
                Text questText = quest.GetComponent<Text>();
                questText.rectTransform.position = questParent.rectTransform.position + new Vector3(100, -12 + -50 * i, 0);
                questText.text = kList[i];
            }

            //questParent.rectTransform.sizeDelta = new Vector2(500, 55 * quests[location.text].Count);
        }
    }
}
