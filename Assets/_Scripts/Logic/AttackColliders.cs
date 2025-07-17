using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the logic for the attack colliders.
/// Start with InitiateAttack and end with EndAttack.
/// </summary>
public class AttackColliders : MonoBehaviour
{
    public int attackIndex { get; private set; } = 0;
    public Action<IDamageable> OnTargetHit;
    [SerializeField] private HitCollider[] attackColliders;
    private HashSet<IDamageable> objectsHit = new();

    private void OnEnable()
    {
        foreach (var collider in attackColliders)
        {
            collider.OnTriggerCollision += OnTargetCollision;
        }
    }

    private void OnDisable()
    {
        foreach (var collider in attackColliders)
        {
            collider.OnTriggerCollision -= OnTargetCollision;
        }
    }

    /// <summary>
    /// For when the attack collider starts, resets the already-hit targets.
    /// </summary>
    public void InitiateAttack()
    {
        objectsHit = new();
        SetAttackColliderActive(true);
    }

    /// <summary>
    /// Resets the combo.
    /// </summary>
    public void ResetCurrentAttack()
    {
        attackIndex = 0;
    }

    /// <summary>
    /// Continues to the next attack in the combo, if the end was reached, resets the counter.
    /// </summary>
    private void IncrementAttackIndex()
    {
        attackIndex++;
        if (attackIndex > attackColliders.Length - 1)
        {
            ResetCurrentAttack();
        }
    }

    /// <summary>
    /// For when the collider ends, and increments the counter.
    /// </summary>
    public void EndAttack()
    {
        SetAttackColliderActive(false);
        IncrementAttackIndex();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    private void SetAttackColliderActive(bool value)
    {
        attackColliders[attackIndex].gameObject.SetActive(value);
    }

    /// <summary>
    /// When a target is hit through HitCollider, notify whoever's subscribed to the Action.
    /// Also adds the IDamageable to the HashSet of already-damaged target so it won't be damaged again.
    /// </summary>
    /// <param name="other"></param>
    private void OnTargetCollision(Collider other)
    {
        if (other.TryGetComponent<IDamageable>(out IDamageable target))
        {
            if (!objectsHit.Contains(target))
            {
                objectsHit.Add(target);
                OnTargetHit?.Invoke(target);
            }
        }
    }

    public bool IsLastHit()
    {
        return attackIndex == attackColliders.Length - 1;
    }
}
