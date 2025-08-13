using UnityEngine;

/// <summary>
/// Rotates an object towards the mouse's current position.
/// </summary>
public class RotateTowardsMouse : MonoBehaviour
{
    Quaternion offset;

    void Start()
    {
        offset = transform.rotation;
    }

    void LateUpdate()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePosition - transform.position;
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = targetRotation * offset;
        }
    }
}
