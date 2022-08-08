using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{

    // variables
    private Vector3 followDistance;
    private Vector3 followPosition;
    public float smoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;
    // end of variables


    // components
    public Transform target;
    // end of componenets


    // Start is called before the first frame update
    void Start()
    {

        followDistance = target.position - transform.position;

    }

    // Update is called once per frame
    private void LateUpdate()
    {

        followPosition = target.position - followDistance;

        transform.position = Vector3.SmoothDamp(transform.position, followPosition, ref velocity, smoothTime);

    }
}
