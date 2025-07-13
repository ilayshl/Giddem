using UnityEngine;

/// <summary>
/// Parent class for all interactables in the game.
/// </summary>
public abstract class InteractableObject : MonoBehaviour
{
    public string ObjectName { get => objectName; }
    public InteractableObjectType ObjectType { get => objectType; }
    [SerializeField] private string objectName;
    [SerializeField] private Material highlightMat;
    [SerializeField] private InteractableObjectType objectType;
    private MeshRenderer meshRenderer;

    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public abstract void OnInteract(CharacterManager character);
}
