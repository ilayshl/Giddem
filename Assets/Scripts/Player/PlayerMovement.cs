using System.Collections;
using UnityEngine;

/// <summary>
/// Holds the basic movement of the player.
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    private const float ATTACK_COOLDOWN = 1f;
    [SerializeField] private float maxSpeed = 5;
    [SerializeField] private float turnSpeed = 360;
    [SerializeField] private float punchDashStrength;
    private float currentSpeed;
    private Vector3 _input;
    private Coroutine attackCooldown;
    Matrix4x4 _isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));

    private Rigidbody _rb;
    private AnimationManager _am;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _am = GetComponent<AnimationManager>();
    }

    void Start()
    {
        currentSpeed = maxSpeed;
    }

    void Update()
    {
        GatherInput();
        CheckForClick();
        Look();
    }

    void FixedUpdate()
    {
        Move();
    }

    private void GatherInput()
    {
        _input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }

    private void CheckForClick()
    {
        if (Input.GetKey(KeyCode.Mouse0) && attackCooldown == null)
        {
            StartAttack();
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            FinishAttack();
        }
    }

    /// <summary>
    /// Rotates the object in the correct rotation.
    /// </summary>
    private void Look()
    {
        if (_input != Vector3.zero)
        {
            var rotation = Quaternion.LookRotation(ToIso(_input), Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, turnSpeed * Time.deltaTime);
            _am.SetRunning(true);
        }
        else
        {
            _am.SetRunning(false);
        }
        _am.anim.SetFloat("moveSpeed", _input.normalized.magnitude);
    }

    /// <summary>
    /// Moves the object forward.
    /// </summary>
    private void Move()
    {
        /* if (currentSpeed < maxSpeed)
        {
            currentSpeed += acceleration * Time.fixedDeltaTime;
            currentSpeed = Mathf.Min(currentSpeed, maxSpeed);
        } */
        _rb.MovePosition(transform.position + (ToIso(_input).normalized * _input.normalized.magnitude) * currentSpeed * Time.fixedDeltaTime);
    }

    private Vector3 ToIso(Vector3 input)
    {
        return _isoMatrix.MultiplyPoint3x4(input);
    }

    private IEnumerator StartAttackCooldown(float time)
    {
        yield return new WaitForSeconds(time);
        attackCooldown = null;
    }

    private void StartAttack()
    {
        _am.OnAttack();
            if (currentSpeed == maxSpeed)
            {
                currentSpeed *= 0.2f;
            }
    }

    /// <summary>
    /// To give that dashing effect when attacking
    /// </summary>
    /* public void AttackDash()
    {
        _rb.AddForce(transform.forward * punchDashStrength, ForceMode.Impulse);
    } */

    public void FinishAttack()
    {
        if (attackCooldown == null)
        {
            _am.StopAttack();
            attackCooldown = StartCoroutine(StartAttackCooldown(ATTACK_COOLDOWN));
            if (currentSpeed != maxSpeed)
            {
                currentSpeed = maxSpeed;
            }
        }
    }
}
