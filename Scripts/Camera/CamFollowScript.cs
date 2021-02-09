using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowScript : MonoBehaviour {

    public Transform target;

    public float smooth = 0.2f;

    public Vector3 offset;


    void FixedUpdate()
    {
        Vector3 RawPosition = target.position + offset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, RawPosition, smooth * Time.deltaTime);
        transform.position = smoothPosition;
        
    }
}
