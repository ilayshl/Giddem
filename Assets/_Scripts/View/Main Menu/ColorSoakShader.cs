using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ColorSoakShader : MonoBehaviour
{
    private const int FULLY_COVERED = -1;
    private const int FULLY_VISIBLE = 1;
    [SerializeField] private bool isActiveOnStart;
    [SerializeField] private int animDuration;
    private Image _image;

    void Awake()
    {
        _image = GetComponent<Image>();
    }

    void Start()
    {
        float randomX = Random.Range(0f, 10f);
        float randomY = Random.Range(0f, 10f);
        _image.material.SetVector("_RandomnessSeed", new Vector2(randomX, randomY));
        if (isActiveOnStart)
        {
            _image.material.SetFloat("_SpreadPercent", FULLY_VISIBLE);
            StartCoroutine(PlayAnimation(animDuration));
        }
    }

    private IEnumerator PlayAnimation(float playDuration)
    {
        while (_image.material.GetFloat("_SpreadPercent") > FULLY_COVERED)
        {
            float currentValue = _image.material.GetFloat("_SpreadPercent");
            _image.material.SetFloat("_SpreadPercent", currentValue - (Time.deltaTime / playDuration));
            yield return null;
        }
    }

}
