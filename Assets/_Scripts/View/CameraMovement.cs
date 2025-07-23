using UnityEngine;

/// <summary>
/// Camera follows a target smoothly.
/// </summary>
public class CameraMovement : MonoBehaviour
{
    public static CameraMovement Instance;

    public Transform Target { get => target; }
    [SerializeField] private Transform target;
    [SerializeField] private float smoothTime = 0.3f;
    private Vector3 _currentVelocity = Vector3.zero;
    private Vector3 _offset;
    private GameObject _cameraAnchor;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
            return;
        }
        Instance = this;

        _offset = transform.position - target.position;
    }

    void FixedUpdate()
    {
        Vector3 targetPosition = target.position + _offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _currentVelocity, smoothTime * Time.deltaTime);
    }

    /// <summary>
    /// Changes the target object of camera, causing it so swiftly slide towards it.
    /// </summary>
    /// <param name="newTarget"></param>
    public void ChangeCameraTarget(Transform newTarget)
    {
        target = newTarget;
        if (target != _cameraAnchor.transform && _cameraAnchor != null)
        {
            Destroy(_cameraAnchor);
        }
    }

    /// <summary>
    /// Spawns an object at the target position, and sets the camera to follow it.
    /// </summary>
    /// <param name="position"></param>
    /// <param name="name"></param>
    public void SpawnCameraAnchor(Vector3 position, string name)
    {
        _cameraAnchor = new GameObject();
        _cameraAnchor.transform.position = position;
        _cameraAnchor.name = name;
        ChangeCameraTarget(_cameraAnchor.transform);
    }

    /// <summary>
    /// Moves the currently spawned Camera Anchor.
    /// </summary>
    /// <param name="targetPosition"></param>
    public void MoveCameraAnchor(Vector3 targetPosition)
    {
        if (_cameraAnchor == null) return;
        _cameraAnchor.transform.position = targetPosition;
    }
}
