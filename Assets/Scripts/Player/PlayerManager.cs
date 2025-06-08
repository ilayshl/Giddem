using System;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    public PlayerState state { get; private set; }
    public static event Action<PlayerState> OnPlayerStateChanged;

    public float maxMoveSpeed { get; private set; }
    public float currentMoveSpeed { get; private set; }
    public float damage { get; private set; }

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
        ChangeGameState();
    }

    public void ChangeGameState(PlayerState newState = PlayerState.Idle)
    {
        state = newState;

        switch (newState)
        {
            case PlayerState.Idle:

                break;
            case PlayerState.Run:

                break;
            case PlayerState.Attack:

                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
        OnPlayerStateChanged?.Invoke(newState);

    }
}
