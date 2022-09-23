using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    public GameObject ob;
    private Vector3 clickPosition;

    public LayerMask cc;
    void Start()
    {
        
    }

    void Update()
    {

        //구매하기 버튼으로 변경
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 clickPos = Vector3.one;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if(Physics.Raycast(ray, out hit))
            {
                if(hit.collider.gameObject.layer == LayerMask.NameToLayer("Wall"))
                {
                    clickPosition = hit.point;
                    GameObject obb = Instantiate(ob);
                    obb.transform.position = clickPosition;
                }
            }

        }
    }
}
