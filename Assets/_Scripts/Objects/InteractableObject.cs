using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    [SerializeField] protected Material highlightMat;
    protected MeshRenderer meshRenderer;

    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Material[] materials = meshRenderer.materials;
            Material[] newMaterials = new Material[materials.Length + 1];
            materials.CopyTo(newMaterials, 0);
            newMaterials[newMaterials.Length - 1] = highlightMat;
            meshRenderer.materials = newMaterials;
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Material[] materials = meshRenderer.materials;
            Material[] newMaterials = new Material[materials.Length - 1];
            newMaterials[0] = materials[0];
            meshRenderer.materials = newMaterials;
        }
    }

    protected abstract void OnInteract();
}
