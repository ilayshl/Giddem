using UnityEngine;

/// <summary>
/// All Arms parent script, specific arms inherit from this script.
/// </summary>
public abstract class Arm : MonoBehaviour
{
    [SerializeField] protected ArmData correspondingArm;

    protected abstract void OnEquip();
    //All visual logic goes here.
    protected abstract void Attack();
    //Logic for normal attack, includes the animation from the corresponding ArmData
    protected abstract void SpecialAttack();
    //Logic for special skill, abstract keyword in order to override in every inheritance

}
