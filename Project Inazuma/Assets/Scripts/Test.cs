using UnityEngine;

public class Test : MonoBehaviour
{
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        rb.AddForce((Vector3.forward + Vector3.up) * 10, ForceMode.Impulse);
    }
}
