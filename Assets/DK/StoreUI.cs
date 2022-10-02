using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Photon.Pun;


public class StoreUI : MonoBehaviourPun
{
    public GameObject[] Bt;
    public GameObject[] Fur;
    int index = -1;

    GameObject UI;
    RaycastHit hit;


    GameObject Furni;
    GameObject Bath;
    GameObject Paint;
    Button instBt;

    Text location;

    PhotonView photonView;


    private void Awake()
    {
        UI = GameObject.Find("StoreUI");
        Furni = GameObject.Find("Fur");
        Bath = GameObject.Find("Bath");
        Paint = GameObject.Find("Paint");
        instBt = GameObject.Find("instBt").GetComponent<Button>();
        location = GameObject.Find("location").GetComponent<Text>();
    }

    void Start()
    {

        //UI = gameObject.GetComponentInParent<Image>();
        Paint.SetActive(false);
        Bath.SetActive(false);
        photonView = GameManager.instance.photonView;
    }

    void Update()
    {
        //print(index);
    }

    public void Onclick()
    {
        GameObject clickObject = EventSystem.current.currentSelectedGameObject;
        for (int i = 0; i < Bt.Length; i++)
        {
            if (Bt[i] == clickObject)
            {
                index = i;
            }
        }
    }



    public void FurBt()
    {
        Furni.SetActive(true);

        Bath.SetActive(false);
        Paint.SetActive(false);
        instBt.onClick.SetPersistentListenerState(0, UnityEngine.Events.UnityEventCallState.RuntimeOnly);
        instBt.onClick.SetPersistentListenerState(1, UnityEngine.Events.UnityEventCallState.Off);
        instBt.onClick.SetPersistentListenerState(2, UnityEngine.Events.UnityEventCallState.Off);
    }

    public void BathBt()
    {
        Bath.SetActive(true);

        Furni.SetActive(false);
        Paint.SetActive(false);
        instBt.onClick.SetPersistentListenerState(0, UnityEngine.Events.UnityEventCallState.Off);
        instBt.onClick.SetPersistentListenerState(1, UnityEngine.Events.UnityEventCallState.RuntimeOnly);
        instBt.onClick.SetPersistentListenerState(2, UnityEngine.Events.UnityEventCallState.Off);
    }

    public void PaintBt()
    {
        Paint.SetActive(true);

        Bath.SetActive(false);
        Furni.SetActive(false);
        instBt.onClick.SetPersistentListenerState(0, UnityEngine.Events.UnityEventCallState.Off);
        instBt.onClick.SetPersistentListenerState(1, UnityEngine.Events.UnityEventCallState.Off);
        instBt.onClick.SetPersistentListenerState(2, UnityEngine.Events.UnityEventCallState.RuntimeOnly);
    }

    public void Instantiate()
    {
        string furName = Fur[index].name;
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.tag.Contains("Floor"))
            {
                GameObject gogo = PhotonNetwork.Instantiate(furName, hit.point, Quaternion.identity);
                CompleteStoreQuest(furName);
                //gogo.transform.position = hit.point;
                UI.SetActive(false);
            }
        }
    }

    void CompleteStoreQuest(string furName)
    {
        if (furName.Contains("DiningCupboard"))
        {
            NK_QuestUI.completeQuest[location.text].Add("찻장 놓기");
            NK_QuestUI.quests[location.text]["찻장 놓기"] = 100;
        }
        if (furName.Contains("DiningChair"))
        {
            NK_QuestUI.completeQuest[location.text].Add("의자 놓기");
            NK_QuestUI.quests[location.text]["의자 놓기"] = 100;
        }
        if (furName.Contains("DiningTable"))
        {
            NK_QuestUI.completeQuest[location.text].Add("식탁 놓기");
            NK_QuestUI.quests[location.text]["식탁 놓기"] = 100;
        }
        if (furName.Contains("SofaSmall"))
        {
            NK_QuestUI.completeQuest[location.text].Add("작은 소파 놓기");
            NK_QuestUI.quests[location.text]["작은 소파 놓기"] = 100;
        }
    }
}
