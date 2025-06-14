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
        anim.SetFloat("moveSpeed", PlayerManager.Instance.input);
    }

    private void OnPlayerStateChanged(PlayerState newState)
    {
        switch (newState)
        {
            case PlayerState.Idle:
                StopAttack();
                break;
            case PlayerState.Run:
                StopAttack();
                break;
            case PlayerState.Attack:
                OnAttack();
                break;
            case PlayerState.Skill:
                Debug.LogWarning("No scripts for special attack!");
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

    private void OnAttack()
    {
        SetRunning(false);
        anim.SetBool("isAttacking", true);
    }

    private void StopAttack()
    {
        anim.SetBool("isAttacking", false);
    }

}
