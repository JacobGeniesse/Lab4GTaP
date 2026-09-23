using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    private float speed = 6f;
    private float horizontalScreenLimit = 10f;
    private float verticalScreenLimit = 6f;

    private PlayerInput playerInput;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerInput = FindAnyObjectByType<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        Vector3 moveInput = playerInput.move.ReadValue<Vector2>();
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

}
