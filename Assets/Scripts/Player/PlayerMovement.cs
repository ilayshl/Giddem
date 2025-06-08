using System;
using System.Collections;
using Unity.Mathematics;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.InputSystem;

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
    private Vector3 attackRotation;
    Matrix4x4 _isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));

    private Rigidbody _rb;
    private AnimationManager _am;

    private LayerMask playerLayer;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _am = GetComponent<AnimationManager>();
        //PlayerManager.OnPlayerStateChanged += 
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
        if (PlayerManager.Instance.state == PlayerState.Attack)
        {
            var rotation = Quaternion.LookRotation(attackRotation, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, turnSpeed * Time.deltaTime);
        }
        else
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
        if (PlayerManager.Instance.state != PlayerState.Attack)
        {
            PlayerManager.Instance.ChangeGameState(PlayerState.Attack);
            currentSpeed *= 0.2f;
            _am.OnAttack();
            attackRotation = CalculateAttackRotation();
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
            if (PlayerManager.Instance.state == PlayerState.Attack)
            {
                _am.StopAttack();
                attackCooldown = StartCoroutine(StartAttackCooldown(ATTACK_COOLDOWN));
                currentSpeed = maxSpeed;
                PlayerManager.Instance.ChangeGameState();
            }
        }
    }

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
