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

        //카메라 팔로우 스크립트를 붙히기

        NetCamera2DFollow camFollow = Camera.main.gameObject.AddComponent<NetCamera2DFollow>();

        camFollow.target = transform;

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