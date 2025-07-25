using Unity.VisualScripting;
using UnityEngine;

/// <summary>
///Holds a reference to Animator component and changes animations based on PlayerManager's state.
/// </summary>
public class PlayerAnimation : MonoBehaviour
{
    [HideInInspector] private Animator anim;
    [SerializeField] private CharacterManager playerManager;

    void Awake()
    {
        anim = GetComponent<Animator>();
        playerManager.OnCharacterStateChanged += OnPlayerStateChanged;
    }

    void OnDestroy()
    {
        playerManager.OnCharacterStateChanged -= OnPlayerStateChanged;
    }

    void Update()
    {
        anim.SetFloat("moveSpeed", playerManager.magnitude);
    }

    private void OnPlayerStateChanged(CharacterState newState)
    {
        switch (newState)
        {
            case CharacterState.Idle:
                OnAttack(false);
                OnDash(false);
                OnTelekinesis(false);
                OnGrapple(false);
                break;
            case CharacterState.Run:
                OnAttack(false);
                break;
            case CharacterState.Attack:
                OnAttack(true);
                break;
            case CharacterState.Telekinesis:
                OnTelekinesis(true);
                break;
            case CharacterState.Grapple:
                OnGrapple(true);
                break;
            case CharacterState.Dash:
                OnDash(true);
                OnTelekinesis(false);
                break;
            case CharacterState.Stunned:

                break;
        }
        SetRunning(newState == CharacterState.Run);
    }

    private void SetRunning(bool value)
    {
        anim.SetBool("isRunning", value);
    }

    private void OnAttack(bool value)
    {
        anim.SetBool("isAttacking", value);
    }

    private void OnDash(bool value)
    {
        anim.SetBool("isDashing", value);
    }

    private void OnGrapple(bool value)
    {
        anim.SetBool("isGrapple", value);
    }

    private void OnTelekinesis(bool value)
    {
        anim.SetBool("isTelekinesis", value);
    }
}
