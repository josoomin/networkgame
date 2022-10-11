using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityStandardAssets._2D;

public class NetPlayer : NetworkBehaviour
{
    public float _speed = 5.0f;
    public Rigidbody2D _rigid;

    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }

    public override void OnStartLocalPlayer()
    {
        //base.OnStartLocalPlayer();

        //ī�޶� �ȷο� ��ũ��Ʈ�� ������

        NetCamera2DFollow camFollow = Camera.main.gameObject.AddComponent<NetCamera2DFollow>();

        camFollow.target = transform;

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