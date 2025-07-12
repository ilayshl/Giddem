using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Checks input and holds logic for attacks (normal and skill).
/// </summary>
public class PlayerAttack : MonoBehaviour
{
    private const float ATTACK_COOLDOWN = 1f;
    [SerializeField] private CharacterManager playerManager;
    [SerializeField] private Collider[] attackColliders;
    private int _currentAttackIndex;
    private Coroutine _attackCooldown;

    private Vector3 _attackRotation;
    private LayerMask _playerLayer;

    private void OnEnable()
    {
        playerManager.OnCharacterStateChanged += GetActiveState;
    }

    private void OnDisable()
    {
        playerManager.OnCharacterStateChanged -= GetActiveState;
    }

    private void Update()
    {
        CheckForClick();
    }

    private void CheckForClick()
    {
        if (Input.GetKey(KeyCode.Mouse0) && _attackCooldown == null)
        {
            playerManager.ChangeCharacterState(CharacterState.Attack);
            LookAtCursor();
        }
    }

    /// <summary>
    /// Sets the last cursor position as the rotation target for the player when attacking. This happens once when attacking.
    /// </summary>
    /// <param name="state"></param>
    private void GetActiveState(CharacterState state)
    {
        if (state == CharacterState.Attack)
        {
            _attackRotation = CalculateAttackRotation();
        }
        else if(_currentAttackIndex != 0)
        {
            ResetAttack();
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
        _attackCooldown = null;
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
                _attackCooldown = StartCoroutine(StartAttackCooldown(ATTACK_COOLDOWN));
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

        IncrementAttack();
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

    /// <summary>
    /// Returns the Vector3 position of the mouse in the isometric world.
    /// </summary>
    /// <returns></returns>
    private Vector3 CalculateAttackRotation()
    {
        Vector3 mouse = Input.mousePosition;
        Ray castPoint = Camera.main.ScreenPointToRay(mouse);
        RaycastHit hit;
        if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, ~_playerLayer))
        {
            Vector3 requiredHitPoint;
            Vector3 playerHeight = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            Vector3 hitPoint = new Vector3(hit.point.x, hit.point.y, hit.point.z);
            float length = Vector3.Distance(playerHeight, hitPoint);
            var degree = 30;
            var radian = degree * Mathf.Deg2Rad;
            float hypote = length / (Mathf.Sin(radian));
            float distanceFromCamera = hit.distance;

            if (transform.position.y > hit.point.y)
            {
                requiredHitPoint = castPoint.GetPoint(distanceFromCamera - hypote);
            }
            else if (transform.position.y < hit.point.y)
            {
                requiredHitPoint = castPoint.GetPoint(distanceFromCamera + hypote);
            }
            else
            {
                requiredHitPoint = castPoint.GetPoint(distanceFromCamera);
            }
            return requiredHitPoint - transform.position;
        }
        return transform.forward;
    }

    /// <summary>
    /// Rotates to the cursor's world position via _attackRotation. To be used in Look
    /// </summary>
    private void LookAtCursor()
    {
        var rotation = Quaternion.LookRotation(_attackRotation, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, playerManager.TurnSpeed * Time.deltaTime * 2);
    }

    /// <summary>
    /// Will be on the attack's animations.
    /// </summary>
    private void OnAttackAnimationStart()
    {
        EnableCollider();
    }

    private void EnableCollider()
    {
        attackColliders[_currentAttackIndex].enabled = true;
    }

    private void DisableCollider()
    {
        attackColliders[_currentAttackIndex].enabled = false;
    }

    private void ResetAttack()
    {
        DisableCollider();
        ResetCurrentAttack();
    }

    private void IncrementAttack()
    {
        _currentAttackIndex++;
        Debug.Log("current attack is now " + _currentAttackIndex);
        if (_currentAttackIndex > attackColliders.Length)
        {
            Debug.Log("current attack > " + attackColliders.Length);
            ResetCurrentAttack();
        }
    }

    private void ResetCurrentAttack()
    {
        Debug.Log("Reset!");
        _currentAttackIndex = 0;
    }

}
