using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustDelete : MonoBehaviour
{
    // �����ð�
    float currentTime = 1f;
    RaycastHit hit;

    void Start()
    {

    }


    void Update()
    {
        
        if (Input.GetMouseButton(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                if (hit.collider.gameObject.tag.Contains("Dust"))
                {
                    Renderer ren = hit.collider.gameObject.GetComponent<Renderer>();
                    currentTime -= Time.deltaTime * 1/2;
                    Color color = ren.material.color;
                    color.a = currentTime;
                    ren.material.color = color;
                    if(color.a < 0.55f)
                    {
                        Destroy(hit.collider.gameObject);
                        currentTime = 1;
                    }
                }
            }
        }
    }
}