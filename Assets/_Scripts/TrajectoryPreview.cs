using UnityEngine;

public class TrajectoryPreview : MonoBehaviour
{
    #region Variables
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private int stepCount = 30;
    [SerializeField] private float stepTime = 0.05f;
    [SerializeField] private LayerMask reboundLayers;
    #endregion

    #region Built-in Methods
    private void Awake()
    {
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion

    #region Custom Methods
    public void ShowTrajectory(Vector3 startPos, Vector3 velocity)
    {
        lineRenderer.positionCount = stepCount;
        lineRenderer.SetPosition(0, startPos);

        Vector3 currentPos = startPos;
        Vector3 currentDir = velocity.normalized;
        float remainingLength = velocity.magnitude * stepTime * stepCount;

        int index = 1;

        for (int bounce = 0; bounce < 5 && index < stepCount; bounce++)
        {
            if (Physics.Raycast(currentPos, currentDir, out RaycastHit hit, remainingLength, reboundLayers))
            {
                currentPos = hit.point;
                currentDir = Vector3.Reflect(currentDir, hit.normal);
                lineRenderer.SetPosition(index, hit.point);
                index++;

                remainingLength -= hit.distance;
            }
            else
            {
                currentPos += currentDir * remainingLength;
                lineRenderer.SetPosition(index, currentPos);
                break;
            }
        }

        lineRenderer.positionCount = index + 1;
    }

    public void HideTrajectory()
    {
        lineRenderer.positionCount = 0;
    }
    #endregion
}
