using System.Collections;
using UnityEngine;

public class PlayerTelekinesis : MonoBehaviour
{
    private const float MOVE_FORCE = 700;
    private const float PUSH_MULT = 5;
    private const float TIME_LIMIT = 2;
    [SerializeField] CharacterManager playerManager;
    [SerializeField] PlayerInteract abilityInteract;
    private TelekinesisObject _objectControlled;
    private Coroutine _activeTelekinesis;
    private Coroutine _getInput;
    private Vector3 _input;
    private Rigidbody _rb;

    private void OnEnable()
    {
        playerManager.OnCharacterStateChanged += GetCharacterState;
        abilityInteract.OnInteractAbility += GetInteractedObject;
    }

    private void OnDisable()
    {
        playerManager.OnCharacterStateChanged -= GetCharacterState;
        abilityInteract.OnInteractAbility -= GetInteractedObject;
    }

    private void GetCharacterState(CharacterState state)
    {
        if (state == CharacterState.Telekinesis)
        {

        }
        else if (_objectControlled != null)
        {
            EndAbility(true);
        }
        //Setting the camera's target to the object, then changing it back to the player
    }

    private void GetInteractedObject(InteractableObject interactedObect)
    {
        if (interactedObect.TryGetComponent<TelekinesisObject>(out TelekinesisObject objectToControl))
        {
            if (playerManager.state == CharacterState.Telekinesis)
            {
                _objectControlled = objectToControl;
                _rb = objectToControl.GetComponent<Rigidbody>();
                _getInput = StartCoroutine(GetInput());
                _activeTelekinesis = StartCoroutine(ControlObject(TIME_LIMIT, MOVE_FORCE));
            }
        }
    }

    private IEnumerator ControlObject(float timeLimit, float movePower)
    {
        float timePassed = 0;
        while (playerManager.state == CharacterState.Telekinesis
        && timePassed < timeLimit)
        {
            Debug.Log($"{timePassed} < {timeLimit}");
            MoveObject(movePower);
            timePassed += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        if (_objectControlled != null)
            {
        ShootObject(movePower);
        EndAbility(false);
            }
    }

    private IEnumerator GetInput()
    {
        while (playerManager.state == CharacterState.Telekinesis)
        {
            _input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            yield return null;
        }
    }

    private void MoveObject(float movePower)
    {
        _rb.AddForce((IsometricHelper.ToIso(_input).normalized * _input.normalized.magnitude)
                * MOVE_FORCE * Time.fixedDeltaTime, ForceMode.Acceleration);
    }

    private void ShootObject(float movePower)
    {
        _rb.AddForce((IsometricHelper.ToIso(_input).normalized * _input.normalized.magnitude)
                * (MOVE_FORCE * PUSH_MULT) * Time.fixedDeltaTime, ForceMode.Impulse);
    }

    private void EndAbility(bool wasCanceled)
    {
        _objectControlled = null;
        _activeTelekinesis = null;
        _getInput = null;
        if (!wasCanceled)
        {
        playerManager.ChangeCharacterState();
        }
    }
}
