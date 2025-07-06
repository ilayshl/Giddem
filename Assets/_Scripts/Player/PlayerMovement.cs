using UnityEngine;

/// <summary>
/// WASD and Rotation movement based on Input and PlayerState.
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] CharacterManager playerManager;
    private const int TURN_SPEED = 720;
    private Vector3 _input;
    private Vector3 attackRotation;
    private Rigidbody _rb;

    private LayerMask playerLayer;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        playerManager.OnCharacterStateChanged += OnPlayerStateChanged;
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
        playerManager.OnCharacterStateChanged -= OnPlayerStateChanged;
    }

    private void OnPlayerStateChanged(CharacterState newState)
    {
        switch (newState)
        {
            case CharacterState.Idle:

                break;
            case CharacterState.Run:

                break;
            case CharacterState.Attack:
                attackRotation = CalculateAttackRotation();
                break;
            case CharacterState.Ability:
                Debug.LogWarning("No scripts for special attack!");
                break;
            case CharacterState.Dash:
                LookInstantly(_input);
                break;
            case CharacterState.Stunned:

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
        if (playerManager.state == CharacterState.Attack)
        {
            var rotation = Quaternion.LookRotation(attackRotation, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, TURN_SPEED * Time.deltaTime * 2);
        }
        else if (playerManager.state == CharacterState.Idle || playerManager.state == CharacterState.Run)
        {
            if (_input != Vector3.zero)
            {
                var rotation = Quaternion.LookRotation(IsometricHelper.ToIso(_input), Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, TURN_SPEED * Time.deltaTime);
                playerManager.ChangeCharacterState(CharacterState.Run);
            }
            else
            {
                playerManager.ChangeCharacterState();
            }
        }
        playerManager.magnitude = _input.normalized.magnitude;
    }

    /// <summary>
    /// Moves the object forward.
    /// </summary>
    private void Move()
    {
        if (playerManager.state == CharacterState.Run || playerManager.state == CharacterState.Attack)
        {
            _rb.MovePosition(transform.position + (IsometricHelper.ToIso(_input).normalized * _input.normalized.magnitude)
                * playerManager.currentMoveSpeed * Time.fixedDeltaTime);
        }
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

    private void LookInstantly(Vector3 destination)
    {
        if (destination != Vector3.zero)
        {
            var rotation = Quaternion.LookRotation(IsometricHelper.ToIso(destination), Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, Mathf.Infinity);
        }
    }

}
