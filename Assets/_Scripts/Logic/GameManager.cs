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
            Destroy(Instance);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        ChangeGameState(GameState.Menu);
    }

    public void ChangeGameState(GameState newState = GameState.Active)
    {
        state = newState;

        switch (newState)
        {
            case GameState.Active:
                Debug.Log("Game is active.");
                break;
            case GameState.Pause:
                Debug.Log("Game is paused.");
                break;
            case GameState.Menu:
                Debug.Log("Game is in main menu.");
                break;
            case GameState.Inventory:
                Debug.Log("Game is in inventory.");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
        OnGameStateChanged?.Invoke(newState);

    }
}
