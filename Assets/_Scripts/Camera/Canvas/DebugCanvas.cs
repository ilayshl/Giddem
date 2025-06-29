using TMPro;
using UnityEngine;

public class DebugCanvas : MonoBehaviour
{
    [SerializeField] private Canvas debugCanvas;
    [SerializeField] private TextMeshProUGUI stateText;
    void Awake()
    {
        PlayerManager.OnPlayerStateChanged += OnPlayerStateChanged;
    }

    void OnDestroy()
    {
        PlayerManager.OnPlayerStateChanged -= OnPlayerStateChanged;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            debugCanvas.gameObject.SetActive(!debugCanvas.isActiveAndEnabled);
        }
    }

    private void OnPlayerStateChanged(PlayerState state)
    {
        stateText.text = state.ToString();
    }
}
