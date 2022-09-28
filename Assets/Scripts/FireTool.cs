using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTool : MonoBehaviour
{
    public GameObject Flames;
    public GameObject Sound;
    public GameObject Light;
    bool isFire = false;

    RaycastHit hit;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        FireShot();
    }

    void FireShot()
    {
        if (Input.GetMouseButton(0))
        {
            Flames.SetActive(true);
            Sound.SetActive(true);
            Light.SetActive(true);
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                if (Vector3.Distance(hit.collider.gameObject.transform.position, transform.position) <= 4f && hit.collider.gameObject.tag.Contains("Weed"))
                {
                    //hit.collider.gameObject.SetActive(false);
                    Destroy(hit.collider.gameObject,0.1f);
                }
            }
        }
            else
            {
                Flames.SetActive(false);
                Sound.SetActive(false);
                Light.SetActive(false);
            }
    }
}
