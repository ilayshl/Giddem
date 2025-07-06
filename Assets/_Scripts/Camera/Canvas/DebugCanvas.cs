using TMPro;
using UnityEngine;

public class DebugCanvas : MonoBehaviour
{
    [SerializeField] private Canvas debugCanvas;
    [SerializeField] private TextMeshProUGUI stateText;
    [SerializeField] private CharacterManager playerManager;
    void Awake()
    {
        playerManager.OnCharacterStateChanged += OnPlayerStateChanged;
    }

    void OnDestroy()
    {
        playerManager.OnCharacterStateChanged -= OnPlayerStateChanged;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            debugCanvas.gameObject.SetActive(!debugCanvas.isActiveAndEnabled);
        }
    }

    private void OnPlayerStateChanged(CharacterState state)
    {
        stateText.text = state.ToString();
    }
}
