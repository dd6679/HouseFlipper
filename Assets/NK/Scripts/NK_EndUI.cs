using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NK_EndUI : MonoBehaviour
{

    public GameObject Cine;
    public GameObject canvas1;
    public GameObject canvas2;
    public GameObject canvas3;

    public GameObject wall1;
    public GameObject wall2;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            GameObject[] fur = GameObject.FindGameObjectsWithTag("Furniture");
            for (int i = 0; i < fur.Length; i++)
            {
                if (fur[i].GetComponent<Outline>() != null)
                {
                    fur[i].GetComponent<Outline>().enabled = false;
                }
            }

            //PhotonNetwork.LoadLevel("ConnectionScene");
            GameObject Player = GameObject.FindGameObjectWithTag("Player");
            Camera cam = Player.GetComponentInChildren<Camera>();
            cam.gameObject.SetActive(false);

            Cine.SetActive(true);
            canvas1.SetActive(false);
            canvas2.SetActive(false);
            canvas3.SetActive(false);
            gameObject.SetActive(false);
            wall1.SetActive(false);
            wall2.SetActive(false);
        }
    }
}
