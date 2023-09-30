using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveComponent : ThrustComponent
{
    SpriteRenderer _spriteRenderer;

    protected override void Start()
    {
        base.Start();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public override void ResetMotion(float moveDirection)
    {
        if (moveDirection == 1)
        {
            _animator.SetBool("NowWalk", true);

            if (_spriteRenderer.flipX == true)
            {
                _spriteRenderer.flipX = false;
            }
        }
        else if (moveDirection == -1)
        {
            _animator.SetBool("NowWalk", true);

            if (_spriteRenderer.flipX == false)
            {
                _spriteRenderer.flipX = true;
            }
        }
        else if (moveDirection == 0)
        {
            _animator.SetBool("NowWalk", false);
        }
    }

    public override void DoAction(float moveDirection)
    {
        _rigid.AddForce(Vector2.right * moveDirection * _speed, ForceMode2D.Force);
    }
}