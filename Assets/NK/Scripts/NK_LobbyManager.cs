using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NK_LobbyManager : MonoBehaviourPunCallbacks
{
    public InputField roomNameInput;
    public byte userNum = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 방 생성
    public void CreateRoom()
    {
        // 방 옵션을 설정
        RoomOptions roomOptions = new RoomOptions();
        // 최대 인원
        roomOptions.MaxPlayers = userNum;
        // 룸 리스트에 보이지 않게? 보이게?
        roomOptions.IsVisible = true;

        // 방 생성 요청 (해당 옵션을 이용해서)
        PhotonNetwork.CreateRoom("HouseRoom", roomOptions);
    }

    // 방이 생성되면 호출되는 함수
    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        print("OnCreatedRoom");
    }

    // 방 생성이 실패될 때 호출되는 함수
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        print("OnCreateRoomFailed , " + returnCode + ", " + message);
        JoinRoom();
    }

    // 방 참가 요청
    public void JoinRoom()
    {
        PhotonNetwork.JoinRandomRoom();
        print(roomNameInput.text + "방에 입장하였습니다.");
    }

    // 방에 입장했을 때 호출
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        print("OnJoinedRoom");
        PhotonNetwork.LoadLevel("GameScene");
    }

    // 랜덤 룸 입장에 실패하면 새로운 방 생성 (master 방 생성)
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        PhotonNetwork.CreateRoom(roomNameInput.text, new RoomOptions { MaxPlayers = userNum });
    }
}
