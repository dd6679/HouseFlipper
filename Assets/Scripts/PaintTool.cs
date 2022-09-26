using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintTool : MonoBehaviour
{

    RaycastHit hit;

    public Material[] material;
    public int x;
    Renderer ren;
    void Start()
    {
        x = -1;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                if (hit.collider.gameObject.tag.Contains("Paint"))
                {

                }

                if (hit.collider.gameObject.tag.Contains("Wall"))
                {
                    //클릭한 오브젝트의 재질 가져오기
                    Renderer ren = hit.collider.gameObject.GetComponent<Renderer>();

                    //매쉬랜더러 켜줌
                    ren.enabled = true;

                    //페인트툴의 x값이 오브젝트의 재질로 들어감
                    ren.sharedMaterial = material[x];

                    //x값 변경
                    //ChangeColor();
                }
            }
        }
    }

    //public void ChangeColor()
    //{
    //        if (x < 2)
    //        {
    //            x++;
    //        }
    //        else x = 0;
    //}
}
