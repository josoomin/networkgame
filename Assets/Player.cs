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

            if (_anim.GetBool("eating") == false) //�Դ� ����(�ִϸ��̼�)�ϰ� �ִ� ���� �ƴ϶��....
            {
                Move(h, v);
                Flip(h);
            }
        }

        void Move(float h, float v)
        {
            Vector2 dir = new Vector2(h, v); //���� ����

            // �̵� �Ÿ� = ���⺤�� * ���ǵ�

            //�̵�
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
                //��
                _renderer.flipX = true;
            }
            else if (h > 0)
            {
                //��
                _renderer.flipX = false;
            }
        }

        //private void OnTriggerEnter2D(Collider2D collision)
        //{
        //    Debug.Log("Ʈ���� �̺�Ʈ! : " + collision.gameObject.name);

        //    Stair stair = collision.gameObject.GetComponent<Stair>();
        //    if (stair != null) // Ʈ���� ������Ʈ�� ����� ���
        //    {
        //        // ��� �� �̵��� ���� ���� ���� return
        //        if (_doingWARP == true) return;

        //        // �÷��̾ ���� �������� ���� �����ֱ�
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
        //                // TODO: �޽���â����, �ֻ�� ������ �˸��� (���߿��� UI �������� �� â����)
        //                // (�޽���: "����� ������ ��� �� �����ϴ�")

        //                PlatformDialog.Show(
        //                    "�ȳ�",
        //                    "����� ������ ��� �� �����ϴ�",
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