using UnityEngine;

/// <summary>
/// Holds information about each arm. *MAKE SURE IS FULLY PREPARED BEFORE RUNTIME*
/// </summary>
[CreateAssetMenu(fileName = "New Arm Data", menuName = "Arm data")]
public class ArmData : ScriptableObject
{
    [Header("General Information")]
    [SerializeField] private string armName;
    public string ArmName { get => armName; }
    [SerializeField] private Arm correspondingArm;
    public Arm CorrespondingArm { get => correspondingArm; }
    [SerializeField] Animation armAnimation;
    public Animation ArmAnimation { get => armAnimation; }
    [SerializeField] private SkinnedMeshRenderer armModel;
    public SkinnedMeshRenderer ArmModel { get => armModel; }
    [SerializeField] private ParticleSystem[] armVFX;
    public ParticleSystem[] ArmVFX { get => armVFX; }


    [Header("Special Skill Information")]
    [SerializeField] private string skillName;
    public string SkillName { get => skillName; }
    [SerializeField] private Animation skillAnimation;
    public Animation SkillAnimation { get => skillAnimation; }
    [SerializeField] private ParticleSystem skillVFX;
    public ParticleSystem SkillVFX { get => skillVFX; }
    [SerializeField] private AudioClip[] skillSFX;
    public AudioClip[] SkillSFX { get => skillSFX; }

}
