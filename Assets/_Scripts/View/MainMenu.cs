using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Transform mainCamera;
    [SerializeField] Transform secondScreenCameraPosition;
    [SerializeField] Button backButton;
    private Vector3 firstScreenCameraPosition;

    void Start()
    {
        firstScreenCameraPosition = mainCamera.position;
    }

    public void MoveToSecondScreen()
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
}
