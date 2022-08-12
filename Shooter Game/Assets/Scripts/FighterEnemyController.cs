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
    public bool[] probabilityArray;
    public int persantageInput;
    private float attackTimer;
    private Vector3 velocityOfEnemy;
    // end of variabless


    // components
    private Rigidbody enemyRb;
    public Transform playerTransform;
    public Animator enemyAnimator;
    //end of componenets


    // game objects
    public Slider EnemyHealthBarSlider;
    public GameObject healUpPrefab;
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

        enemyAnimator = GetComponent<Animator>();


    }

    // Update is called once per frame
    void Update()
    {

        isGameRunning = playerControllerScript.isGameRunning;

        if (isGameRunning)
        {

            SetHealth(currentHealth);


            if (attackTimer > 0)
            {
                attackTimer -= Time.deltaTime;
            }
        }
        else
        {

            enemyAnimator.SetBool("Victory", true);

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

                enemyAnimator.SetTrigger("Punch");

                playerControllerScript.GetDamage(15);
                attackTimer = attackSpeed;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Bullet"))
        {

            GetDamege(20);

        }
    }

    public void GetDamege(int damage)
    {

        currentHealth -= damage;

        if (currentHealth <= 0)
        {

            if (Probability(persantageInput))
            {

                Vector3 spawnPos = new Vector3(transform.position.x, 0.8f, transform.position.z);

                Instantiate(healUpPrefab, spawnPos, healUpPrefab.transform.rotation);

            }

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



    // this method returns 40% "true" value for heal ups spawn probability
    public bool Probability(int persantage)
    {

        probabilityArray = new bool[10];
        int loop = persantage / 10;

        for (int i = 0; i < loop; i++)
        {

            probabilityArray[i] = true;

        }

        for (int i = loop; i < probabilityArray.Length; i++)
        {

            probabilityArray[i] = false;

        }


        //bool[] probabilityArray = { false, false, false, false, false, false, true, true, true, true };

        int arrayIndex = Random.Range(0, probabilityArray.Length);
        bool randomSatate = probabilityArray[arrayIndex];

        return randomSatate;
    }

}


