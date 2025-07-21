using System.Collections;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    [SerializeField] private float minFlickerTime = 0.05f; // Minimum delay between flickers
    [SerializeField] private float maxFlickerTime = 0.3f;  // Maximum delay between flickers
    [SerializeField] private bool flickerOnStart = false;


    private Light pointLight;

    private void Awake()
    {
        pointLight = GetComponent<Light>();
    }

    private void Start()
    {
        if (flickerOnStart)
        {
            StartCoroutine(FlickerRoutine());
        }
    }

    /// <summary>
    /// Coroutine to turn light on and off repeatedly with a random delay.
    /// </summary>
    IEnumerator FlickerRoutine()
    {
        while (true)
        {
            pointLight.enabled = !pointLight.enabled;
            float waitTime = Random.Range(minFlickerTime, maxFlickerTime);
            yield return new WaitForSeconds(waitTime);
        }
    }
}
