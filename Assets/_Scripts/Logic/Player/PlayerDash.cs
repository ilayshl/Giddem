using System.Collections;
using UnityEngine;

/// <summary>
/// Dash ability
/// </summary>
public class PlayerDash : MonoBehaviour
{
    [SerializeField] CharacterManager playerManager;
    [Header("Dash Data")]
    private const int DASH_UNITS = 8; //Length of the dash
    private const float DASH_POWER = 4f; //Multiplier for moveSpeed
    private const float DESTINATION_RANGE = 0.4f;
    public float dashCooldown = 2f; //Public to be edited in the game via attributes
    [SerializeField] private int dashLimit = 2; //How many consecutive dashes the player can perform before cd
    [SerializeField] Transform forwardTransform; //To create collision ray
    [SerializeField] private LayerMask layerToCollide;
    private bool _isDashing;
    private Coroutine _activeDashCooldown; //Coroutine handling the cooldown
    private int _currentDashes; //How many consecutive dashes were performed
    private float _dashWindowTime = 1.1f; //How much time needs to elapse between dashes to reset currentDashes
    private Vector3 _lastInput; //Direction of the dash
    private Vector3 _dashDestination; //Where the dash should stop at
    private Rigidbody _rb;

    [Header("Dash Particles")]
    [SerializeField] private SkinnedMeshRenderer[] meshRenderer;
    [SerializeField] private Material silhouetteMaterial;
    private Coroutine _activeDashParticles;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        playerManager.OnCharacterStateChanged += SetDashing;
    }

    void OnDisable()
    {
        playerManager.OnCharacterStateChanged -= SetDashing;
    }

    void Update()
    {
        CheckForInput();
    }

    private void FixedUpdate()
    {
        if (_isDashing)
        {
            Dash(_lastInput);
        }
    }

    /// <summary>
    /// Checks for pressed input
    /// </summary>
    private void CheckForInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PrepareDash();
        }
    }

    private void SetDashing(CharacterState state)
    {
        _isDashing = state == CharacterState.Dash;
    }

    /// <summary>
    /// Does the necessary checks and declares the direction input before a dash
    /// </summary>
    private void PrepareDash()
    {
        if (CanDash() && !_isDashing)
        {
            SetLastInput();
            playerManager.ChangeCharacterState(CharacterState.Dash);
            _dashDestination = CheckDashCollision();
            _currentDashes++;
            ChangeGravity();
            _rb.linearVelocity = Vector3.zero;
            LookInstantly(_lastInput);
            if (_activeDashCooldown != null) StopCoroutine(_activeDashCooldown);
            _activeDashParticles = StartCoroutine(DashParticles(this.transform, 0.03f, meshRenderer));
            _activeDashCooldown = StartCoroutine(nameof(DashComboWindow), CanDash() ? _dashWindowTime : dashCooldown);
        }
    }

    /// <summary>
    /// Shoots a ray to check for collision with terrain
    /// </summary>
    /// <returns></returns>
    private Vector3 CheckDashCollision()
    {
        float moveSpeed = playerManager.CurrentMoveSpeed * DASH_POWER * Time.fixedDeltaTime;
        float raycastMaxDistance = moveSpeed * DASH_UNITS;
        RaycastHit hit;
        if (Physics.Raycast(forwardTransform.position, GetDashDirection(), out hit, raycastMaxDistance, (int)layerToCollide))
        {
            Debug.DrawRay(forwardTransform.position, GetDashDirection() * hit.distance, Color.red, 5f);
            return hit.point;
        }
        else
        {
            Debug.DrawRay(forwardTransform.position, GetDashDirection() * raycastMaxDistance, Color.green, 5f);
            return transform.position + GetDashDirection() * raycastMaxDistance;
        }
    }

    /// <summary>
    /// Moves the player until it reaches the dashDestination
    /// </summary>
    /// <param name="input"></param>
    private void Dash(Vector3 input)
    {
        //Vector3 distanceToCalculate = _dashDestination - new Vector3(0, _dashDestination.y, 0);
        Vector2 targetPosition = new Vector2(_dashDestination.x, _dashDestination.z);
        Vector2 playerPosition = new Vector2(transform.position.x, transform.position.z);
        if (Vector3.Distance(playerPosition, targetPosition) > DESTINATION_RANGE)
        {
            _rb.MovePosition(transform.position + GetDashDirection() * playerManager.CurrentMoveSpeed * DASH_POWER * Time.fixedDeltaTime);
        }
        else
        {
            ResetDash();
        }
    }

    /// <summary>
    /// If no input is inserted the dash is set to forward
    /// </summary>
    /// <returns></returns>
    private Vector3 GetDashDirection()
    {
        if (_lastInput == Vector3.zero)
        {
            return transform.forward;
        }
        else
        {
            return IsometricHelper.ToIso(_lastInput).normalized * _lastInput.normalized.magnitude;
        }
    }

    /// <summary>
    /// Stops the coroutine that makes the player move.
    /// </summary>
    private void ResetDash()
    {
        StopCoroutine(_activeDashParticles);
        playerManager.ChangeCharacterState();
        ChangeGravity();
    }

    /// <summary>
    /// Waits time, then resets currentDashes
    /// </summary>
    /// <param name="windowTime"></param>
    /// <returns></returns>
    private IEnumerator DashComboWindow(float windowTime)
    {
        yield return new WaitForSeconds(windowTime);
        _currentDashes = 0;
    }

    /// <summary>
    /// Whether or not the player has reached the limit amount of dashes.
    /// </summary>
    /// <returns></returns>
    private bool CanDash()
    {
        return _currentDashes != dashLimit;
    }

    /// <summary>
    /// Gets the last input before a dash
    /// </summary>
    private void SetLastInput()
    {
        _lastInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }

    /// <summary>
    /// Makes player negate gravity when dashing
    /// </summary>
    private void ChangeGravity()
    {
        _rb.useGravity = !_isDashing;
    }

    /// <summary>
    /// Creates new objects with the current mesh of the player.
    /// </summary>
    /// <param name="position"></param>
    /// <param name="spawnInterval"></param>
    /// <param name="objectRenderer"></param>
    /// <returns></returns>
    private IEnumerator DashParticles(Transform position, float spawnInterval, SkinnedMeshRenderer[] objectRenderer)
    {
        while (true)
        {
            for (int i = 0; i < objectRenderer.Length; i++)
            {
                GameObject spawnedObject = new();
                spawnedObject.transform.SetPositionAndRotation(position.position, position.rotation);
                MeshRenderer spawnedMeshRenderer = spawnedObject.AddComponent<MeshRenderer>();
                MeshFilter spawnedMeshFilter = spawnedObject.AddComponent<MeshFilter>();

                Mesh mesh = new();
                objectRenderer[i].BakeMesh(mesh);
                spawnedMeshFilter.mesh = mesh;

                spawnedMeshRenderer.material = silhouetteMaterial;

                Destroy(spawnedObject, 0.175f);
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    /// <summary>
    /// Rotates the player instantly towards the destination vector
    /// </summary>
    /// <param name="destination"></param>
    private void LookInstantly(Vector3 destination)
    {
        if (destination != Vector3.zero)
        {
            var rotation = Quaternion.LookRotation(IsometricHelper.ToIso(destination), Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, Mathf.Infinity);
        }
    }
}
