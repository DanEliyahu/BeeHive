using UnityEngine;

public class PlayerMoveState : EntityState
{
    public PlayerMoveState(Player player, StateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.SetStopDistance(0);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (ReachedTarget())
            stateMachine.ChangeState(player.IdleState);

        MoveToDestination();
    }

    public override void HandleInput(Vector2 worldPosition, RaycastHit2D raycastHit)
    {
        base.HandleInput(worldPosition, raycastHit);
        if (raycastHit.collider)
        {
            // Possible other actions such as Teleport state...
            stateMachine.ChangeState(player.AttackState);
        }
        else
        {
            stateMachine.ChangeState(player.MoveState);
        }
    }
}