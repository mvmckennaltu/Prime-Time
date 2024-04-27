using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    public float rotationSpeed = 30f; // Speed of rotation in degrees per second
    public Vector3 rotationAxis = Vector3.forward; // Axis of rotation (e.g., Vector3.up for Y-axis)

    private void Update()
    {
        // Rotate the platform continuously
        transform.Rotate(rotationAxis, rotationSpeed * Time.deltaTime);
    }
}
