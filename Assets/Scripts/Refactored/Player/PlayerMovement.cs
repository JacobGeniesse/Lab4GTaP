using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    private float speed = 6f;
    private float horizontalScreenLimit = 10f;
    private float verticalScreenLimit = 6f;

    private bool startMoving = false;

    [SerializeField] private InputActionAsset inputList;
    private InputAction move;


    private void OnEnable()
    {
        try
        { 
            move = inputList["Move"];
        }
        catch
        {
            throw new ArgumentException("Unable to find inputList", nameof(PlayerMovement));  
        }

        if (move != null)
        {
            move.started += StartMove;
            move.canceled += EndMove;
        }
    }

    private void OnDisable()
    {
        if(move != null)
        {
            move.started -= StartMove;
            move.canceled -= EndMove;
        }
    }

    void Update()
    {
        //While a movement key is pressed update the movement function
        if(startMoving == true)
        {
            Movement();
        }
    }

    void Movement()
    {
        Vector3 moveInput = move.ReadValue<Vector2>();
        transform.Translate(new Vector3(moveInput.x, moveInput.y, 0) * Time.deltaTime * speed);
        if (transform.position.x > horizontalScreenLimit || transform.position.x <= -horizontalScreenLimit)
        {
            transform.position = new Vector3(transform.position.x * -1f, transform.position.y, 0);
        }
        if (transform.position.y > verticalScreenLimit || transform.position.y <= -verticalScreenLimit)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y * -1, 0);
        }
    }

    void StartMove(InputAction.CallbackContext context)
    {
        startMoving = true;
    }

    void EndMove(InputAction.CallbackContext context)
    {
        startMoving = false;
    }
}
