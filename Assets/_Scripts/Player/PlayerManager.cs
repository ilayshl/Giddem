using System;
using UnityEngine;

/// <summary>
/// Static singleton that holds information and can change PlayerState.
/// </summary>
public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    public PlayerState state { get; private set; }
    public static event Action<PlayerState> OnPlayerStateChanged;

    public float maxMoveSpeed { get; private set; } = 5;
    public float currentMoveSpeed { get; private set; } = 5;
    public float damage { get; private set; }
    [HideInInspector] public float magnitude;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            Debug.Log("Destroyed " + gameObject.name);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        ChangePlayerState();
    }

    public void ChangePlayerState(PlayerState newState = PlayerState.Idle)
    {
        state = newState;

        switch (newState)
        {
            case PlayerState.Idle:
                currentMoveSpeed = maxMoveSpeed;

                break;
            case PlayerState.Run:

                break;
            case PlayerState.Attack:
                currentMoveSpeed *= 0.2f;
                break;
            case PlayerState.Skill:

                break;
            case PlayerState.Dash:
                currentMoveSpeed = maxMoveSpeed;
                break;
            case PlayerState.Inactive:

                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
        OnPlayerStateChanged?.Invoke(newState);
    }
}
