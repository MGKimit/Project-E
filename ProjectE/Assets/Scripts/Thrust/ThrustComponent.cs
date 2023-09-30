using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 추력을 이용하는 컴포넌트
/// </summary>
public abstract class ThrustComponent : MonoBehaviour
{
    [SerializeField]
    protected bool _canAction = true; // 현재 액션을 수행할 수 있는지 확인 --> 특정한 이유로 사용 불가능할 때 쓰임

    protected Animator _animator; // 행동마다 애니메이션이 존재함

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