using System.Collections;
using UnityEngine;

public class Cam : MonoBehaviour
{
    #region Variables
    [SerializeField] private Transform ballTransform;

    private Vector3 _shakeOffset = Vector3.zero;
    private float _initialZ;
    private Vector3 _targetPosition;
    #endregion

    #region Properties
    public Vector3 ShakeOffset 
    {  
        get { return _shakeOffset; } 
        set { _shakeOffset = value; } 
    }
    #endregion

    #region Built-in Methods
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _initialZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        _targetPosition = ballTransform.position;
        _targetPosition.z = _initialZ;
    }

    private void LateUpdate()
    {
        transform.position = _targetPosition + _shakeOffset;
    }
    #endregion

    public IEnumerator CameraShake(System.Func<float> getMagnitude)
    {
        Vector3 originalPos = transform.position;

        while (true)
        {
            float magnitude = getMagnitude() * 0.1f;
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            _shakeOffset = new Vector3(x, y, 0f);

            yield return null;
        }
    }
}
