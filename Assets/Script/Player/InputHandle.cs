using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandle : MonoBehaviour
{
    bool shootingInput;
    bool boomInput;
    Vector2 movementInput;

    public bool ShootingInput => shootingInput;
    public bool BoomInput => boomInput;
    public Vector2 MovementInput => movementInput;
    PlayerControls inputActions;
    // Start is called before the first frame update
    void Start()
    {
        if(inputActions == null)
        {
            inputActions = new PlayerControls();
            inputActions.Player.Movement.performed += data => movementInput = data.ReadValue<Vector2>();
            inputActions.Player.Movement.canceled += data => movementInput = data.ReadValue<Vector2>();
            inputActions.Player.Shoot.started += data => shootingInput = true;
            inputActions.Player.Shoot.canceled += data => shootingInput = false;
            inputActions.Player.Boom.started += data => boomInput = true;
            inputActions.Player.Boom.canceled += data => boomInput = false;
        }
        inputActions.Enable();
    }
    private void OnDisable()
    {
        inputActions.Disable();
    }
}
