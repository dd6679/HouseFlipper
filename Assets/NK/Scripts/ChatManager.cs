using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatManager : MonoBehaviourPun
{
    // InputChat
    public InputField inputChat;
    // ChatItem 공장
    public GameObject chatItemFactory;
    // ScrollView의 Content
    public RectTransform trContent;

    // 내 아이디 색
    Color idColor;

    // Start is called before the first frame update
    void Start()
    {
        // InputField에서 엔터를 쳤을 때 호출되는 함수 등록
        inputChat.onSubmit.AddListener(OnSubmit);
        // 마우스 커서 비활성화
        Cursor.visible = false;
        

    }

    // InputField에서 엔터를 쳤을 때 호출되는 함수
    private void OnSubmit(string arg)
    {
        string chatText = PhotonNetwork.NickName + " : " + arg;
        photonView.RPC("RpcAddChat", RpcTarget.All, chatText);
        inputChat.text = "";
        inputChat.ActivateInputField();
    }

    // 이전 Content의 H
    float prevContentH;
    // ScrollView의 RectTransform
    public RectTransform trScrollView;

    [PunRPC]
    void RpcAddChat(string chatText)
    {
        prevContentH = trContent.sizeDelta.y;
        GameObject item = Instantiate(chatItemFactory, trContent);
        ChatItem chat = item.GetComponent<ChatItem>();
        chat.SetText(chatText);

        StartCoroutine(AutoScrollBottom());
    }

    IEnumerator AutoScrollBottom()
    {
        yield return null;

        //trScrollView H보다 Content H 값이 커지면
        if (trContent.sizeDelta.y > trScrollView.sizeDelta.y)
        {
            if (trContent.anchoredPosition.y >= prevContentH - trScrollView.sizeDelta.y)
            {
                trContent.anchoredPosition = new Vector2(0, trContent.sizeDelta.y - trScrollView.sizeDelta.y);
            }
        }
    }

    public void OnClickChattingBtn()
    {
        OnSubmit(inputChat.text);
    }
}
