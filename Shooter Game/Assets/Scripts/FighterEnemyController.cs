using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterEnemyController : MonoBehaviour
{

    // variable
    
    public float speed;
    private Vector3 velocityOfEnemy;
    // end of variabless


    // components
    private Rigidbody enemyRb;
    public Transform playerTransform;
    //end of componenets


    // Start is called before the first frame update
    void Start()
    {

        enemyRb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {

        velocityOfEnemy = transform.forward * speed * Time.fixedDeltaTime;
        enemyRb.velocity = velocityOfEnemy;

        float angleBetween = 270 - Mathf.Atan2(transform.position.z - playerTransform.position.z, transform.position.x - playerTransform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0f, angleBetween, 0f));

    }

}
