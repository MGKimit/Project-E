using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpComponent : ThrustComponent
{
    bool _onAir = false;
    public bool OnAir { get { { return _onAir; } } }

    public override void DoAction()
    {
        base.DoAction();

        if (_onAir == true) return;

        _onAir = true;
        _rigid.AddForce(Vector2.up * _speed, ForceMode2D.Impulse);
    }

    public override void ResetMotion()
    {
        if (_animator.GetBool("NowDash") == true) // ���� �뽬 �ִϸ��̼��� ���� ���̶��, �뽬�� ĵ����
        {
            _animator.SetBool("NowDash", false);
        }

        if (_animator.GetBool("NowJump") == false)
        {
            _animator.SetBool("NowJump", true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor") && _onAir == true)
        {
            _onAir = false;
            _animator.SetBool("NowJump", false);
        }
    }
}
