using System.Collections;
using UnityEngine;

/// <summary>
/// Dash ability
/// </summary>
public class PlayerDash : MonoBehaviour
{
    private const float DASH_DURATION = 0.25f;
    private const float DASH_POWER = 3f;
    public float dashCooldown = 2f; //Public to be edited in the game via attributes
    [SerializeField] private int dashLimit = 2; //How many consecutive dashes the player can perform. 
    [SerializeField] Transform forwardTransform; //To check collision
    [SerializeField] private LayerMask terrainLayer;
    private Coroutine _activeDashCooldown;
    private int _currentDashes;
    private float _dashWindowTime = 1f;
    private Vector3 _lastInput;
    private Vector3 _dashDestination;
    private Rigidbody _rb;

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
        if (PlayerManager.Instance.state == PlayerState.Dash)
        {
            Dash(_lastInput);
        }
    }

    private void CheckForInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (PlayerManager.Instance.state != PlayerState.Dash) //Check if not already dashing
            {
                if (CanDash())
                {
                    SetLastInput();
                    PlayerManager.Instance.ChangePlayerState(PlayerState.Dash);
                    _dashDestination = CheckDashCollision(_lastInput);
                    Invoke(nameof(ResetDash), DASH_DURATION);
                    _currentDashes++;
                    if (_activeDashCooldown != null) StopCoroutine(_activeDashCooldown);
                    _activeDashCooldown = StartCoroutine(nameof(DashComboWindow), CanDash() ? _dashWindowTime : dashCooldown);
                }
            }
        }
    }

    private Vector3 CheckDashCollision(Vector3 input)
    {
        float moveSpeed = PlayerManager.Instance.currentMoveSpeed * DASH_POWER * Time.fixedDeltaTime;
        float raycastMaxDistance = moveSpeed * 12;
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
    private void Dash(Vector3 input)
    {
        Vector3 distanceToCalculate = _dashDestination - new Vector3(0, _dashDestination.y, 0);
        if (Vector3.Distance(transform.position, distanceToCalculate) > 0.4)
        {
            _rb.MovePosition(transform.position + GetDashDirection() * PlayerManager.Instance.currentMoveSpeed * DASH_POWER * Time.fixedDeltaTime);
        }
        else
        {
            CancelInvoke(nameof(ResetDash));
            ResetDash();
        }
    }

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
    }

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
}
