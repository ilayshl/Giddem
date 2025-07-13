using UnityEngine;

public class PlayerAttackColliders : MonoBehaviour
{
    [SerializeField] private Collider[] attackColliders;
    public int attackIndex { get; private set; } = 0;

    public void InitiateAttack()
    {
        Debug.Log($"Attack {attackIndex} is on");
        attackColliders[attackIndex].enabled = true;
    }

    public void StopAttack()
    {
        Debug.Log($"Attack {attackIndex} is off");
        attackColliders[attackIndex].enabled = false;
        IncrementAttackIndex();
    }

    public void ResetCurrentAttack()
    {
        Debug.Log("Reset!");
        attackIndex = 0;
    }

    private void IncrementAttackIndex()
    {
        attackIndex++;
        if (attackIndex > attackColliders.Length)
        {
            ResetCurrentAttack();
        }
    }
}
