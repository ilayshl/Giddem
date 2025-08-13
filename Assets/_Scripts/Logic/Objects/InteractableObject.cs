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
    protected InteractableObjectType objectType;
    private MeshRenderer meshRenderer;

    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        ObjectTypeInitiate(); //Declare each inheriting script's type
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

    /// <summary>
    /// Each inheriting script should declare what happens when the object is interacted with.
    /// </summary>
    public abstract void OnInteract();

    /// <summary>
    /// Each inheriting script should declare its own type.
    /// </summary>
    protected abstract void ObjectTypeInitiate();
}
