using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Transform mainCamera;
    [SerializeField] Transform secondScreenCameraPosition;
    [SerializeField] Button backButton;
    [SerializeField] Canvas settingsCanvas, aboutCanvas;
    private Vector3 firstScreenCameraPosition;

    void OnEnable()
    {
        FadeUI.Instance.OnFadeInFinish += TransitionScenes;
    }
    void Start()
    {
        firstScreenCameraPosition = mainCamera.position;
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
        mainCamera.DOMoveX(secondScreenCameraPosition.position.x, 0.5f).SetEase(Ease.InOutSine);
        Invoke(nameof(ShowBackButton), 0.5f);
    }

    private void ShowBackButton()
    {
        backButton.gameObject.SetActive(true);
    }

    public void MoveToFirstScreen()
    {
        mainCamera.DOMoveX(firstScreenCameraPosition.x, 0.5f).SetEase(Ease.InOutSine);
        backButton.gameObject.SetActive(false);
    }

    /// <summary>
    /// 1 = New game, 2 = Sandbox
    /// </summary>
    /// <param name="scene"></param>
    public void LoadScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

    private void TransitionScenes()
    {

    }
}
