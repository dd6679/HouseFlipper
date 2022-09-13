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
        // ���� ���� ��û
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

        // �� �г��� ����
        string nickName = PhotonNetwork.LocalPlayer.NickName;
        nickName = nickNameInput.text;
        print("����� �г����� " + nickName + " �Դϴ�.");

        // �ι� ���� ��û
        PhotonNetwork.JoinLobby();
    }

    // ���� ����
    public void DisConnect() => PhotonNetwork.Disconnect();

    // ���� ������ �� ȣ��
    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        print("�������");
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        print(System.Reflection.MethodBase.GetCurrentMethod().Name);

        // LobbyScene���� �̵�
        PhotonNetwork.LoadLevel("LobbyScene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
