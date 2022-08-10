using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class FighterEnemyController : MonoBehaviour
{

    // variable
    public float speed;
    public float attackSpeed;
    private float attackTimer;
    private Vector3 velocityOfEnemy;
    // end of variabless


    // components
    private Rigidbody enemyRb;
    public Transform playerTransform;
    //end of componenets


    // classes
    public PlayerController playerControllerScript;
    // end of classes

    // Start is called before the first frame update
    void Start()
    {

        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();

        enemyRb = GetComponent<Rigidbody>();

        attackTimer = attackSpeed;

    }

    // Update is called once per frame
    void Update()
    {


        if(attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {

        velocityOfEnemy = transform.forward * speed * Time.fixedDeltaTime;
        enemyRb.velocity = velocityOfEnemy;

        float angleBetween = 270 - Mathf.Atan2(transform.position.z - playerTransform.position.z, transform.position.x - playerTransform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0f, angleBetween, 0f));

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            if (attackTimer <= 0)
            {
                playerControllerScript.GetDamage(15);
                attackTimer = attackSpeed;
            }
        }
    }
}


