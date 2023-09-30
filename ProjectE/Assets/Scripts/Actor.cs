using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : Tree
{
    public Vector2 _moveDirection;
    public Vector2 MoveDirection { get { return _moveDirection; } }

    MoveComponent _moveComponent;
    public MoveComponent MoveComponent { get { return _moveComponent; } }

    DashComponent _dashComponent;
    public DashComponent DashComponent { get { return _dashComponent; } }

    JumpComponent _jumpComponent;
    public JumpComponent JumpComponent { get { return _jumpComponent; } }

    private BaseControl input;
    public BaseControl Input { get { return input; } }

    private void OnEnable()
    {
        input = new BaseControl();
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }

    protected override void Update()
    {
        base.Update();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        _moveComponent = GetComponent<MoveComponent>();
        _dashComponent = GetComponent<DashComponent>();
        _jumpComponent = GetComponent<JumpComponent>();
    }

    private void FixedUpdate()
    {
        Vector2 direction = Input.Player.Movement.ReadValue<Vector2>();
        _moveComponent.DoAction(direction.x);
    }

    protected override Node SetUp()
    {
        Node root = new Selector(
            new List<Node>
            {
                new Sequence(new List<Node> { new CanDash(this), new Dash(this) }),
                new Sequence(new List<Node> { new CanJump(this), new Jump(this) }),
                new Sequence(new List<Node> { new Move(this) })
            }
        );

        return root;
    }
}

public class CanDash : Node
{
    Actor loadActor;
    bool isDashPressed;

    public CanDash(Actor actor) : base()
    {
        loadActor = actor;
    }

    public override NodeState Evaluate()
    {
        isDashPressed = loadActor.Input.Player.Dash.ReadValue<float>() != 0f;

        if (isDashPressed && loadActor.DashComponent.NowDashCooltime == false)
        {
            _state = NodeState.SUCCESS;
        }
        else
        {
            _state = NodeState.FAILURE;
        }

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
        Vector2 direction = loadActor.Input.Player.Movement.ReadValue<Vector2>();
        loadActor.DashComponent.DoAction(direction.x);
        loadActor.DashComponent.ResetMotion();

        _state = NodeState.SUCCESS;
        return _state;
    }
}

public class CanJump : Node
{
    Actor loadActor;
    bool isJumpPressed;

    public CanJump(Actor actor) : base()
    {
        loadActor = actor;
    }

    public override NodeState Evaluate()
    {
        isJumpPressed = loadActor.Input.Player.Jump.ReadValue<float>() != 0f;

        if (isJumpPressed && loadActor.JumpComponent.OnAir == false)
        {
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
        loadActor.JumpComponent.DoAction();
        loadActor.JumpComponent.ResetMotion();

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
        Vector2 direction = loadActor.Input.Player.Movement.ReadValue<Vector2>();
        loadActor.MoveComponent.ResetMotion(direction.x);

        _state = NodeState.SUCCESS;
        return _state;
    }
}
