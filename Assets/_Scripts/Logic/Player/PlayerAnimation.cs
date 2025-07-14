using Unity.VisualScripting;
using UnityEngine;

/// <summary>
///Holds a reference to Animator component and changes animations based on PlayerManager's state.
/// </summary>
public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private CharacterManager playerManager;
    private Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        playerManager.OnCharacterStateChanged += OnPlayerStateChanged;        
    }

    private void OnDisable()
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
                OnGrapple();
                break;
            case CharacterState.Dash:
                OnDash(true);
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

    private void OnGrapple()
    {
        anim.SetTrigger("grapple");
    }

    private void OnTelekinesis(bool value)
    {
        anim.SetBool("isTelekinesis", value);
    }
}
