using System.Collections;
using UnityEngine;

/// <summary>
/// Dash ability
/// </summary>
public class PlayerDash : MonoBehaviour
{
    private const int DASH_UNITS = 12; //Same as .25 second of dash
    private const float DASH_POWER = 3f; //Multiplier for moveSpeed
    public float dashCooldown = 2f; //Public to be edited in the game via attributes
    [SerializeField] private int dashLimit = 2; //How many consecutive dashes the player can perform before cd
    [SerializeField] Transform forwardTransform; //To create collision ray
    [SerializeField] private LayerMask terrainLayer;
    [SerializeField] private SkinnedMeshRenderer[] meshRenderer;
    private bool _isDashing;
    private Coroutine _activeDashCooldown; //Coroutine handling the cooldown
    private int _currentDashes; //How many consecutive dashes were performed
    private float _dashWindowTime = 1.25f; //How much time needs to elapse between dashes to reset currentDashes
    private Vector3 _lastInput; //Direction of the dash
    private Vector3 _dashDestination; //Last position of the dash
    private Rigidbody _rb;

    private Coroutine activeDashParticles;
    [Header("Dash Particles")]
    [SerializeField] Material silhouetteMaterial;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
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

    void OnEnable()
    {
        PlayerManager.OnPlayerStateChanged += SetDashing;
    }

    void OnDisable()
    {
        PlayerManager.OnPlayerStateChanged -= SetDashing;
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

    private void SetDashing(PlayerState state)
    {
        _isDashing = state == PlayerState.Dash;
    }

    /// <summary>
    /// Does the necessary checks and declares the direction input before a dash
    /// </summary>
    private void PrepareDash()
    {
        if (PlayerManager.Instance.state != PlayerState.Dash) //Check if not already dashing
        {
            if (CanDash())
            {
                SetLastInput();
                PlayerManager.Instance.ChangePlayerState(PlayerState.Dash);
                _dashDestination = CheckDashCollision();
                _currentDashes++;
                if (_activeDashCooldown != null) StopCoroutine(_activeDashCooldown);
                activeDashParticles = StartCoroutine(DashParticles(this.transform, 0.05f, meshRenderer));
                _activeDashCooldown = StartCoroutine(nameof(DashComboWindow), CanDash() ? _dashWindowTime : dashCooldown);
            }
        }
    }

    /// <summary>
    /// Shoots a ray to check for collision with terrain
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    private Vector3 CheckDashCollision()
    {
        float moveSpeed = PlayerManager.Instance.currentMoveSpeed * DASH_POWER * Time.fixedDeltaTime;
        float raycastMaxDistance = moveSpeed * DASH_UNITS;
        RaycastHit hit;
        if (Physics.Raycast(forwardTransform.position, GetDashDirection(), out hit, raycastMaxDistance, (int)terrainLayer))
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
        Vector3 distanceToCalculate = _dashDestination - new Vector3(0, _dashDestination.y, 0);
        if (Vector3.Distance(transform.position, distanceToCalculate) > 0.4)
        {
            _rb.MovePosition(transform.position + GetDashDirection() * PlayerManager.Instance.currentMoveSpeed * DASH_POWER * Time.fixedDeltaTime);
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

    private void ResetDash()
    {
        PlayerManager.Instance.ChangePlayerState();
        StopCoroutine(activeDashParticles);
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

    private bool CanDash()
    {
        return _currentDashes != dashLimit;
    }

    private void SetLastInput()
    {
        _lastInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }
    
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

                Destroy(spawnedObject, 2f);
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    } 
}
