using UnityEngine;

/// <summary>
/// WASD and Rotation movement based on Input and PlayerState.
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    private const int TURN_SPEED = 720;
    private Vector3 _input;
    private Vector3 attackRotation;
    Matrix4x4 _isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));

    private Rigidbody _rb;

    private LayerMask playerLayer;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        PlayerManager.OnPlayerStateChanged += OnPlayerStateChanged;
    }

    void Update()
    {
        GatherInput();
        Look();
    }

    void FixedUpdate()
    {
        Move();
    }

    void OnDestroy()
    {
        PlayerManager.OnPlayerStateChanged -= OnPlayerStateChanged;
    }

    private void OnPlayerStateChanged(PlayerState newState)
    {
        switch (newState)
        {
            case PlayerState.Idle:

                break;
            case PlayerState.Run:

                break;
            case PlayerState.Attack:
                attackRotation = CalculateAttackRotation();
                break;
            case PlayerState.Skill:
                Debug.LogWarning("No scripts for special attack!");
                break;
            case PlayerState.Inactive:

                break;
        }
    }

    /// <summary>
    /// Gets the current input as a Vector3.
    /// </summary>
    private void GatherInput()
    {
        _input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }



    /// <summary>
    /// Rotates the object in the correct rotation.
    /// </summary>
    private void Look()
    {
        if (PlayerManager.Instance.state == PlayerState.Attack)
        {
            var rotation = Quaternion.LookRotation(attackRotation, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, TURN_SPEED * Time.deltaTime);
        }
        else
        {

            if (_input != Vector3.zero)
            {
                var rotation = Quaternion.LookRotation(ToIso(_input), Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, TURN_SPEED * Time.deltaTime);
                PlayerManager.Instance.ChangePlayerState(PlayerState.Run);
            }
            else
            {
                PlayerManager.Instance.ChangePlayerState();
            }
        }
        PlayerManager.Instance.input = _input.normalized.magnitude;

    }

    /// <summary>
    /// Moves the object forward.
    /// </summary>
    private void Move()
    {
        _rb.MovePosition(transform.position + (ToIso(_input).normalized * _input.normalized.magnitude) * PlayerManager.Instance.currentMoveSpeed * Time.fixedDeltaTime);
    }

    /// <summary>
    /// Changes a Vector3 to fit an Isometric POV.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    private Vector3 ToIso(Vector3 input)
    {
        return _isoMatrix.MultiplyPoint3x4(input);
    }

    /// <summary>
    /// Returns the Vector3 position of the mouse in the isometric world.
    /// </summary>
    /// <returns></returns>
    private Vector3 CalculateAttackRotation()
    {
        Vector3 mouse = Input.mousePosition;
        Ray castPoint = Camera.main.ScreenPointToRay(mouse);
        RaycastHit hit;
        if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, ~playerLayer))
        {
            Vector3 requiredHitPoint;
            Vector3 playerHeight = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            Vector3 hitPoint = new Vector3(hit.point.x, hit.point.y, hit.point.z);
            float length = Vector3.Distance(playerHeight, hitPoint);
            var degree = 30;
            var radian = degree * Mathf.Deg2Rad;
            float hypote = length / (Mathf.Sin(radian));
            float distanceFromCamera = hit.distance;

            if (transform.position.y > hit.point.y)
            {
                requiredHitPoint = castPoint.GetPoint(distanceFromCamera - hypote);
            }
            else if (transform.position.y < hit.point.y)
            {
                requiredHitPoint = castPoint.GetPoint(distanceFromCamera + hypote);
            }
            else
            {
                requiredHitPoint = castPoint.GetPoint(distanceFromCamera);
            }
            return requiredHitPoint - transform.position;
        }
        return transform.forward;
    }

}
