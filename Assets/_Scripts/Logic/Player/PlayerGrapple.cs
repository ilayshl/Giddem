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
    private bool _isGrounded;
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
            if (_activeGrapple != null)
            {
                StopCoroutine(_activeGrapple);
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
            }
        }
    }

    /* private IEnumerator DrawLine()
    {

        yield return new WaitForEndOfFrame();
    } */

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
        if (_activeGrapple != null)
        {
            playerManager.ChangeCharacterState();
            _rb.linearVelocity = Vector3.zero;
        }
    }

}
