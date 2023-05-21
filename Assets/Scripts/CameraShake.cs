using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Jobs;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] float shakeMagnitude;
    [SerializeField] float shakeDuration;

    Vector3 initialPosition;
    void Start()
    {
        initialPosition = transform.position;
    }

    public void Play()
    {
        StartCoroutine(Shake());
    }

    IEnumerator ShakeOld()
    {
        int shakesN = 10;
        float timePerMove = shakeDuration / (float)shakesN;
        Debug.Log(timePerMove);
        for (int i = 0; i < shakesN; i++)
        {
            Vector2 targetPosition = initialPosition + (Vector3)Random.insideUnitCircle * shakeMagnitude;
            transform.position = targetPosition;
            yield return new WaitForSeconds(timePerMove);
        }
        transform.position = initialPosition;
    }

    IEnumerator Shake()
    {
        float timeShaken = 0f;
        while (timeShaken < shakeDuration)
        {
            transform.position = initialPosition + (Vector3)Random.insideUnitCircle * shakeMagnitude;
            timeShaken += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.position = initialPosition;
    }
}
