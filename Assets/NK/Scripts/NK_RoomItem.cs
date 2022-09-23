using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NK_RoomItem : MonoBehaviour
{
    // ���� (�� �̸� (0 / 0))
    public Text roomInfo;

    // ����
    public Text roomDesc;

    // �� id
    int map_id;

    // Ŭ���� �Ǿ��� �� ȣ��Ǵ� �Լ��� ������ �ִ� ����
    public Action<string, int> onClickAction;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetInfo(RoomInfo info)
    {
        name = info.Name;
        roomInfo.text = info.Name + " (" + info.PlayerCount + " / " + info.MaxPlayers + ")";

        // desc ����
        roomDesc.text = (string)info.CustomProperties["desc"];

        // map id ����
        map_id = (int)info.CustomProperties["map_id"];
    }

    public void OnClick()
    {
        // ���࿡ onClickAction�� null�� �ƴ϶��
        if (onClickAction != null)
        {
            // onClickAction ����
            onClickAction(name, map_id);
        }
    }
}
