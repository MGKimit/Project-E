using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �߷��� �̿��ϴ� ������Ʈ
/// </summary>
public abstract class ThrustComponent : MonoBehaviour
{
    [SerializeField]
    protected bool _canAction = true; // ���� �׼��� ������ �� �ִ��� Ȯ�� --> Ư���� ������ ��� �Ұ����� �� ����

    protected Animator _animator; // �ൿ���� �ִϸ��̼��� ������

    protected Rigidbody2D _rigid;

    [SerializeField]
    protected float _speed = 20;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    public virtual void ResetMotion() { }

    public virtual void ResetMotion(float direction) { }

    public virtual void DoAction() 
    {
        if (!_canAction) return;
    }

    public virtual void DoAction(float direction) 
    {
        if (!_canAction) return;
    }
}