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
    public static Dictionary<string, List<string>> quests = new Dictionary<string, List<string>> {
        {"���� 14��", new List<string>{"���� �۱�", "����Ʈ ĥ�ϱ�"} },
        {"�� 25��", new List<string>{"������ ������", "���� �۱�", "����Ʈ ĥ�ϱ�"} },
        {"�� 18��", new List<string>{"������ ������", "���� �۱�", "����Ʈ ĥ�ϱ�"} },
        {"�� 15��", new List<string>{"������ ������", "���� �۱�", "����Ʈ ĥ�ϱ�"} },
        {"�� 45��", new List<string>{"������ ������", "���� �۱�", "����Ʈ ĥ�ϱ�"} },
        { "�Ž� 45��", new List<string> { "������ ������", "���� �۱�", "�� ö���ϱ�", "����Ʈ ĥ�ϱ�" } },
        { "�Ž� 25��", new List<string> { "������ ������", "���� �۱�", "�� ö���ϱ�", "����Ʈ ĥ�ϱ�" } },
        {"�ֹ� 22��", new List<string>{"������ ������", "���� �۱�" } },
        {"��� 5��", new List<string>{"���� �۱�", "�� ö���ϱ�"} },
        {"��� 10��", new List<string>{"���� �۱�", "�� ö���ϱ�"} },
        {"��� 13��", new List<string>{"���� �۱�", "�� ö���ϱ�"} },
        {"��� 8��", new List<string>{"���� �۱�", "�� ö���ϱ�"} },
        {"�ٿ뵵�� 8��", new List<string>{"������ ������", "�� ö���ϱ�"} },
        { "��", new List<string> { "������ ������" } },
        { "���� 12��", new List<string> { "�� ö���ϱ�" } },
        {"���� 42��", new List<string>{"������ ������", "����Ʈ ĥ�ϱ�" } } };

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
