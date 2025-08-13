using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Holds the behaviour of the BloodSoakShader.
/// Use this ONLY with the corresponding shader to control its animation.
/// </summary>
public class ColorSoakShader : MonoBehaviour
{
    private const int END_VALUE = -1;
    private const int START_VALUE = 1;
    [SerializeField] private bool isActiveOnStart;
    [SerializeField] private int animDuration;
    private Image _image;

    void Awake()
    {
        _image = GetComponent<Image>();
    }

    /// <summary>
    /// Decides on a random seed and checks if the shader should immediately play.
    /// </summary>
    void Start()
    {
        float randomX = Random.Range(0f, 10f);
        float randomY = Random.Range(0f, 10f);
        _image.material.SetVector("_RandomnessSeed", new Vector2(randomX, randomY));
        if (isActiveOnStart)
        {
            _image.material.SetFloat("_SpreadPercent", START_VALUE);
            StartCoroutine(PlayAnimation(animDuration));
        }
    }

    /// <summary>
    /// Resets the values.
    /// </summary>
    void OnDestroy()
    {
        _image.material.SetFloat("_SpreadPercent", START_VALUE);
        _image.material.SetVector("_RandomnessSeed", Vector2.zero);
    }

    /// <summary>
    /// Gradually decreases _SpreadPercent to make the shader appear.
    /// </summary>
    /// <param name="playDuration"></param>
    /// <returns></returns>
    private IEnumerator PlayAnimation(float playDuration)
    {
        while (_image.material.GetFloat("_SpreadPercent") > END_VALUE)
        {
            float currentValue = _image.material.GetFloat("_SpreadPercent");
            _image.material.SetFloat("_SpreadPercent", currentValue - (Time.deltaTime / playDuration));
            yield return null;
        }
    }

}
