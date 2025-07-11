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

    /// <summary>
    /// For when this object gets in range of whoever checks for it.
    /// </summary>
    public void ShowOutline()
    {
        Material[] materials = meshRenderer.materials;
        Material[] newMaterials = new Material[materials.Length + 1];
        materials.CopyTo(newMaterials, 0);
        newMaterials[newMaterials.Length - 1] = highlightMat;
        meshRenderer.materials = newMaterials;
    }

    /// <summary>
    /// For when this object gets out of range of whoever checks for it.
    /// </summary>
    public void RemoveOutline()
    {
        Material[] materials = meshRenderer.materials;
        Material[] newMaterials = new Material[materials.Length - 1];
        newMaterials[0] = materials[0];
        meshRenderer.materials = newMaterials;
    }

    public abstract void OnInteract();
}
