using UnityEngine;

/// <summary>
/// The default arm that is automatically equipped (for now).
/// </summary>
public class DefaultArm : Arm
{
    protected override void OnEquip()
    {
        Debug.Log(correspondingArm.ToString());
    }

    protected override void Attack()
    {

    }

    protected override void SpecialAttack()
    {

    }



}
