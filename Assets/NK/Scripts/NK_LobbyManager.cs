using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NK_LobbyManager : MonoBehaviourPunCallbacks
{
    // ���̸� InputField
    public InputField inputRoomName;
    // ���ο� InputField
    public InputField InputMaxPlayer;
    // ����� Button
    public Button btnCreate;
    // ������ Button
    public Button btnJoin;

    // ���� ������
    Dictionary<string, RoomInfo> roomCache = new Dictionary<string, RoomInfo>();
    // �븮��Ʈ Content
    public Transform trListContent;

    // map Thumbnail
    public GameObject[] mapThumbs;

    void Start()
    {
        //���̸�(InputField)�� ����� ��, ȣ��Ǵ� �Լ� ���
        inputRoomName.onValueChanged.AddListener(OnRoomNameValueChanged);
        //���ο�(InputField)�� ����� ��, ȣ��Ǵ� �Լ� ���
        InputMaxPlayer.onValueChanged.AddListener(OnMaxPlayerValueChanged);
        //roomName.onValueChanged.AddListener();
    }


    void Update()
    {

    }

    public void OnRoomNameValueChanged(string s)
    {
        //����
        btnJoin.interactable = s.Length > 0;
        //����
        btnCreate.interactable = s.Length > 0 && InputMaxPlayer.text.Length > 0;

        print("OnValueChanged : " + s);
    }

    public void OnMaxPlayerValueChanged(string s)
    {
        // ����
        btnCreate.interactable = s.Length > 0 && inputRoomName.text.Length > 0;
    }
    public void OnSubmit(string s)
    {
        //���࿡ s�� ���̰� 0���� ũ�ٸ�
        if (s.Length > 0)
        {
            CreateRoom();
        }
        print("OnSubmit : " + s);
    }

    //�� ����
    public void CreateRoom()
    {
        // �� �ɼ��� ����
        RoomOptions roomOptions = new RoomOptions();
        // �ִ� �ο� (0�̸� �ִ��ο�)
        roomOptions.MaxPlayers = byte.Parse(InputMaxPlayer.text);
        // �� ����Ʈ�� ������ �ʰ�? ���̰�?
        roomOptions.IsVisible = true;
        // custom ������ ����
        ExitGames.Client.Photon.Hashtable ht = new ExitGames.Client.Photon.Hashtable();
        ht["desc"] = "�ʺ���";
        ht["map_id"] = Random.Range(0, mapThumbs.Length);
        roomOptions.CustomRoomProperties = ht;
        // custom ������ �����ϴ� ����
        roomOptions.CustomRoomPropertiesForLobby = new string[] { "desc", "map_id" };

        // �� ���� ��û (�ش� �ɼ��� �̿��ؼ�)
        PhotonNetwork.CreateRoom(inputRoomName.text, roomOptions);
    }

    //���� �����Ǹ� ȣ�� �Ǵ� �Լ�
    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        print("OnCreatedRoom");
    }

    //�� ������ ���� �ɶ� ȣ�� �Ǵ� �Լ�
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        print("OnCreateRoomFailed , " + returnCode + ", " + message);
    }

    //�� ���� ��û
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(inputRoomName.text);
    }

    //�� ������ �Ϸ� �Ǿ��� �� ȣ�� �Ǵ� �Լ�
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        print("OnJoinedRoom");
        PhotonNetwork.LoadLevel(2);
    }

    //�� ������ ���� �Ǿ��� �� ȣ�� �Ǵ� �Լ�
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        print("OnJoinRoomFailed, " + returnCode + ", " + message);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        base.OnRoomListUpdate(roomList);
        for (int i = 0; i < roomList.Count; i++)
            print(roomList[i].Name);
        // �븮��Ʈ UI�� ��ü ����
        DeleteRoomListUI();
        // �븮��Ʈ ������ ������Ʈ
        UpdateRoomCache(roomList);
        // �븮��Ʈ UI ��ü ����
        CreateRoomListUI();
    }

    void DeleteRoomListUI()
    {
        foreach (Transform tr in trListContent)
        {
            Destroy(tr.gameObject);
        }
    }

    void UpdateRoomCache(List<RoomInfo> roomList)
    {
        for (int i = 0; i < roomList.Count; i++)
        {
            // ����, ����
            if (roomCache.ContainsKey(roomList[i].Name))
            {
                // ���࿡ �ش� ���� ������ ���̶��
                if (roomList[i].RemovedFromList)
                {
                    // roomCache���� �ش� ������ ����
                    roomCache.Remove(roomList[i].Name);
                }
                // �׷����ʴٸ�
                else
                {
                    // ���� ����
                    roomCache[roomList[i].Name] = roomList[i];
                }
            }
            // �߰�
            else
            {
                roomCache.Add(roomList[i].Name, roomList[i]);
            }
        }
    }

    public GameObject roomItemFactory;
    void CreateRoomListUI()
    {
        foreach (RoomInfo info in roomCache.Values)
        {
            // ������� �����.
            GameObject go = Instantiate(roomItemFactory, trListContent);
            // ������� ������ ����(������(0/0))
            NK_RoomItem item = go.GetComponent<NK_RoomItem>();
            item.SetInfo(info);

            // roomItem ��ư�� Ŭ���Ǹ� ȣ��Ǵ� �Լ� ���
            item.onClickAction = SetRoomName;

            string desc = (string)info.CustomProperties["desc"];
            int map_id = (int)info.CustomProperties["map_id"];
            print(desc + ", " + map_id);
        }
    }

    // ���� Thumbnail id
    int prevMapId = -1;
    void SetRoomName(string room, int map_id)
    {
        // ���̸� ����
        inputRoomName.text = room;

        // ���࿡ ���� �� Thumbnail�� Ȱ��ȭ�� �Ǿ��ִٸ�
        if (prevMapId > -1)
        {

            //mapThumbs[prevMapId].SetActive(false);
        }

        // �� Thumbnail ����
        //mapThumbs[map_id].SetActive(true);

        // ���� �� id ����
        prevMapId = map_id;
    }
}
