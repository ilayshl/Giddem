using System;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    public void InitiateAttack()
    {
        objectsHit = new();
        SetAttackCollider(true);
    }

    public void ResetCurrentAttack()
    {
        attackIndex = 0;
    }

    private void IncrementAttackIndex()
    {
        attackIndex++;
        if (attackIndex > attackColliders.Length - 1)
        {
            ResetCurrentAttack();
        }
    }

    public void EndAttack()
    {
        SetAttackCollider(false);
        IncrementAttackIndex();
    }

    private void SetAttackCollider(bool value)
    {
        attackColliders[attackIndex].gameObject.SetActive(value);
    }

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
