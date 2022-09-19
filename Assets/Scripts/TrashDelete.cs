using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashDelete : MonoBehaviour
{
    public GameObject sellUI;
    float currentTime = 0;

    AudioSource audioSource;
    public AudioClip deleteSound;
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }


    RaycastHit hit;
    Transform tr;

    void Update()
    {
        if (sellUI.activeSelf == true)
        {
            currentTime += Time.deltaTime;

            if (currentTime > 1)
            {
                sellUI.SetActive(false);
            }
        }else
        {
            currentTime = 0;
        }
        if (Input.GetMouseButton(0))
        {

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                if (hit.collider.gameObject.tag.Contains("Trash"))
                {
                    tr = hit.collider.gameObject.transform;
                    

                }

                if (hit.collider.gameObject.CompareTag("Furniture") && NK_ChangeTool.instance.index == (int)NK_ChangeTool.ToolState.SellTool && NK_ChangeTool.instance.isMoving)
                {
                    sellUI.SetActive(true);
                    Destroy(hit.collider.gameObject);
                }
            }
        }
        if (tr != null)
        {
            tr.localScale -= new Vector3(0.05f, 0.05f, 0.05f);
            if (tr.localScale.x <= 0.1f)
            {
                Destroy(tr.gameObject);
                audioSource.PlayOneShot(deleteSound);
            }
        }
    }


}
