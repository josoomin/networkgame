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

    //캐릭터 머리 위 이름(3D 텍스트)
    TextMesh _nameText;

    //동기화 변수(SyncVar) [대괄호로 감싸는걸 어트리뷰트]
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

        //카메라 팔로우 스크립트를 붙히기

        NetCamera2DFollow camFollow = Camera.main.gameObject.AddComponent<NetCamera2DFollow>();

        camFollow.target = transform;

        camFollow.damping = 0.5f;
        camFollow.lookAheadFactor = 1.0f;

        //프레이어가 생성되는 시점에 랜덤으로 이름을 생성

        //서버(호스트)에 생성된 이름을 알리기, 커맨드함수 호출
        //string name = "Player" + Random.Range(100, 999);

        //캔버스 아래, 네임 인풋에서 이름 가져오시
        InputField inputName = UI_Manager.I.transform.Find("InputName").GetComponent<InputField>();

        string name = inputName.text;
        CmdSetupPlayer(name); // 플레이어 셋업
    }

    [Command]
    public void CmdSetupPlayer(string name) //호스트(서버)에서 실행되는 코드
    {
        Debug.Log("CmdSetupPlayer : " + name);
        _playerName = name;

        // Canvas아래 UI_Chat에 있는 동기화 변수, _statusText에 메시지를 넣기
        // "플레이어 누구가 입장하였습니다."

        UI_Manager.I.ui_chat._statusText = "플레이어 " + _playerName + "님이 입장하셨습니다.";
    }

    [Command]
    public void CmdSendChatMessage(string msg)
    {
        Debug.Log("서버가 챗 메시지를 받았습니다. : " + msg);

        //동기화 변수를 수정하기
        UI_Manager.I.ui_chat._statusText = msg;
    }

    //플레이어 이동 구현
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