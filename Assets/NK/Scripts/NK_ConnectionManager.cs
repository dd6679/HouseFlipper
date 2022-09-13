using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NK_ConnectionManager : MonoBehaviourPunCallbacks
{
    public InputField nickNameInput;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void Connect()
    {
        // 서버 접속 요청
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnected()
    {
        base.OnConnected();
        print(System.Reflection.MethodBase.GetCurrentMethod().Name);
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        print(System.Reflection.MethodBase.GetCurrentMethod().Name);

        // 내 닉네임 설정
        string nickName = PhotonNetwork.LocalPlayer.NickName;
        nickName = nickNameInput.text;
        print("당신의 닉네임은 " + nickName + " 입니다.");

        // 로버 진입 요청
        PhotonNetwork.JoinLobby();
    }

    // 연결 끊기
    public void DisConnect() => PhotonNetwork.Disconnect();

    // 연결 끊겼을 때 호출
    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        print("연결끊김");
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        print(System.Reflection.MethodBase.GetCurrentMethod().Name);

        // LobbyScene으로 이동
        PhotonNetwork.LoadLevel("LobbyScene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
