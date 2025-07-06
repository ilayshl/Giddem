using System.Collections;
using UnityEngine;

/// <summary>
/// Checks input and holds logic for attacks (normal and skill).
/// </summary>
public class PlayerAttack : MonoBehaviour
{
    [SerializeField] CharacterManager playerManager;
    private const float ATTACK_COOLDOWN = 1f;
    private Coroutine attackCooldown;

    // Update is called once per frame
    private void Update()
    {
        CheckForClick();
    }

    private void CheckForClick()
    {
        if (Input.GetKey(KeyCode.Mouse0) && attackCooldown == null)
        {
            StartAttack();
        }
    }

    /// <summary>
    /// Runs the Coroutine for /time/ seconds.
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    private IEnumerator StartAttackCooldown(float time)
    {
        yield return new WaitForSeconds(time);
        attackCooldown = null;
    }

    /// <summary>
    /// If not already attacking, sets state to attack.
    /// </summary>
    private void StartAttack()
    {
        playerManager.ChangeCharacterState(CharacterState.Attack);
    }

    /// <summary>
    /// Starts the AttackCooldown coroutine and resets PlayerStates
    /// </summary>
    private void FinishAttack()
    {
        if (attackCooldown == null)
        {
            if (playerManager.state == CharacterState.Attack)
            {
                attackCooldown = StartCoroutine(StartAttackCooldown(ATTACK_COOLDOWN));
                playerManager.ChangeCharacterState();
            }
        }
    }

    /// <summary>
    /// Whenever an attack animation ends, it checks if left click is still held.
    /// TO BE USED BY ANIMATIONS
    /// </summary>
    private void CheckFinishAttack()
    {
        if (Input.GetKey(KeyCode.Mouse0) == false)
        {
            FinishAttack();
        }
    }

}
