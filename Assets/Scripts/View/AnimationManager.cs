using UnityEngine;

/// <summary>
/// Responsible for handling the animations of the player.
/// </summary>
public class AnimationManager : MonoBehaviour
{
    [HideInInspector] public Animator anim;
    private int _comboCount = 0;

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
        _comboCount = 0;
        anim.SetInteger("comboCount", _comboCount);
        anim.SetBool("isAttacking", false);
    }

}
