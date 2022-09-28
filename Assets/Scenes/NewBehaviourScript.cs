using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
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
                if (hit.collider.gameObject.tag.Contains("Trash"))
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
