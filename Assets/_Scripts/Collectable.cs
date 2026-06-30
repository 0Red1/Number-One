using System.Collections;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    #region Variables
    private GameManager _gm;
    #endregion

    #region Built-in Methods
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _gm = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Collect();
        }
    }
    #endregion

    #region Custom Methods
    private void Collect()
    {
        _gm.CurrentCollectable++;
        GetComponent<SphereCollider>().enabled = false;
        StartCoroutine(AnimCollectable());
    }
    #endregion

    #region Kawaii
    /// <summary>
    /// Coroutine to rotate, translate and scale collectable
    /// </summary>
    /// <returns></returns>
    IEnumerator AnimCollectable()
    {
        float duration = 0.4f;
        float timeElasped = 0;
        float rotationSpeed = 720;
        float height = 0.1f;
        float scale = 0.2f;

        Vector3 startPos = transform.position;

        while (timeElasped < duration)
        {
            float normalizedTime = timeElasped / duration;
            float smoothTime = Mathf.SmoothStep(0, 1, normalizedTime);

            float heightOffset = Mathf.Sin(smoothTime * Mathf.PI) * height;

            transform.position = startPos + new Vector3(0, heightOffset, 0);

            float rotationStep = rotationSpeed * Time.deltaTime;
            transform.Rotate(0, rotationStep, 0);

            float scaleOffset = 1 + Mathf.Sin(smoothTime * Mathf.PI) * scale;
            transform.localScale = new Vector3(scaleOffset, scaleOffset, transform.localScale.z);

            timeElasped += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }
    #endregion
}
