using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 8;
    [SerializeField] private float stopThreshold = 0.5f;

    private Rigidbody2D rb;
    private Camera mainCamera;

    private Vector2 currentDestination;
    private bool shouldMove;
    private float currentStopThreshold;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        MoveToDestination();
    }

    public void SetDestination(Vector2 screenPosition)
    {
        if (!mainCamera) return;

        var worldPosition = mainCamera.ScreenToWorldPoint(screenPosition);
        var hit = Physics2D.Raycast(worldPosition, Vector2.zero);
        if (hit)
        {
            currentDestination = hit.transform.position;
            currentStopThreshold = stopThreshold;
        }
        else
        {
            currentDestination = worldPosition;
            currentStopThreshold = 0;
        }

        shouldMove = true;
    }

    private void MoveToDestination()
    {
        if (Vector2.Distance(transform.position, currentDestination) <= currentStopThreshold)
            shouldMove = false;

        if (!shouldMove) return;

        var movement = Vector2.MoveTowards(transform.position, currentDestination, moveSpeed * Time.fixedDeltaTime);
        rb.MovePosition(movement);
    }
}