using UnityEngine;

/// <summary>
/// Responsible for handling the animations of the player.
/// </summary>
public class AnimationManager : MonoBehaviour
{
    public Animator anim;
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
        _comboCount++;
        anim.SetInteger("comboCount", _comboCount);
        anim.SetBool("isAttacking", true);
        if (_comboCount == 3) { _comboCount = 0; }
    }

}
