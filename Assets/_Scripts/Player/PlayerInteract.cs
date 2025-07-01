using System.Collections.Generic;
using Interfaces;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    List<IInteractable> interactablesInRange = new();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (interactablesInRange != null)
            {
                foreach (var interactable in interactablesInRange)
                {
                    interactable.OnInteract();
                }
            }
        }    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            if (other.TryGetComponent<IInteractable>(out IInteractable interactable))
            {
                interactablesInRange.Add(other.GetComponent<IInteractable>());
                interactable.OnRangeEnter();
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            if (other.TryGetComponent<IInteractable>(out IInteractable interactable))
            {
                if (interactablesInRange.Contains(interactable))
                {
                    interactablesInRange.Remove(interactable);
                    interactable.OnRangeExit();
                }
            }
        }
    }
}
