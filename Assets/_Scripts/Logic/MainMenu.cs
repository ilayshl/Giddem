using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Holds the behaviour of the main menu canvases.
/// </summary>
public class MainMenu : MonoBehaviour
{
    [SerializeField] private Transform mainCamera;
    [SerializeField] private Transform secondScreenCameraPosition;
    [SerializeField] private Button backButton;
    [SerializeField] private Canvas settingsCanvas, aboutCanvas;
    [SerializeField] private float animationTime;
    private int _sceneToTransition;
    private Vector3 _firstScreenCameraPosition;

    void Start()
    {
        _firstScreenCameraPosition = mainCamera.position;
    }

    private void DisableCanvases()
    {
        settingsCanvas.gameObject.SetActive(false);
        aboutCanvas.gameObject.SetActive(false);
    }

    public void ShowSettingsCanvas()
    {
        DisableCanvases();
        settingsCanvas.gameObject.SetActive(true);
        MoveToSecondScreen();
    }

    public void ShowAboutCanvas()
    {
        DisableCanvases();
        aboutCanvas.gameObject.SetActive(true);
        MoveToSecondScreen();
    }

    private void MoveToSecondScreen()
    {
        mainCamera.DOMoveX(secondScreenCameraPosition.position.x, animationTime).SetEase(Ease.InOutSine);
        Invoke(nameof(ChangeBackButton), animationTime);
    }

    private void ChangeBackButton()
    {
        bool isActive = backButton.isActiveAndEnabled;
        backButton.gameObject.SetActive(!isActive);
    }

    public void MoveToFirstScreen()
    {
        mainCamera.DOMoveX(_firstScreenCameraPosition.x, animationTime).SetEase(Ease.InOutSine);
        ChangeBackButton();
    }


    public void QuitApplication()
    {
        Application.Quit();
    }

}
