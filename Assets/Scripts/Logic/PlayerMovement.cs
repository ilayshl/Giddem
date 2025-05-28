using UnityEngine;

/// <summary>
/// Holds the basic movement of the player.
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private float turnSpeed = 360;
    private Vector3 _input;
    Matrix4x4 _isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));

    private Rigidbody _rb;
    private AnimationManager _am;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _am = GetComponent<AnimationManager>();
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

    private void GatherInput()
    {
        _input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }

    private void Look()
    {
        if (_input != Vector3.zero)
        {
            var rotation = Quaternion.LookRotation(ToIso(_input), Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, turnSpeed * Time.deltaTime);
        }
        _am.anim.SetFloat("moveSpeed", _input.magnitude);
    }

    private void Move()
    {
        _rb.MovePosition(transform.position + (ToIso(_input).normalized * _input.normalized.magnitude) * moveSpeed * Time.deltaTime);
    }

    private Vector3 ToIso(Vector3 input)
    {
        return _isoMatrix.MultiplyPoint3x4(input);
    }
}
