using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NetworkGame
{
    public class Player : Unit
    {
        public static Player I;

        public int _floor = 1;
        public int Floor { get { return _floor; } }

        [SerializeField] float _speed = 1.0f;

        Rigidbody2D _rigid;

        bool _doingWARP = false;

        void Awake()
        {
            I = this;    
        }

        protected override void Start()
        {
            base.Start();

            _rigid = GetComponent<Rigidbody2D>();
        }

        void FixedUpdate()
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            if (h == 0 && v == 0)
            {
                _anim.SetBool("Run", false);
            }
            else
            {
                _anim.SetBool("Run", true);
            }

            if (_anim.GetBool("eating") == false) //먹는 동작(애니메이션)하고 있는 중이 아니라면....
            {
                Move(h, v);
                Flip(h);
            }
        }

        void Move(float h, float v)
        {
            Vector2 dir = new Vector2(h, v); //방향 벡터

            // 이동 거리 = 방향벡터 * 스피드

            //이동
            //transform.Translate(dir * _speed * Time.deltaTime);
            if (_doingWARP == false)
                _rigid.velocity = dir * _speed * Time.fixedDeltaTime;
            else
                _rigid.velocity = Vector2.zero;
        }

        void Flip(float h)
        {
            if (h < 0)
            {
                //좌
                _renderer.flipX = true;
            }
            else if (h > 0)
            {
                //우
                _renderer.flipX = false;
            }
        }

        //private void OnTriggerEnter2D(Collider2D collision)
        //{
        //    Debug.Log("트리거 이벤트! : " + collision.gameObject.name);

        //    Stair stair = collision.gameObject.GetComponent<Stair>();
        //    if (stair != null) // 트리거 오브젝트가 계단인 경우
        //    {
        //        // 방금 막 이동된 경우는 워프 안함 return
        //        if (_doingWARP == true) return;

        //        // 플레이어를 다음 목적지로 워프 시켜주기
        //        if (stair._destObj != null)
        //        {
        //            transform.position = stair._destObj.transform.position;
        //            StartWARP();
        //        }

        //        if(stair._direction == StairDirection.DOWN)
        //        {
        //            _floor++;
        //            UI_Manager.I.topBar.Refresh();
        //        }
        //        else if (stair._direction == StairDirection.UP)
        //        {
        //            if (_floor == 1)
        //            {
        //                // TODO: 메시지창으로, 최상단 층임을 알리기 (나중에는 UI 디자인이 된 창으로)
        //                // (메시지: "현재는 던전을 벗어날 수 없습니다")

        //                PlatformDialog.Show(
        //                    "안내",
        //                    "현재는 던전을 벗어날 수 없습니다",
        //                    PlatformDialog.Type.SubmitOnly,
        //                    () => {
        //                        Debug.Log("OK");
        //                    },
        //                    null
        //                );
        //            }
        //            else
        //            {
        //                _floor--;
        //                UI_Manager.I.topBar.Refresh();
        //            }
        //        }
        //    }
        //}

        //void StartWARP()
        //{
        //    UI_Manager.I.screenBlock.Play();
        //    _doingWARP = true;
        //    Invoke("StopWARP", 2.0f);
        //}
        //void StopWARP()
        //{
        //    _doingWARP = false;
        //    UI_Manager.I.screenBlock.Stop();
        //}
    }
}