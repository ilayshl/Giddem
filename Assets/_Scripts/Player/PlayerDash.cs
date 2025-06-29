using System.Collections;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    private const float DASH_DURATION = 0.25f;
    private const float DASH_POWER = 3f;
    public float dashCooldown = 1.5f; //Public to be edited in the game via attributes
    [SerializeField] private int dashLimit = 2; //How many consecutive dashes the player can perform. 
    private Coroutine _dashWindow;
    private Vector3 _lastInput;
    private Rigidbody _rb;
    private int _currentDashes;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        CheckForDash();
    }

    private void FixedUpdate()
    {
        if (PlayerManager.Instance.state == PlayerState.Dash)
        {
            Dash(_lastInput);
        }
    }

    private void CheckForDash()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (PlayerManager.Instance.state != PlayerState.Dash) //Check if not already dashing
            {
                if (CanDash())
                {
                    SetLastInput();
                    PlayerManager.Instance.ChangePlayerState(PlayerState.Dash);
                    Rotate();
                    Invoke(nameof(ResetDash), DASH_DURATION);
                    _currentDashes++;
                    Debug.Log($"Current dashes: {_currentDashes} out of: {dashLimit}");
                    if (_dashWindow != null) StopCoroutine(_dashWindow);
                    _dashWindow = StartCoroutine(nameof(DashComboWindow), dashCooldown);
                }
            }
        }
    }

    private void Rotate()
    {
        if (_lastInput == Vector3.zero) return;
        var rotation = Quaternion.LookRotation(IsometricHelper.ToIso(_lastInput), Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, Mathf.Infinity);
    }

    private void Dash(Vector3 input)
    {
        Vector3 dashDirection;
        if (input == Vector3.zero)
        {
            dashDirection = transform.forward;
        }
        else
        {
            dashDirection = (IsometricHelper.ToIso(input).normalized * input.normalized.magnitude);
        }
        _rb.MovePosition(transform.position + dashDirection * PlayerManager.Instance.currentMoveSpeed * DASH_POWER * Time.fixedDeltaTime);
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
