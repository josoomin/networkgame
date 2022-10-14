using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using UnityStandardAssets._2D;

public class NetPlayer : NetworkBehaviour
{
    public float _speed = 5.0f;
    public Rigidbody2D _rigid;

    //ĳ���� �Ӹ� �� �̸�(3D �ؽ�Ʈ)
    TextMesh _nameText;

    //����ȭ ����(SyncVar) [���ȣ�� ���δ°� ��Ʈ����Ʈ]
    [SyncVar(hook = nameof(OnNameChanged))]
    public string _playerName;

    void OnNameChanged(string oldName, string newName)
    {
        if(_nameText == null)
            _nameText = GetComponentInChildren<TextMesh>();

        _nameText.text = _playerName;

        Debug.Log("OnNameChanged : " + _playerName);
    }

    void Start()
    {
        //_nameText = transform.Find("Info/NameText").GetComponent<TextMesh>();
        _rigid = GetComponent<Rigidbody2D>();
    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();

        UI_Manager.I.ui_chat._player = this;

        //ī�޶� �ȷο� ��ũ��Ʈ�� ������

        NetCamera2DFollow camFollow = Camera.main.gameObject.AddComponent<NetCamera2DFollow>();

        camFollow.target = transform;

        camFollow.damping = 0.5f;
        camFollow.lookAheadFactor = 1.0f;

        //�����̾ �����Ǵ� ������ �������� �̸��� ����

        //����(ȣ��Ʈ)�� ������ �̸��� �˸���, Ŀ�ǵ��Լ� ȣ��
        //string name = "Player" + Random.Range(100, 999);

        //ĵ���� �Ʒ�, ���� ��ǲ���� �̸� ��������
        InputField inputName = UI_Manager.I.transform.Find("InputName").GetComponent<InputField>();

        string name = inputName.text;
        CmdSetupPlayer(name); // �÷��̾� �¾�
    }

    [Command]
    public void CmdSetupPlayer(string name) //ȣ��Ʈ(����)���� ����Ǵ� �ڵ�
    {
        Debug.Log("CmdSetupPlayer : " + name);
        _playerName = name;

        // Canvas�Ʒ� UI_Chat�� �ִ� ����ȭ ����, _statusText�� �޽����� �ֱ�
        // "�÷��̾� ������ �����Ͽ����ϴ�."

        UI_Manager.I.ui_chat._statusText = "�÷��̾� " + _playerName + "���� �����ϼ̽��ϴ�.";
    }

    [Command]
    public void CmdSendChatMessage(string msg)
    {
        Debug.Log("������ ê �޽����� �޾ҽ��ϴ�. : " + msg);

        //����ȭ ������ �����ϱ�
        UI_Manager.I.ui_chat._statusText = msg;
    }

    //�÷��̾� �̵� ����
    void Update()
    {
        if( isLocalPlayer == true)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            Vector2 dir = new Vector2(h, v);

            _rigid.velocity = dir * Time.fixedDeltaTime * _speed * 100.0f;
        }
    }
}