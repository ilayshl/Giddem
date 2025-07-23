using System.Collections;
using UnityEngine;

/// <summary>
/// Logic for player's grapple ability.
/// </summary>
public class PlayerGrapple : MonoBehaviour
{
    private const float MOVE_SPEED = 3;
    private const float TIME_LIMIT = 0.5f;
    [SerializeField] CharacterManager playerManager;
    [SerializeField] PlayerInteract abilityInteract;
    private LineRenderer _lr;
    private GrappleObject _grappleTarget;
    private Rigidbody _rb;
    private Coroutine _activeGrapple;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

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
            if (_grappleTarget != null) //Resets the ability mid-cast.
            {
                EndAbility(true);
            }
    }

    /// <summary>
    /// Gets an InteractableObject from PlayerInteract, then checks if it is Grapple
    /// </summary>
    /// <param name="interactedObect"></param>
    private void GetInteractedObject(InteractableObject interactedObect)
    {
        if (interactedObect.TryGetComponent<GrappleObject>(out GrappleObject objectToGrapple))
        {
            if (playerManager.state == CharacterState.Grapple)
            {
                _grappleTarget = objectToGrapple;
                SpawnCameraAnchor();
            }
        }
    }

    /// <summary>
    /// Is called by the grappling animation.
    /// </summary>
    private void StartGrapple()
    {
        _activeGrapple = StartCoroutine(GrappleTowards(_grappleTarget.transform, TIME_LIMIT));
    }

    /// <summary>
    /// Moves the character towards the target position for a timeLimit amount of seconds.
    /// </summary>
    /// <param name="targetPosition"></param>
    /// <param name="timeLimit"></param>
    /// <returns></returns>
    private IEnumerator GrappleTowards(Transform targetPosition, float timeLimit)
    {
        float timePassed = 0;
        while (timePassed < timeLimit)
        {
            timePassed += Time.fixedDeltaTime;
            _rb.MovePosition(Vector3.Lerp(transform.position, targetPosition.position, MOVE_SPEED * Time.fixedDeltaTime));
            yield return new WaitForFixedUpdate();
        }
        EndAbility(false);
    }

    /// <summary>
    /// Resets all variables.
    /// If the ability was not canceled, changes back the CharacterState.
    /// </summary>
    /// <param name="wasCanceled"></param>
    private void EndAbility(bool wasCanceled)
    {
        if (_activeGrapple != null) StopCoroutine(_activeGrapple);
        _activeGrapple = null;
        _grappleTarget = null;
        CameraMovement.Instance.ChangeCameraTarget(transform);
        _rb.linearVelocity = Vector3.zero;
        if (!wasCanceled)
        {
            playerManager.ChangeCharacterState();
        }
    }

    private void SpawnCameraAnchor()
    {
        Vector3 positionToSpawn = Vector3.Lerp(transform.position, _grappleTarget.transform.position, 0.5f);
        CameraMovement.Instance.SpawnCameraAnchor(positionToSpawn, "Grapple Camera Anchor");
    }
}
