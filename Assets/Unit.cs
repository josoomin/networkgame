using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NetworkGame
{
public class Unit : MonoBehaviour
    {
        public int _hp;
        public int _maxHp;  //�ν����Ϳ��� �����ֱ�
        public Color _origColor;

        protected Animator _anim;
        protected SpriteRenderer _renderer;

        protected virtual void Start()
        {
            _anim = GetComponent<Animator>();
            _renderer = GetComponent<SpriteRenderer>();

            // ü�� �ʱ�ȭ
            _hp = _maxHp;
        }

        void Update()
        {
            
        }

        public void DoDamage(int damage)
        {
            _hp -= damage;

            DoDamageEffect();

            if ( _hp <= 0)
            {
                //���ӿ���ó��
               // UI_Manager.I.Gameover.Show();
            }
        }

        void DoDamageEffect()
        {
            StartCoroutine(_ChangeColor());
        }

        IEnumerator _ChangeColor()
        {
            _origColor = _renderer.color;

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
}