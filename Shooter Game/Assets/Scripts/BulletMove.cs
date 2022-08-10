using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{

    // variables
    public float speed;
    // end of variables


    // components
    public Transform playerTransform;
    private Rigidbody bulletRb;
    // end of componenets


    // game objects

    // end of game objecs


    // Start is called before the first frame update
    void Start()
    {

        bulletRb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        

    }

    private void FixedUpdate()
    {
        bulletRb.velocity = transform.forward * speed * Time.fixedDeltaTime;
        bulletRb.angularVelocity = Vector3.zero;

    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if(collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }

    }
}
