using UnityEngine;

/// <summary>
/// Interactable objects will be here
/// </summary>
public interface IInteractable
{
    public void OnInteract();
    public void OnRangeEnter();
    public void OnRangeExit();
    }
