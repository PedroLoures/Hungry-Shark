using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    public float playerSpeed = 5f;
    public float rotationSpeed = 100f;
    PlayerInput playerInput;

    Vector2 currentMovementInput;
    Vector3 currentMovement;
    bool isMovementPressed;
    float rotationFactorPerFrame = 1f;

    private void Awake()
    {
        playerInput = new PlayerInput();

        playerInput.CharacterControls.Move.started += OnMovementInput;
        playerInput.CharacterControls.Move.canceled += OnMovementInput;
        playerInput.CharacterControls.Move.performed += OnMovementInput;
    }

    void OnMovementInput (InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
        currentMovement.x = currentMovementInput.x;
        currentMovement.y = 0;
        currentMovement.z = currentMovementInput.y;

        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    }

    void HandleRotation()
    {

        if (isMovementPressed)
        {
            transform.Rotate(currentMovement, rotationSpeed * Time.deltaTime);
        }

    }

    private void Update()
    {
        HandleRotation();

        //HandleAnimation would come here

        transform.Translate(Vector3.forward * Time.deltaTime * playerSpeed);
    }

    private void OnEnable()
    {
        {
            playerInput.CharacterControls.Enable();
        }
    }

    private void OnDisable()
    {
        {
            playerInput.CharacterControls.Disable();
        }
    }
}
