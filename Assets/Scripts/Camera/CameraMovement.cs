using UnityEngine;

/// <summary>
/// Camera follows a target smoothly.
/// </summary>
public class CameraMovement : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] private float smoothTime = 0.3f;
    private Vector3 _currentVelocity = Vector3.zero;
    private Vector3 _offset;

    void Awake()
    {
        _offset = transform.position - target.position;
    }

    void FixedUpdate()
    {
        Vector3 targetPosition = target.position + _offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _currentVelocity, smoothTime * Time.deltaTime);
    }
}
