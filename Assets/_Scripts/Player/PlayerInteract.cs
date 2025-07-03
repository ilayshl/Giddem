using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private List<InteractableObject> interactablesInRange = new(); //Every object in range

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            foreach (var interactableInRange in interactablesInRange)
            {
                interactableInRange.OnInteract();
            }
            if (interactablesInRange != null)
            {
            interactablesInRange = new();
            }
        }
    }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Interactable"))
            {
                if (other.TryGetComponent<InteractableObject>(out InteractableObject interactable))
                {
                    interactablesInRange.Add(interactable);
                    interactable.OnRangeEnter();
                }

            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Interactable"))
            {
                if (other.TryGetComponent<InteractableObject>(out InteractableObject interactable))
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
