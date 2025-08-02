using UnityEngine;

/// <summary>
/// WASD and Rotation movement based on Input and PlayerState.
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterManager playerManager;
    [SerializeField] private Transform forwardHeightOffset;
    private Vector3 _input;
    private Rigidbody _rb;


    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
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
        if (playerManager.state == CharacterState.Idle || playerManager.state == CharacterState.Run)
        {
            if (_input != Vector3.zero)
            {
                var rotation = Quaternion.LookRotation(IsometricHelper.ToIso(_input), Vector3.up); //Gets the input rotation
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, playerManager.TurnSpeed * Time.deltaTime);
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
        if (_input == Vector3.zero) return;

        if (playerManager.state == CharacterState.Run || playerManager.state == CharacterState.Attack)
        {
            Vector3 correctDirection = IsometricHelper.ToIso(_input).normalized * _input.normalized.magnitude;
            //_rb.linearVelocity = correctDirection * playerManager.CurrentMoveSpeed;
            RaycastHit hit;
            Debug.DrawLine(transform.position, transform.position + correctDirection *  0.33f, Color.white, 0.1f);
            if (!Physics.Raycast(forwardHeightOffset.position, correctDirection, out hit, .33f))
            {
                _rb.MovePosition(transform.position + correctDirection * playerManager.CurrentMoveSpeed * Time.fixedDeltaTime);
            }
        }
    }

    
}
