using System;
using UnityEngine;

/// <summary>
/// Holds information and can change CharacterState.
/// </summary>
public class CharacterManager : MonoBehaviour
{
    public CharacterState state { get; private set; }
    public event Action<CharacterState> OnCharacterStateChanged;

    public float maxMoveSpeed { get; private set; } = 5;
    public float currentMoveSpeed { get; private set; } = 5;
    public float damage { get; private set; }
    [HideInInspector] public float magnitude;

    void Start()
    {
        ChangeCharacterState();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="newState"></param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public void ChangeCharacterState(CharacterState newState = CharacterState.Idle)
    {

        if (newState == state) return; //If same state- no need to transition
        currentMoveSpeed = maxMoveSpeed;

        switch (newState)
        {
            case CharacterState.Idle:

                break;
            case CharacterState.Run:

                break;
            case CharacterState.Attack:
                if (state != CharacterState.Idle && state != CharacterState.Run) return;
                currentMoveSpeed *= 0.2f;
                break;
            case CharacterState.Ability:

                break;
            case CharacterState.Dash:

                break;
            case CharacterState.Stunned:

                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
        state = newState;
        OnCharacterStateChanged?.Invoke(state);
    }
}
