using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class NK_CompleteDemolish : MonoBehaviourPun
{
    public GameObject[] walls;
    int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < walls.Length; i++)
        {
            if (walls[i] == null)
            {
                count++;
            }
        }
        print(count);
    }
}
