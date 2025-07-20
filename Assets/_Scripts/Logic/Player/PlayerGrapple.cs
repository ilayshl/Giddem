using System.Collections;
using UnityEngine;

public class PlayerGrapple : MonoBehaviour
{
    private const float MOVE_SPEED = 3;
    private const float TIME_LIMIT = 0.5f;
    [SerializeField] CharacterManager playerManager;
    [SerializeField] PlayerInteract abilityInteract;
    [SerializeField] private LayerMask groundLayer;
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
        if (state == CharacterState.Grapple)
        {
            
        }
        else
        {
            if (_grappleTarget != null)
            {
                EndAbility(true);
            }
        }
    }

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

    private void StartGrapple()
    {
        _activeGrapple = StartCoroutine(GrappleTowards(_grappleTarget.transform, TIME_LIMIT));
    }

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

    private void EndAbility(bool wasCanceled)
    {
        if(_activeGrapple != null) StopCoroutine(_activeGrapple);
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
