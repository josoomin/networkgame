using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetMob : NetworkBehaviour
{
    Vector3 _startPos;

    void Start()
    {
        GetComponent<SpriteRenderer>().sortingOrder = 1;

        _startPos = transform.position;
    }

    void Update()
    {
        Vector3 pos = transform.position;

        pos.x = _startPos.x + Mathf.PingPong(Time.time, 2.0f);

        transform.position = pos;
    }
}
