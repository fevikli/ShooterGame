using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class FighterEnemyController : MonoBehaviour
{

    // variable
    public float speed;
    public float attackSpeed;
    public int currentHealth;
    public int maxHealth;
    public bool isGameRunning;
    private float attackTimer;
    private Vector3 velocityOfEnemy;
    // end of variabless


    // components
    private Rigidbody enemyRb;
    public Transform playerTransform;
    //end of componenets


    // game objects
    public Slider EnemyHealthBarSlider;
    // end of game objects


    // classes
    public PlayerController playerControllerScript;
    // end of classes

    // Start is called before the first frame update
    void Start()
    {

        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();        
        enemyRb = GetComponent<Rigidbody>();
        isGameRunning = playerControllerScript.isGameRunning;
        attackTimer = attackSpeed;

        maxHealth = 100;
        currentHealth = maxHealth;
        SetMaxHealth(maxHealth);

    }

    // Update is called once per frame
    void Update()
    {

        isGameRunning = playerControllerScript.isGameRunning;

        if (isGameRunning)
        {

            SetHealth(currentHealth);

            //EnemyHealthBarSlider.value = 10;

            if (attackTimer > 0)
            {
                attackTimer -= Time.deltaTime;
            }
        }


    }

    private void FixedUpdate()
    {
        if (isGameRunning)
        {
            velocityOfEnemy = transform.forward * speed * Time.fixedDeltaTime;
            enemyRb.velocity = velocityOfEnemy;

            float angleBetween = 270 - Mathf.Atan2(transform.position.z - playerTransform.position.z, transform.position.x - playerTransform.position.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0f, angleBetween, 0f));
        }
        else
        {

            enemyRb.velocity = Vector3.zero;
            enemyRb.angularVelocity = Vector3.zero;

        }

    }

    private void OnCollisionStay(Collision collision)
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

    private void OnCollisionEnter(Collision collision)
    {
        
        if(collision.gameObject.CompareTag("Bullet"))
        {

            GetDamege(20);

        }
    }

    public void GetDamege(int damage)
    {

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);

            playerControllerScript.AddScore(1);
        }

    }


    public void SetMaxHealth(int health)
    {

        EnemyHealthBarSlider.maxValue = health;
        EnemyHealthBarSlider.value = health;

    }

    public void SetHealth(int health)
    {

        EnemyHealthBarSlider.value = health;

    }

}


