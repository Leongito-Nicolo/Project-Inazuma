using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Rigidbody rb;
    private PlayerInput player;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Launch(Vector3 direction)
    {
        rb.AddForce((direction + Vector3.up) * _speed, ForceMode.Impulse);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var playerInstance = other.gameObject.GetComponent<PlayerInput>();
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
