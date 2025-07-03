using Interfaces;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    [SerializeField] protected string interactableName;
    [SerializeField] protected Material highlightMat;
    protected MeshRenderer meshRenderer;

    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void OnRangeEnter()
    {
        Material[] materials = meshRenderer.materials;
        Material[] newMaterials = new Material[materials.Length + 1];
        materials.CopyTo(newMaterials, 0);
        newMaterials[newMaterials.Length - 1] = highlightMat;
        meshRenderer.materials = newMaterials;
    }

    public void OnRangeExit()
    {
        Material[] materials = meshRenderer.materials;
        Material[] newMaterials = new Material[materials.Length - 1];
        newMaterials[0] = materials[0];
        meshRenderer.materials = newMaterials;
    }

    public abstract void OnInteract();
}
