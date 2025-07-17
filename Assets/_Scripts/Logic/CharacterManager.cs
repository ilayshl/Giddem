using System;
using UnityEngine;

/// <summary>
/// Holds information and can change CharacterState.
/// </summary>
public class CharacterManager : MonoBehaviour
{
    public CharacterState state { get; private set; }
    public event Action<CharacterState> OnCharacterStateChanged;

    public float MaxMoveSpeed { get; private set; } = 5;
    public float CurrentMoveSpeed { get; private set; } = 5;
    public float TurnSpeed { get; private set; } = 720;
    public float Damage { get; private set; } = 5;

    [HideInInspector] public float magnitude;

    void Start()
    {
        ChangeCharacterState();
    }

    /// <summary>
    /// Respoinsible for the current state of the character and pings all subscribed methods.
    /// </summary>
    /// <param name="newState"></param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public void ChangeCharacterState(CharacterState newState = CharacterState.Idle)
    {

        if (newState == state) return; //If same state- no need to transition
        CurrentMoveSpeed = MaxMoveSpeed;

        switch (newState)
        {
            case CharacterState.Idle:

                break;
            case CharacterState.Run:

                break;
            case CharacterState.Attack:
                if (state != CharacterState.Idle && state != CharacterState.Run) return;
                CurrentMoveSpeed *= 0.2f;
                break;
            case CharacterState.Telekinesis:

                break;
            case CharacterState.Grapple:

                break;
            case CharacterState.Dash:

                break;
            case CharacterState.Stunned:

                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);

            case CharacterState.Dialog:
                break;
        }
        state = newState;
        OnCharacterStateChanged?.Invoke(state);
    }
}
