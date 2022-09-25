using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StoreUI : MonoBehaviour
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


    private void Awake()
    {
        UI = GameObject.Find("StoreUI");
        Furni = GameObject.Find("Fur");
        Bath = GameObject.Find("Bath");
        Paint = GameObject.Find("Paint");
        instBt = GameObject.Find("instBt").GetComponent<Button>();
    }

    void Start()
    {

        //UI = gameObject.GetComponentInParent<Image>();
        Paint.SetActive(false);
        Bath.SetActive(false);


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
            if(Bt[i] == clickObject)
            {
                print(index);
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


    public void Instantiate(int i)
    {
        GameObject gogo = Instantiate(Fur[index]);
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out hit))
        {
            gogo.transform.position = hit.point;
            UI.SetActive(false);
        }
    }
}
