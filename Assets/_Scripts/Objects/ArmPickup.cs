using Interfaces;
using UnityEngine;

public class ArmPickup : MonoBehaviour, IInteractable
{
    [SerializeField] ArmData armData;
    [SerializeField] Material highlightMat;
    private MeshRenderer meshRenderer;

    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void Start()
    {
        Debug.Log("Arm is ready");
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

    public void OnInteract()
    {
        Debug.Log("interacted");
        Destroy(this.gameObject);
    }
}
