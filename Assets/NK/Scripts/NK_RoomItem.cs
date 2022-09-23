using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NK_RoomItem : MonoBehaviour
{
    // 내용 (방 이름 (0 / 0))
    public Text roomInfo;

    // 설명
    public Text roomDesc;

    // 맵 id
    int map_id;

    // 클릭이 되었을 때 호출되는 함수를 가지고 있는 변수
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

        // desc 설정
        roomDesc.text = (string)info.CustomProperties["desc"];

        // map id 설정
        map_id = (int)info.CustomProperties["map_id"];
    }

    public void OnClick()
    {
        // 만약에 onClickAction가 null이 아니라면
        if (onClickAction != null)
        {
            // onClickAction 실행
            onClickAction(name, map_id);
        }
    }
}
