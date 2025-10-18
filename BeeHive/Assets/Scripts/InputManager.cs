using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Player player;

    private InputAction clickAction;
    private InputAction positionAction;

    private bool isClicked;

    private void Awake()
    {
        clickAction = InputSystem.actions.FindAction("Click");
        positionAction = InputSystem.actions.FindAction("Position");

        clickAction.performed += _ => isClicked = true;
    }

    private void OnEnable()
    {
        clickAction.Enable();
        positionAction.Enable();
    }

    private void OnDisable()
    {
        clickAction.Disable();
        positionAction.Disable();
    }

    private void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (isClicked)
            {
                var pointerPosition = positionAction.ReadValue<Vector2>();
                player.HandleInput(pointerPosition);
            }
        }

        isClicked = false;
    }
}