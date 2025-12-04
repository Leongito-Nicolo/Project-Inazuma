using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Rigidbody rb;
    private PlayerInput player;

    public bool hasPlayer = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {

    }

    public void Launch(Vector3 direction)
    {
        rb.AddForce((direction + Vector3.up) * _speed, ForceMode.Impulse);
        hasPlayer = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (hasPlayer) return;

        if (other.CompareTag("Player"))
        {
            hasPlayer = true;
            var playerInstance = other.gameObject.GetComponent<PlayerInput>();
            var playerMov = other.gameObject.GetComponent<PlayerController>();

            playerMov.hasBall = true;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            transform.SetParent(other.transform);
            transform.position = playerMov._ballPos.position;

            if (player == null)
            {
                player = playerInstance;
            }
            else
            {
                if (player.gameObject != playerInstance.gameObject)
                {
                    player.enabled = false;
                    playerInstance.enabled = true;

                    player = playerInstance;
                }
            }
        }
    }
}
