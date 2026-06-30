using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Bumper : MonoBehaviour
{
    #region Variables
    private AudioSource _audioSource;

    private Vector3 _shakeOffset = Vector3.zero;
    #endregion

    #region Built-in Methods
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _audioSource.Play();
            gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
            StartCoroutine(SquatchEffect(0.2f));
        }
    }
    #endregion

    #region Kawaii
    /// <summary>
    /// Coroutine to squash bumper
    /// </summary>
    /// <param name="duration"></param>
    /// <returns></returns>
    IEnumerator SquatchEffect(float duration)
    {
        float timeElapsed = 0;

        Vector3 startScale = transform.localScale;
        Vector3 squashedScale = new Vector3(startScale.x * 1.2f, startScale.y * 0.8f, startScale.z);

        while (timeElapsed < duration)
        {

            float normalizedTime = timeElapsed / duration;

            transform.localScale = Vector3.Lerp(squashedScale, startScale, normalizedTime);

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        transform.localScale = startScale;
    }
    #endregion
}
