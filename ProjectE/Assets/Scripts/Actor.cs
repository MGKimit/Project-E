using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : Tree
{
    SpriteRenderer _spriteRenderer;
    Rigidbody2D _rigid;
    Animator _animator;

    public Animator Animator { get { return _animator; } }

    float _movedir;

    private string _currentState;

    CanJump _canJump;

    [SerializeField]
    float _moveSpeed = 20;

    [SerializeField]
    float _jumpThrust = 5;

    [SerializeField]
    float _dashThrust = 8;

    // Start is called before the first frame update
    protected override void Start()
    {
        _canJump = new CanJump(this);

        base.Start();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigid = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D col) => _canJump.OnCollisionEvent(col);

    public void Dash()
    {
        if (_movedir == 0) return;

        if(_rigid.velocity.x > 0 && _movedir == -1 || _rigid.velocity.x < 0 && _movedir == 1) // 움직임과 대쉬가 반대되는 경우
        {
            _rigid.velocity = new Vector2(0, _rigid.velocity.y); // 속도 한번 리셋
        }


        _rigid.AddForce(Vector2.right * _movedir * _dashThrust, ForceMode2D.Impulse);
        _animator.SetTrigger("NowDash");
    }

    public void Jump()
    {
        _rigid.AddForce(Vector2.up * _jumpThrust, ForceMode2D.Impulse);
        _animator.SetBool("NowJump", true);
    }

    public void ResetDirection()
    {
        _movedir = Input.GetAxisRaw("Horizontal");
        if (_movedir == 1)
        {
            _animator.SetBool("NowWalk", true);

            if (_spriteRenderer.flipX == true)
            {
                _spriteRenderer.flipX = false;
            }
        }
        else if (_movedir == -1)
        {
            _animator.SetBool("NowWalk", true);

            if (_spriteRenderer.flipX == false)
            {
                _spriteRenderer.flipX = true;
            }
        }
        else if(_movedir == 0)
        {
            _animator.SetBool("NowWalk", false);
        }
    }

    private void FixedUpdate()
    {
        _rigid.AddForce(Vector2.right * _movedir * _moveSpeed, ForceMode2D.Force);
    }

    protected override Node SetUp()
    {
        Node root = new Selector(
            new List<Node>
            {
                new Sequence(new List<Node> { new CanDash(this), new Dash(this) }),
                new Sequence(new List<Node> { _canJump, new Jump(this) }),
                new Sequence(new List<Node> { new Move(this) })
            }
        );

        return root;
    }
}

public class CanDash : TimerNode
{
    Actor loadActor;

    public CanDash(Actor actor) : base()
    {
        loadActor = actor;
        delay = 2.0f;
    }

    public override NodeState Evaluate()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && nowRunningAction == true)
        {
            _state = NodeState.SUCCESS;
            nowRunningAction = false;
        }
        else _state = NodeState.FAILURE;

        Timer();

        return _state;
    }
}

public class Dash : Node
{
    Actor loadActor;

    public Dash(Actor actor) : base()
    {
        loadActor = actor;
    }

    public override NodeState Evaluate()
    {
        loadActor.Dash();


        _state = NodeState.SUCCESS;
        return _state;
    }
}

public class CanJump : Node
{
    Actor loadActor;
    bool nowJump = false;

    public CanJump(Actor actor) : base()
    {
        loadActor = actor;
    }

    public void OnCollisionEvent(Collision2D col)
    {
        if(col.gameObject.CompareTag("Floor") && nowJump == true)
        {
            nowJump = false;
            loadActor.Animator.SetBool("NowJump", false);
        }
    }

    public override NodeState Evaluate()
    {
        if (Input.GetKeyDown(KeyCode.Space) && nowJump == false)
        {
            nowJump = true;
            _state = NodeState.SUCCESS;
        }
        else _state = NodeState.FAILURE;

        return _state;
    }
}

public class Jump : Node
{
    Actor loadActor;

    public Jump(Actor actor) : base()
    {
        loadActor = actor;
    }

    public override NodeState Evaluate()
    {
        loadActor.Jump();

        _state = NodeState.SUCCESS;
        return _state;
    }
}

public class Move : Node
{
    Actor loadActor;

    public Move(Actor actor) : base()
    {
        loadActor = actor;
    }

    public override NodeState Evaluate()
    {
        loadActor.ResetDirection();

        _state = NodeState.SUCCESS;
        return _state;
    }
}
