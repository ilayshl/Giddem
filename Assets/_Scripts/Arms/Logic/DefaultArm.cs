using UnityEngine;

/// <summary>
/// The default arm that is automatically equipped (for now).
/// </summary>
public class DefaultArm : Arm
{
    protected override void OnEquip()
    {
        Debug.Log(correspondingArm.ToString());
        //Apply new animator
        //Apply new skinned mesh renderer
    }

    protected override void Attack()
    {
        
    }

    protected override void SpecialAttack()
    {

    }



}
