using UnityEngine;
using UnityEngine.SceneManagement;

public class RedBall : MonoBehaviour
{
    #region Variables
    [SerializeField] private GameObject particule;
    [SerializeField] private float forceMultiplicator;
    [SerializeField] private GameObject cam;
    [SerializeField] private Camera staticCam;
    [SerializeField] private TrajectoryPreview trajectoryPreview;

    private bool _isShoot;
    private Coroutine _shakeCoroutine;
    private Rigidbody _rb;
    private Scene _currentScene;
    private Vector3 _trajectoryStartPos;
    #endregion


    #region Built-in Methods
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _currentScene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isShoot == false)
        {
            if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
            {
                _trajectoryStartPos = transform.position;
                forceMultiplicator += 0.001f;
                forceMultiplicator = Mathf.Clamp(forceMultiplicator, 0f, 0.3f);

                _shakeCoroutine = StartCoroutine(cam.GetComponent<Cam>().CameraShake(() => forceMultiplicator));

                Vector3 velocity = transform.up * forceMultiplicator * 50;
                trajectoryPreview.ShowTrajectory(_trajectoryStartPos, velocity);
            }

            if (Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0))
            {
                if (_shakeCoroutine != null)
                {
                    StopCoroutine(_shakeCoroutine);
                    cam.GetComponent<Cam>().ShakeOffset = Vector3.zero;
                }
                
                _rb.AddForce(transform.up * forceMultiplicator, ForceMode.Impulse);
                forceMultiplicator = 0;
                _isShoot = true;
                trajectoryPreview.HideTrajectory();
            }

            Ray ray = staticCam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Vector3 direction = hit.point - transform.position;

                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Damage")
        {
            SceneManager.LoadScene(_currentScene.name);
        }
    }
    #endregion
}
