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
    // ChatItem ����
    public GameObject chatItemFactory;
    // ScrollView�� Content
    public RectTransform trContent;

    // �� ���̵� ��
    Color idColor;

    // Start is called before the first frame update
    void Start()
    {
        // InputField���� ���͸� ���� �� ȣ��Ǵ� �Լ� ���
        inputChat.onSubmit.AddListener(OnSubmit);
        // ���콺 Ŀ�� ��Ȱ��ȭ
        Cursor.visible = false;
        

    }

    // InputField���� ���͸� ���� �� ȣ��Ǵ� �Լ�
    private void OnSubmit(string arg)
    {
        string chatText = PhotonNetwork.NickName + " : " + arg;
        photonView.RPC("RpcAddChat", RpcTarget.All, chatText);
        inputChat.text = "";
        inputChat.ActivateInputField();
    }

    // ���� Content�� H
    float prevContentH;
    // ScrollView�� RectTransform
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

        //trScrollView H���� Content H ���� Ŀ����
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
