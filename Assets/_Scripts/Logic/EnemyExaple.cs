using UnityEngine;

public class EnemyExaple : MonoBehaviour, IDamageable, IKillable
{
    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public void TakeDamage(float amount)
    {
        anim.SetTrigger("isDamaged");
    }

    public void OnKill()
    {
        
    }
}
