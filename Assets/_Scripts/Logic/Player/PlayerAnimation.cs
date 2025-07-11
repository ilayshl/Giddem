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
                break;
            case CharacterState.Run:
                OnAttack(false);
                break;
            case CharacterState.Attack:
                OnAttack(true);
                break;
            case CharacterState.Telekinesis:
                Debug.Log($"[{this.name}] Telekinesis");
                break;
            case CharacterState.Grapple:
                Debug.Log($"[{this.name}] Grapple");
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

}
