using UnityEngine;

/// <summary>
/// Parent class for all interactables in the game.
/// </summary>
public abstract class InteractableObject : MonoBehaviour
{
    [SerializeField] protected Material highlightMat;
    protected MeshRenderer meshRenderer;

    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    /// <summary>
    /// When this object gets in range of whoever checks for it.
    /// </summary>
    public void OnRangeEnter()
    {
        Material[] materials = meshRenderer.materials;
        Material[] newMaterials = new Material[materials.Length + 1];
        materials.CopyTo(newMaterials, 0);
        newMaterials[newMaterials.Length - 1] = highlightMat;
        meshRenderer.materials = newMaterials;
    }

    /// <summary>
    /// When this object gets out of range of whoever checks for it.
    /// </summary>
    public void OnRangeExit()
    {
        Material[] materials = meshRenderer.materials;
        Material[] newMaterials = new Material[materials.Length - 1];
        newMaterials[0] = materials[0];
        meshRenderer.materials = newMaterials;
    }

    public abstract void OnInteract();
}
