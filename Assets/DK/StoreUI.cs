using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StoreUI : MonoBehaviour
{
    public GameObject[] Bt;
    public GameObject[] Fur;
    public int index = -1;

    GameObject UI;
    RaycastHit hit;

    void Start()
    {
       UI = GameObject.Find("StoreUI");

        //UI = gameObject.GetComponentInParent<Image>();
    }

    void Update()
    {
        print(index);
    }

    public void Onclick()
    {
        GameObject clickObject = EventSystem.current.currentSelectedGameObject;
        for (int i = 0; i < Bt.Length; i++)
        {
            if(Bt[i] == clickObject)
            {
                index = i;
            }
        }
    }


    public void Go0()
    {
        print("11");

        index = 0;


        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;


        //UI.gameObject.SetActive(false);

        GameObject gogo = Instantiate(Fur[index]);
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out hit))
        {
                gogo.transform.position = hit.point;
        }
    }
    

    public void Instantiate(int i)
    {
        GameObject gogo = Instantiate(Fur[index]);
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out hit))
        {
            gogo.transform.position = hit.point;
        }
    }
}
