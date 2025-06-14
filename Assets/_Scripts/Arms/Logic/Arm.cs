using UnityEngine;

/// <summary>
/// All Arms parent script, specific arms inherit from this script.
/// </summary>
public abstract class Arm : MonoBehaviour
{
    [SerializeField] ArmData correspondingArm;

    protected abstract void ArmAttack();
    //Logic for normal attack, includes the animation from the corresponding ArmData
    protected abstract void ArmSpecialAttack();
    //Logic for special skill, abstract keyword in order to override in every inheritance

}
