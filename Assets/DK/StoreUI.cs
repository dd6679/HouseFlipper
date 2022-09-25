using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreUI : MonoBehaviour
{
    public GameObject[] go;
    int size;

    GameObject UI;
    RaycastHit hit;

    void Start()
    {
       UI = GameObject.Find("StoreUI");

        //UI = gameObject.GetComponentInParent<Image>();

    }

    void Update()
    {
        //for (int i = 0; i < size; i++)

        //{
        //    go[i] = (GameObject)Instantiate(go[size]);
        //}

    }



    public void Go0()
    {
        print("11");

        size = 0;


        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;


        //UI.gameObject.SetActive(false);

        GameObject gogo = Instantiate(go[size]);
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out hit))
        {
                gogo.transform.position = hit.point;
        }
    }
    public void Go1()
    {
        size = 1;


        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;


        GameObject gogo  = Instantiate(go[size]);
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out hit))
        {
                gogo.transform.position = hit.point;
        }
    }
    public void Go2()
    {
        size = 2;


        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
        //UI.gameObject.SetActive(false);


        GameObject gogo = Instantiate(go[size]);
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out hit))
        {
            gogo.transform.position = hit.point;
        }
    }
    
    //public void Instantiate(int i)
    //{
    //    i = size;
    //    GameObject gogo = Instantiate(go[i]);
    //    Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
    //    if (Physics.Raycast(ray, out hit))
    //    {
    //        gogo.transform.position = hit.point;
    //    }
    //}
}
