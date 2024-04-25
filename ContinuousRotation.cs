using UnityEngine;

public class ContinuousRotation : MonoBehaviour
{
    public float rotationSpeed = 0.5f; // Speed of rotation in degrees per second

    void Update()
    {
        // Rotate the camera around its X-axis
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
