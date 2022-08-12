using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{

    // variables
    public float speed;
    public float shootRate;
    public int currentHealth;
    public int maxHealth;
    public bool isGameRunning;
    public int score;
    public int highScore;
    private float shootCooldown;
    private float verticalInput;
    private float horizontalInput;
    private Vector3 velocityOfPlayer;
    // end of variables


    // components
    private Rigidbody playerRb;
    public Transform bulletSpanwPoint;
    public Animator playerAnimator;
    // end of components

    // game objects
    private Camera gameCam;
    public GameObject bulletPrefab;
    public GameObject gameOverPanel;
    // end of game objects

    // claseses
    public UIManager UIManagerScript;
    // end of classes


    // Start is called before the first frame update
    void Start()
    {


        playerRb = GetComponent<Rigidbody>();

        gameCam = Camera.main;

        maxHealth = 100;
        currentHealth = maxHealth;
        UIManagerScript.SetMaxHealth(maxHealth);

        isGameRunning = true;

        gameOverPanel.SetActive(false);

        playerAnimator = GetComponent<Animator>();

        score = 0;
        highScore = PlayerPrefs.GetInt("HighScore", 0);



    }

    // Update is called once per frame
    void Update()
    {

        if (isGameRunning)
        {

            LookTowardMouse();

            UIManagerScript.SetHealth(currentHealth);

            if (shootCooldown > 0f)
            {
                shootCooldown -= Time.deltaTime;
            }

            if (Input.GetMouseButton(0))
            {

                if (shootCooldown <= 0)
                {
                    Shoot();
                }

            }

            if (currentHealth <= 0)
            {
                isGameRunning = false;
                playerRb.velocity = Vector3.zero;
                playerRb.angularVelocity = Vector3.zero;

                playerAnimator.SetBool("isDead",true);

                gameOverPanel.SetActive(true);

                Debug.Log("Game Over");
            }

        }

    }

    private void FixedUpdate()
    {

        if (isGameRunning)
        {
            velocityOfPlayer = new Vector3(horizontalInput * speed * Time.fixedDeltaTime, 0f, verticalInput * speed * Time.fixedDeltaTime);
            playerRb.velocity = velocityOfPlayer;

            playerAnimator.SetFloat("Speed", playerRb.velocity.magnitude);

        }
        else
        {
            playerRb.velocity = Vector3.zero;
            playerRb.angularVelocity = Vector3.zero;
        }

    }

    private void Shoot()
    {

        playerAnimator.SetTrigger("Shoot");

        shootCooldown = shootRate;

        Instantiate(bulletPrefab, bulletSpanwPoint.position, transform.rotation);

    }

    void LookTowardMouse()
    {

        // get input frm player
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");


        // look toward mouse position
        Vector3 mouseWorldPosition = gameCam.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * 15f);
        float angleBetween = 270 - Mathf.Atan2(transform.position.z - mouseWorldPosition.z, transform.position.x - mouseWorldPosition.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0f, angleBetween, 0f));

    }

    public void GetDamage(int damage)
    {

        playerAnimator.SetTrigger("GetDamage");

        currentHealth -= damage;
        Debug.Log("Health : " + currentHealth);

        

        if (currentHealth <= 0)
        {
            isGameRunning = false;
            playerRb.velocity = Vector3.zero;
            playerRb.angularVelocity = Vector3.zero;

            playerAnimator.SetBool("isDead", true);

            gameOverPanel.SetActive(true);

            Debug.Log("Game Over");
        }

    }

    public void AddScore(int scoreAmount)
    {

        score += scoreAmount;
        Debug.Log("score added");
        if(score > highScore)
        {

            PlayerPrefs.SetInt("HighScore", score);

        }

    }


}

