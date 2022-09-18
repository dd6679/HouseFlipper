using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashDelete : MonoBehaviour
{
    void Start()
    {
        
    }


    RaycastHit hit;
    float timer;
    float timer2 = 1f;
    Transform tr;
    void Update()
    {
        if (tr != null)
        {
            tr.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
            if (tr.localScale.x <= 0.1f)
            {
                Destroy(tr.gameObject);
            }
        }
        if (Input.GetMouseButton(0))
        {

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                //tr = hit.collider.gameObject.GetComponent<Transform>();
                if (hit.collider.gameObject.tag.Contains("Trash"))
                {

                    tr = hit.collider.gameObject.transform;
                //hit.collider.gameObject.transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f);

            
                }
            }
        }
    }

   
}
