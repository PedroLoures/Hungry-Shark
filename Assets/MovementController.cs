using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    PlayerInput playerInput;

    Vector3 currentMovementInput;
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
        currentMovementInput = context.ReadValue<Vector3>();
        currentMovement.x = currentMovementInput.x;
        currentMovement.y = currentMovementInput.y;
        currentMovement.z = currentMovementInput.z;

        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0 || currentMovementInput.z != 0;
    }

    void HandleRotation()
    {
        Quaternion currentRotation = transform.rotation;

        if (isMovementPressed)
        {
            Quaternion targetRotation = Quaternion.LookRotation(currentMovement);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame * Time.deltaTime);
        }

    }

    private void Update()
    {
        HandleRotation();

        //HandleAnimation would come here

        transform.Translate(currentMovement * Time.deltaTime);
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
