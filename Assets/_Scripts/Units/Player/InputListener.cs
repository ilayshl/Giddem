using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Unused attempt for an InputListener.
/// THIS SCRIPT IS UNUSED- IT IS HERE FOR FURTHER DEVELOPMENT
/// </summary>
public class InputListener : MonoBehaviour
{
    [SerializeField] CharacterManager playerManager;
    [SerializeField] private KeyCode escapeInput, attackInput, dashInput, abilityInput, interactInput;
    HashSet<KeyCode> inputs = new();

    void Awake()
    {
        inputs.Add(escapeInput);
        inputs.Add(attackInput);
        inputs.Add(dashInput);
        inputs.Add(abilityInput);
        inputs.Add(interactInput);

    }

    // Update is called once per frame
    void Update()
    {
        foreach (var input in inputs)
        {
            if (Input.GetKeyDown(input))
            {

            }
        }
    }
}
