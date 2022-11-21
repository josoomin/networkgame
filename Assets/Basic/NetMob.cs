using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetMob : NetworkBehaviour
{
    public int _maxHp = 100;

    [SyncVar]
    public int _hp = 0;

    SpriteRenderer _renderer;

    Vector3 _startPos;
    Color _origColor; //original color

    void Start()
    {
        if(isServer)
        {
            //체력 초기화 (서버에서)
            _hp = _maxHp;
        }

        _renderer = GetComponent<SpriteRenderer>();
        _renderer.sortingOrder = 1;

        _origColor = _renderer.color;

        _startPos = transform.position;
    }

    void Update()
    {
        ////서버에서 실행
        //if(isServer)
        //{
        //    Debug.Log("Update Server : " + Time.time);
        //}

        //// 클라이언트에서 실행
        //if(isClient)
        //{
        //    Debug.Log("Update Client : " + Time.time);
        //}

        if (isServer)
        {
            Vector3 pos = transform.position;

            pos.x = _startPos.x + Mathf.PingPong(Time.time, 2.0f);

            transform.position = pos;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isServer)
        {
            _hp -= 10;

            Debug.Log("Collision, attacker : " + collision.gameObject.name);

            NetPlayer netPlayer = collision.gameObject.GetComponent<NetPlayer>();
            if (netPlayer != null)
            {
                Debug.Log("attacker player name: " + netPlayer._playerName);
            }

            Rpc_DoDamageEffect();

            if (_hp <= 0)
            {
                //사망처리
                Destroy(gameObject);
            }
        }
    }

    [ClientRpc]
    void Rpc_DoDamageEffect()
    {
        StartCoroutine(_ChangeColor());
    }

    IEnumerator _ChangeColor()
    {
        _renderer.color = Color.red;

        yield return new WaitForSeconds(0.1f);

        _renderer.color = _origColor;

        yield return new WaitForSeconds(0.1f);

        _renderer.color = Color.red;

        yield return new WaitForSeconds(0.1f);

        _renderer.color = _origColor;

        yield return new WaitForSeconds(0.1f);

        _renderer.color = Color.red;

        yield return new WaitForSeconds(0.1f);

        _renderer.color = _origColor;

    }
}
