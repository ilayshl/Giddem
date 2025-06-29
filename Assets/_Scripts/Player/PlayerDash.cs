using System.Collections;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    private const float DASH_DURATION = 0.25f;
    private const float DASH_POWER = 4f;
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
                    PlayerManager.Instance.ChangePlayerState(PlayerState.Dash);
                    SetLastInput();
                    Invoke(nameof(ResetDash), DASH_DURATION);
                    _currentDashes++;
                    Debug.Log($"Current dashes: {_currentDashes} out of: {dashLimit}");
                    if (_dashWindow != null) StopCoroutine(_dashWindow);
                    _dashWindow = StartCoroutine(nameof(DashComboWindow), dashCooldown);
                }
            }
        }
    }

    private void Dash(Vector3 input)
    {
        Vector3 dashDirection;
        if (input == Vector3.zero)
        {
        dashDirection = transform.forward * PlayerManager.Instance.currentMoveSpeed * DASH_POWER * Time.fixedDeltaTime;
        }
        else
        {
        dashDirection = (IsometricHelper.ToIso(input).normalized * input.normalized.magnitude) * PlayerManager.Instance.currentMoveSpeed * DASH_POWER * Time.fixedDeltaTime;
        }
         _rb.MovePosition(transform.position + dashDirection);
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
        /* if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
        {
            _lastInput = transform.forward;
        }
        else
        {
            _lastInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        } */
        _lastInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

    }
}
