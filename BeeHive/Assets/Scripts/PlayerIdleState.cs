using UnityEngine;

public class PlayerIdleState : EntityState
{
    public PlayerIdleState(Player player, StateMachine stateMachine) : base(player, stateMachine)
    {
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