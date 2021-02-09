using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    Transform target;
    Vector3 originalPos;
    bool isShaking = false;

    void Start()
    {
        target = GetComponent<Transform>();
        originalPos = target.localPosition;
    }

    float shakeDuration = 0.5f;

    public void shake(float duration)
    {
        if(duration > 0)
        {
            shakeDuration += duration;
        }
    }

    void Update()
    {
        if(shakeDuration > 0 && !isShaking)
        {
            StartCoroutine(ScreenShake());
        }
    }

    IEnumerator ScreenShake()
    {
        var startTime = Time.realtimeSinceStartup;

        isShaking = true;
        while (Time.realtimeSinceStartup < startTime + shakeDuration)
        {
            var randomPoint = new Vector3(Random.Range(-0.085f, 0.085f), Random.Range(-0.085f, 0.085f), originalPos.z);
            target.localPosition = randomPoint;
            yield return null;
        }

        shakeDuration = 0f;
        target.localPosition = originalPos;
        isShaking = false;
    }




}
