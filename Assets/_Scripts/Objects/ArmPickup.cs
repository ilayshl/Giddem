using UnityEngine;

public class ArmPickup : MonoBehaviour, IInteractable
{
    [SerializeField] ArmData armData;
    private HighlightHandler highlightHandler;

    void Awake()
    {
        highlightHandler = GetComponent<HighlightHandler>();
    }

    public void OnInteract()
    {
        Debug.Log("interacted");
        Destroy(this.gameObject);
    }

    public void OnRangeEnter()
    {
        highlightHandler?.OnRangeEnter();
    }

    public void OnRangeExit()
    {
        highlightHandler?.OnRangeExit();
    }
    
    
}
