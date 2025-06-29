using TMPro;
using UnityEngine;

public class DebugCanvas : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI stateText;
    void Awake()
    {
        PlayerManager.OnPlayerStateChanged += OnPlayerStateChanged;
    }

    void OnDestroy()
    {
        PlayerManager.OnPlayerStateChanged -= OnPlayerStateChanged;
    }

    private void OnPlayerStateChanged(PlayerState state)
    {
        stateText.text = state.ToString();
    }
}
