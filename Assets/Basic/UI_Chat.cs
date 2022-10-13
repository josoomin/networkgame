using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class UI_Chat : NetworkBehaviour
{

    Text _chatText;
    InputField _chatInput;
    public NetPlayer _player = null;

    void Awake()
    {
        _chatText = transform.Find("Scroll View/Viewport/Content/ChatText").GetComponent<Text>();

        _chatInput = transform.parent.Find("InputChat").GetComponent<InputField>();
    }

    [SyncVar (hook = nameof(OnStatusTextChanged))]
    public string _statusText;

    void OnStatusTextChanged(string oldText, string newTexst)
    {
        if(_chatText == null)
        {
            _chatText = transform.Find("Scroll View/Viewport/Content/ChatText").GetComponent<Text>();
        }

        if(_chatText != null)
        {
            _chatText.text = _statusText;
        }
    }

    //InputField�� �̺�Ʈ �Լ�(submit, ��, �Է� �� ���� ������ ��)
    public void OnChatSubmit()
    {
        string chatMessage = _chatInput.text;

        Debug.Log("OnChatSubmit !! " + chatMessage);

        //Ŀ�ǵ��Լ��� ȣ���Ͽ��� �������� ����ȭ ������ _statusText�����ϰ� �ϱ�

        _player.CmdSendChatMessage(chatMessage);
    }
}
