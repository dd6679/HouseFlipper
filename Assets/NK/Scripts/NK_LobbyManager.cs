using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NK_LobbyManager : MonoBehaviourPunCallbacks
{
    // 방이름 InputField
    public InputField inputRoomName;
    // 총인원 InputField
    public InputField InputMaxPlayer;
    // 방생성 Button
    public Button btnCreate;
    // 방참가 Button
    public Button btnJoin;

    // 방의 정보들
    Dictionary<string, RoomInfo> roomCache = new Dictionary<string, RoomInfo>();
    // 룸리스트 Content
    public Transform trListContent;

    // map Thumbnail
    public GameObject[] mapThumbs;

    void Start()
    {
        //방이름(InputField)이 변경될 때, 호출되는 함수 등록
        inputRoomName.onValueChanged.AddListener(OnRoomNameValueChanged);
        //총인원(InputField)이 변경될 때, 호출되는 함수 등록
        InputMaxPlayer.onValueChanged.AddListener(OnMaxPlayerValueChanged);
        //roomName.onValueChanged.AddListener();
    }


    void Update()
    {

    }

    public void OnRoomNameValueChanged(string s)
    {
        //참가
        btnJoin.interactable = s.Length > 0;
        //생성
        btnCreate.interactable = s.Length > 0 && InputMaxPlayer.text.Length > 0;

        print("OnValueChanged : " + s);
    }

    public void OnMaxPlayerValueChanged(string s)
    {
        // 생성
        btnCreate.interactable = s.Length > 0 && inputRoomName.text.Length > 0;
    }
    public void OnSubmit(string s)
    {
        //만약에 s의 길이가 0보다 크다면
        if (s.Length > 0)
        {
            CreateRoom();
        }
        print("OnSubmit : " + s);
    }

    //방 생성
    public void CreateRoom()
    {
        // 방 옵션을 설정
        RoomOptions roomOptions = new RoomOptions();
        // 최대 인원 (0이면 최대인원)
        roomOptions.MaxPlayers = byte.Parse(InputMaxPlayer.text);
        // 룸 리스트에 보이지 않게? 보이게?
        roomOptions.IsVisible = true;
        // custom 정보를 셋팅
        ExitGames.Client.Photon.Hashtable ht = new ExitGames.Client.Photon.Hashtable();
        ht["desc"] = "초보방";
        ht["map_id"] = Random.Range(0, mapThumbs.Length);
        roomOptions.CustomRoomProperties = ht;
        // custom 정보를 공개하는 설정
        roomOptions.CustomRoomPropertiesForLobby = new string[] { "desc", "map_id" };

        // 방 생성 요청 (해당 옵션을 이용해서)
        PhotonNetwork.CreateRoom(inputRoomName.text, roomOptions);
    }

    //방이 생성되면 호출 되는 함수
    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        print("OnCreatedRoom");
    }

    //방 생성이 실패 될때 호출 되는 함수
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        print("OnCreateRoomFailed , " + returnCode + ", " + message);
    }

    //방 참가 요청
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(inputRoomName.text);
    }

    //방 참가가 완료 되었을 때 호출 되는 함수
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        print("OnJoinedRoom");
        PhotonNetwork.LoadLevel(2);
    }

    //방 참가가 실패 되었을 때 호출 되는 함수
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
        // 룸리스트 UI를 전체 삭제
        DeleteRoomListUI();
        // 룸리스트 정보를 업데이트
        UpdateRoomCache(roomList);
        // 룸리스트 UI 전체 생성
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
            // 수정, 삭제
            if (roomCache.ContainsKey(roomList[i].Name))
            {
                // 만약에 해당 룸이 삭제된 것이라면
                if (roomList[i].RemovedFromList)
                {
                    // roomCache에서 해당 정보를 삭제
                    roomCache.Remove(roomList[i].Name);
                }
                // 그렇지않다면
                else
                {
                    // 정보 수정
                    roomCache[roomList[i].Name] = roomList[i];
                }
            }
            // 추가
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
            // 룸아이템 만든다.
            GameObject go = Instantiate(roomItemFactory, trListContent);
            // 룸아이템 정보를 셋팅(방제목(0/0))
            NK_RoomItem item = go.GetComponent<NK_RoomItem>();
            item.SetInfo(info);

            // roomItem 버튼이 클릭되면 호출되는 함수 등록
            item.onClickAction = SetRoomName;

            string desc = (string)info.CustomProperties["desc"];
            int map_id = (int)info.CustomProperties["map_id"];
            print(desc + ", " + map_id);
        }
    }

    // 이전 Thumbnail id
    int prevMapId = -1;
    void SetRoomName(string room, int map_id)
    {
        // 룸이름 설정
        inputRoomName.text = room;

        // 만약에 이전 맵 Thumbnail이 활성화가 되어있다면
        if (prevMapId > -1)
        {

            //mapThumbs[prevMapId].SetActive(false);
        }

        // 맵 Thumbnail 설정
        //mapThumbs[map_id].SetActive(true);

        // 이전 맵 id 저장
        prevMapId = map_id;
    }
}
