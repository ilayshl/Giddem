using UnityEngine;

/// <summary>
///Holds a reference to Animator component and changes animations based on PlayerManager's state.
/// </summary>
public class PlayerAnimation : MonoBehaviour
{
    [HideInInspector] public Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
        PlayerManager.OnPlayerStateChanged += OnPlayerStateChanged;
    }

    void OnDestroy()
    {
        PlayerManager.OnPlayerStateChanged -= OnPlayerStateChanged;
    }

    void Update()
    {
        anim.SetFloat("moveSpeed", PlayerManager.Instance.magnitude);
    }

    private void OnPlayerStateChanged(PlayerState newState)
    {
        switch (newState)
        {
            case PlayerState.Idle:
                OnAttack(false);
                OnDash(false);
                break;
            case PlayerState.Run:
                OnAttack(false);
                break;
            case PlayerState.Attack:
                OnAttack(true);
                break;
            case PlayerState.Skill:
                Debug.LogWarning("No scripts for special attack!");
                break;
            case PlayerState.Dash:
                OnDash(true);
                break;
            case PlayerState.Inactive:

                break;
        }
        SetRunning(newState == PlayerState.Run);
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
