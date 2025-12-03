using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private Vector2 currentDir;
    private Vector2 startPosition;
    private Vector2 currentPosition;
    private bool shouldMove;
    private Rigidbody rb;

    public int i = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (shouldMove)
        {
            Vector3 dir = new(currentDir.x, 0, currentDir.y);
            rb.MovePosition(transform.position + 4f * Time.fixedDeltaTime * dir);
        }
    }

    public void Press(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            shouldMove = true;
        }

        if (context.canceled)
        {
            shouldMove = false;
            currentDir = Vector2.zero;
            i = 0;
        }
    }



    public void Position(InputAction.CallbackContext context)
    {
        if (context.performed && shouldMove)
        {
            if (i == 0)
            {
                startPosition = context.ReadValue<Vector2>();
                i++;
            }

            currentPosition = context.ReadValue<Vector2>();
            currentDir = (currentPosition - startPosition).normalized;
        }

    }


}
