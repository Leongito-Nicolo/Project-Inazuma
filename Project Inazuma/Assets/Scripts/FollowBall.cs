using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform target;
    public float smooth = 5.0f;

    void Update()
    {
        Vector3 pos = new Vector3(target.position.x, transform.position.y, target.position.z - 10f);
        transform.position = Vector3.Lerp(
            transform.position, pos,
            Time.deltaTime * smooth);
    }

}
