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
    [SerializeField] private Animator armAnimator;
    public Animator ArmAnimator { get => armAnimator; }
    [SerializeField] private SkinnedMeshRenderer armModel;
    public SkinnedMeshRenderer ArmModel { get => armModel; }
    [SerializeField] private ParticleSystem[] armVFX;
    public ParticleSystem[] ArmVFX { get => armVFX; }
    [SerializeField] private Collider[] attackColliders;
    public Collider[] AttackColliders { get => attackColliders; } 


    [Header("Special Skill Information")]
    [SerializeField] private string skillName;
    public string SkillName { get => skillName; }
    [SerializeField] private ParticleSystem skillVFX;
    public ParticleSystem SkillVFX { get => skillVFX; }
    [SerializeField] private AudioClip[] skillSFX;
    public AudioClip[] SkillSFX { get => skillSFX; }
    [SerializeField] private Collider[] skillColliders;
    public Collider[] SkillColliders { get => SkillColliders; } 

    public override string ToString()
    {
        string output = $"Arm name: {ArmName} \nSpecial skill name: {skillName}";
        return output;
    }
}
