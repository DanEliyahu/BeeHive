using UnityEngine;

public class PlayerAttackState : EntityState
{
    private Flower target;
    private bool isAttacking;

    public PlayerAttackState(Player player, StateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        target = player.Target.GetComponent<Flower>();
        player.SetStopDistance(player.AttackStopDistance);
    }

    public override void Update()
    {
        base.Update();
        if (isAttacking && target)
        {
            target.TakeDamage(player.DamagePerSecond * Time.deltaTime);
        }

        if (!target)
            stateMachine.ChangeState(player.IdleState);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (isAttacking || ReachedTarget())
            return;
        MoveToDestination();
    }

    public override void Exit()
    {
        base.Exit();
        isAttacking = false;
    }

    public override void HandleInput(Vector2 worldPosition, RaycastHit2D raycastHit)
    {
        base.HandleInput(worldPosition, raycastHit);
        if (raycastHit.collider)
        {
            if (player.Target == target.transform)
            {
                Debug.Log("Attack power increasing");
            }
            else
            {
                stateMachine.ChangeState(player.AttackState);
            }
        }
        else
        {
            stateMachine.ChangeState(player.MoveState);
        }
    }

    protected override bool ReachedTarget()
    {
        if (base.ReachedTarget())
            isAttacking = true;
        return isAttacking;
    }
}