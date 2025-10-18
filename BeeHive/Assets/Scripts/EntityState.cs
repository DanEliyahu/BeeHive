using UnityEngine;

public abstract class EntityState
{
    protected readonly Player player;
    protected readonly StateMachine stateMachine;

    protected EntityState(Player player, StateMachine stateMachine)
    {
        this.player = player;
        this.stateMachine = stateMachine;
    }

    public virtual void Enter()
    {
    }

    public virtual void Update()
    {
    }

    public virtual void FixedUpdate()
    {
    }

    public virtual void Exit()
    {
    }

    public virtual void HandleInput(Vector2 worldPosition, RaycastHit2D raycastHit)
    {
    }

    protected void MoveToDestination()
    {
        var movement =
            Vector2.MoveTowards(player.transform.position, player.TargetPosition,
                player.MoveSpeed * Time.fixedDeltaTime);
        player.Rb.MovePosition(movement);
    }

    protected virtual bool ReachedTarget()
    {
        return Vector2.Distance(player.transform.position, player.TargetPosition) <= player.StopDistance;
    }
}