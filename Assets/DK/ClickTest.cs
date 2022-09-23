using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickTest : MonoBehaviour
{
    public GameObject[] go;
    int i;
    public ScrollRect UI;

    Vector3 ScreenCenter;
    RaycastHit hit;

    void Start()
    {


        UI = GetComponentInParent<ScrollRect>();
        //ScreenCenter = new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2);
    }

    void Update()
    {

    }


    //public void UiRock()
    //{
        
    //    FGo(i);
    //}

    public void FGo1()
    {
        i = 0;

        //Vector3 mos = Input.mousePosition;
        //mos.z = camera.farClipPlane; // 카메라가 보는 방향과, 시야를 가져온다.

        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
        //UI.gameObject.SetActive(false);


        GameObject gogo = Instantiate(go[i]);
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out hit))
        {

                gogo.transform.position = hit.point;
        }
    }
    public void FGo2()
    {
        i = 1;

        //Vector3 mos = Input.mousePosition;
        //mos.z = camera.farClipPlane; // 카메라가 보는 방향과, 시야를 가져온다.

        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
        //UI.gameObject.SetActive(false);


        GameObject gogo = Instantiate(go[i]);
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out hit))
        {
                gogo.transform.position = hit.point;
        }
    }
    public void FGo3()
    {
        i = 2;

        //Vector3 mos = Input.mousePosition;
        //mos.z = camera.farClipPlane; // 카메라가 보는 방향과, 시야를 가져온다.

        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
        //UI.gameObject.SetActive(false);


        GameObject gogo = Instantiate(go[i]);
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out hit))
        {
            //if(hit = null)
                
            gogo.transform.position = hit.point;
        }
    }
}
