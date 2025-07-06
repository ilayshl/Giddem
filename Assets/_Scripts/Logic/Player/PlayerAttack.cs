using System.Collections;
using UnityEngine;

/// <summary>
/// Checks input and holds logic for attacks (normal and skill).
/// </summary>
public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private CharacterManager playerManager;
    private const float ATTACK_COOLDOWN = 1f;
    private Coroutine _attackCooldown;

    private void Update()
    {
        CheckForClick();
    }

    private void CheckForClick()
    {
        if (Input.GetKey(KeyCode.Mouse0) && _attackCooldown == null)
        {
            StartAttack();
        }
    }

    /// <summary>
    /// Runs the Coroutine for /time/ seconds.
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    private IEnumerator Start_attackCooldown(float time)
    {
        yield return new WaitForSeconds(time);
        _attackCooldown = null;
    }

    /// <summary>
    /// If not already attacking, sets state to attack.
    /// </summary>
    private void StartAttack()
    {
        playerManager.ChangeCharacterState(CharacterState.Attack);
    }

    /// <summary>
    /// Starts the _attackCooldown coroutine and resets PlayerStates
    /// </summary>
    private void FinishAttack()
    {
        if (_attackCooldown == null)
        {
            if (playerManager.state == CharacterState.Attack)
            {
                _attackCooldown = StartCoroutine(Start_attackCooldown(ATTACK_COOLDOWN));
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

    /// <summary>
    /// Resets the cooldown coroutine, to be used in the dash animation
    /// </summary>
    private void ResetCooldown()
    {
        if (_attackCooldown != null)
        {
            StopCoroutine(_attackCooldown);
            _attackCooldown = null;
        }
    }

}
