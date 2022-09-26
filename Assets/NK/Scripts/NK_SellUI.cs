using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NK_SellUI : MonoBehaviourPun
{
    public GameObject sellUI;
    public Text income;
    RaycastHit hit;
    float currentTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        sellUI = GameObject.Find("SellUI");
        income = GameObject.Find("income").gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sellUI.transform.GetChild(0).gameObject.activeSelf == true)
        {
            currentTime += Time.deltaTime;

            if (currentTime > 1)
            {
                sellUI.SetActive(false);
            }
        }
        else
        {
            currentTime = 0;
        }

        if (Input.GetMouseButton(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                if (hit.collider.gameObject.CompareTag("Furniture") && GameManager.instance.myChangeTool.index == (int)NK_ChangeTool.ToolState.SellTool && GameManager.instance.myChangeTool.isMoving)
                {
                    int incomeNum;
                    sellUI.SetActive(true);
                    if (int.TryParse(income.text, out incomeNum))
                    {
                        incomeNum += 1000;
                        income.text = incomeNum.ToString();
                    }
                    if (hit.collider.gameObject.GetComponent<PhotonView>() != null)
                        photonView.RPC("RpcSell", RpcTarget.All, hit.collider.gameObject.GetComponent<PhotonView>().ViewID);
                }
            }
        }
    }

    [PunRPC]
    private void RpcSell(int viewId)
    {
        PhotonView view = PhotonView.Find(viewId);

        if (view != null)
        {
            Destroy(view.gameObject);
        }
    }

}
