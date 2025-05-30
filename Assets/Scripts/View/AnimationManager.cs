using UnityEngine;

/// <summary>
/// Responsible for handling the animations of the player.
/// </summary>
public class AnimationManager : MonoBehaviour
{
    [HideInInspector] public Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void SetRunning(bool value)
    {
        anim.SetBool("isRunning", value);
    }

    public void OnAttack()
    {
        SetRunning(false);
        anim.SetBool("isAttacking", true);
    }

    public void StopAttack()
    {
        anim.SetBool("isAttacking", false);
    }

}
