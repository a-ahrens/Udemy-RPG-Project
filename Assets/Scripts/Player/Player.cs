using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity

{
    [Header("Attack Details")]
    public Vector2[] attackMovement;
    public float counterAttackDuration = 0.2f;

    public bool isBusy { get; private set; }

    #region Movement
    [Header("Move Info")]
    public float moveSpeed = 8f;
    public float jumpForce;

    [Header("Dash Info")]
    public float dashSpeed;
    public float dashDuration;
    public float dashDirection { get; private set; }

    #endregion

    public SkillManager skill { get; private set; }

    #region States
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerWallSlideState wallSlideState { get; private set; }
    public PlayerWallJumpState wallJumpState { get; private set; }
    public PlayerDashState dashState { get; private set; }

    public PlayerPrimaryAttackState primaryAttackState { get; private set; }
    public PlayerCounterAttackState counterAttackState { get; private set; }


    #endregion

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState = new PlayerAirState(this, stateMachine, "Jump");
        wallSlideState = new PlayerWallSlideState(this, stateMachine, "WallSlide");
        wallJumpState = new PlayerWallJumpState(this, stateMachine, "WallJump");
        dashState = new PlayerDashState(this, stateMachine, "Dash");

        primaryAttackState = new PlayerPrimaryAttackState(this, stateMachine, "Attack");
        counterAttackState = new PlayerCounterAttackState(this, stateMachine, "CounterAttack");

    }

    protected override void Start()
    {
        base.Start();

        skill = SkillManager.instance;

        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
        CheckForDashInput();
    }

    private void CheckForDashInput()
    {
        if(IsWallDetected())
        {
            return;
        }

        

        if (Input.GetKeyDown(KeyCode.LeftShift) && SkillManager.instance.dash.CanUseSkill())
        {
            
            dashDirection = Input.GetAxisRaw("Horizontal");

            if(dashDirection == 0)
            {
                dashDirection = facingDirection;
            }

            stateMachine.ChangeState(dashState);
        }
    }

    public IEnumerator BusyFor(float seconds)
    {
        isBusy = true;

        yield return new WaitForSeconds(seconds);

        isBusy = false;
    }

    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();







}
