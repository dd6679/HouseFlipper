using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviourPunCallbacks
{
    public Slider loadSlider;

    public GameObject instructor;
    public GameObject mento;

    public GameObject imbage1;
    public GameObject imbage2;
    



    void Start()
    {
        loadSlider.minValue = 0;
        loadSlider.maxValue = 10;
    }

    void Update()
    {
        loadSlider.value += Time.deltaTime;

        if(loadSlider.value > 5)
        {
            instructor.SetActive(false);
            imbage1.SetActive(false);
            mento.SetActive(true);
            imbage2.SetActive(true);
        }
        if(loadSlider.value > 8)
        {
            PhotonNetwork.LoadLevel(3);
        }

    }

    public void OnClick()
    {
        PhotonNetwork.LoadLevel(3);
    }
}
