using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashComponent : ThrustComponent
{
    WaitForSeconds _seconds;

    [SerializeField]
    float _waitTime = 2f;

    bool _nowDashCooltime = false;
    public bool NowDashCooltime { get { return _nowDashCooltime; } }

    protected override void Start()
    {
        base.Start();
        _seconds = new WaitForSeconds(_waitTime);
    }

    public override void DoAction(float direction)
    {
        base.DoAction();
        if (_nowDashCooltime == true) return;

        if (_rigid.velocity.x > 0 && direction == -1 || _rigid.velocity.x < 0 && direction == 1) // �����Ӱ� �뽬�� �ݴ�Ǵ� ���
        {
            _rigid.velocity = new Vector2(0, _rigid.velocity.y); // �ӵ� �ѹ� ����
        }

        _rigid.AddForce(Vector2.right * direction * _speed, ForceMode2D.Impulse);
        _nowDashCooltime = true;
        StartCoroutine(RunDashCooltime());
    }

    public override void ResetMotion()
    {
        if (_animator.GetBool("NowJump") == true)
        {
            _animator.SetBool("NowJump", false);
        }

        if (_animator.GetBool("NowDash") == false)
        {
            _animator.SetBool("NowDash", true);
        }
    }

    // ���� �ڷ�ƾ �����ֱ�
    IEnumerator RunDashCooltime()
    {
        yield return _seconds;
        _nowDashCooltime = false;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor") && _nowDashCooltime == true)
        {
            _animator.SetBool("NowDash", false);
        }
    }
}
