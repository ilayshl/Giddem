using UnityEngine;

public class FireLightFlicker : MonoBehaviour
{
    [SerializeField] private float maxInterval = 1f;
    [SerializeField] private float minIntensity = 0.5f;
    [SerializeField] private float maxIntensity = 1f;
    [SerializeField] private float maxDisplacement = 0.25f;

    private float _targetIntensity;
    private float _interval;
    private float _timer;
    private Vector3 _origin;
    private Vector3 _targetPosition;

    private Light _myLight;

    private void Awake()
    {
        _myLight = GetComponent<Light>();
    }
    void Start()
    {
        _origin = transform.position;
        _targetIntensity = Random.Range(minIntensity, maxIntensity);
        _interval = Random.Range(0, maxInterval);
        _targetPosition = _origin;
    }

    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > _interval)
        {
            _targetIntensity = Random.Range(minIntensity, maxIntensity);
            _targetPosition = _origin + Random.insideUnitSphere * maxDisplacement;
            _timer = 0;
            _interval = Random.Range(0, maxInterval);
        }

        _myLight.intensity = Mathf.Lerp(_myLight.intensity, _targetIntensity, _timer / _interval);
        transform.position = Vector3.Lerp(transform.position, _targetPosition, _timer / _interval);
    }
}
