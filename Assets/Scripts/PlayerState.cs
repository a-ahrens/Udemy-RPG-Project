using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected PlayerStateMachine stateMachine;
    protected Player player;
    private string animBoolName;

    public PlayerState(Player player, PlayerStateMachine stateMachine, string animBoolName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        Debug.Log("I enter " + this.animBoolName);
    }

    public virtual void Update()
    {
        Debug.Log("I'm in " + this.animBoolName);
    }

    public virtual void Exit()
    {
        Debug.Log("I exit " + this.animBoolName);
    }
}
