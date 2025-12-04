using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed;
    public Transform ballPos;
    private Vector2 currentDir;
    private Vector2 startPosition;
    private Vector2 currentPosition;
    private bool shouldMove;
    private Rigidbody rb;

    private Ball ball;

    private int i = 0;
    public bool hasBall;
    private Vector2 screenPos;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (shouldMove)
        {
            Vector3 dir = new(currentDir.x, 0, currentDir.y);
            rb.MovePosition(transform.position + _speed * Time.fixedDeltaTime * dir);
            transform.LookAt(transform.position + dir);
        }

        if (hasBall)
        {
            ball.transform.position = ballPos.position;
        }

    }

    public void Tap(InputAction.CallbackContext context)
    {
        if (!hasBall) return;

        if (context.performed)
        {

            Ray ray = Camera.main.ScreenPointToRay(screenPos);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {

                hasBall = false;
                Vector3 direction = (hit.point - transform.position).normalized;
                Debug.Log(hit.point - transform.position);
                ball.transform.SetParent(null);
                ball.Launch(direction);

            }
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
                screenPos = startPosition;
                i++;
            }

            currentPosition = context.ReadValue<Vector2>();
            currentDir = (currentPosition - startPosition).normalized;
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            ball = other.gameObject.GetComponent<Ball>();
        }
    }


}
