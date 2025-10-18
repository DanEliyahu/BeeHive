using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 8;
    public float MoveSpeed => moveSpeed;
    [SerializeField] private float attackStopDistance = 0.5f;
    public float AttackStopDistance => attackStopDistance;
    public float StopDistance { get; private set; }

    [SerializeField] private float damagePerSecond = 1;
    public float DamagePerSecond => damagePerSecond;
    public Vector2 TargetPosition { get; private set; }
    public Transform Target { get; private set; }

    public Rigidbody2D Rb { get; private set; }
    private Camera mainCamera;

    private StateMachine stateMachine;

    public PlayerIdleState IdleState { get; private set; }
    public PlayerAttackState AttackState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }

    private void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();

        stateMachine = new StateMachine();
        IdleState = new PlayerIdleState(this, stateMachine);
        AttackState = new PlayerAttackState(this, stateMachine);
        MoveState = new PlayerMoveState(this, stateMachine);
    }

    private void Start()
    {
        mainCamera = Camera.main;
        stateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        stateMachine.Update();
    }

    private void FixedUpdate()
    {
        stateMachine.FixedUpdate();
    }

    public void HandleInput(Vector2 screenPosition)
    {
        if (!mainCamera) return;

        Vector2 worldPosition = mainCamera.ScreenToWorldPoint(screenPosition);
        var hit = Physics2D.Raycast(worldPosition, Vector2.zero);
        if (hit.collider)
        {
            Target = hit.transform;
            TargetPosition = Target.position;
        }
        else
        {
            TargetPosition = worldPosition;
        }

        stateMachine.CurrentState.HandleInput(worldPosition, hit);
    }

    public void SetStopDistance(float stopDistance)
    {
        StopDistance = stopDistance;
    }
}