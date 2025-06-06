using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState state { get; private set; }
    public static event Action<GameState> OnGameStateChanged;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = null;
    }

    void Start()
    {
        ChangeGameState();
    }

    public void ChangeGameState(GameState newState = GameState.Active)
    {
        state = newState;

        switch (newState)
        {
            case GameState.Active:

                break;
            case GameState.Pause:

                break;
            case GameState.Menu:

                break;
            case GameState.Inventory:

                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
        OnGameStateChanged?.Invoke(newState);

    }
}
