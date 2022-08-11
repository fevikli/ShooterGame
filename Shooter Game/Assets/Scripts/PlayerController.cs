using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{

    // variables
    public float speed;
    public float shootRate;
    public float currentHealth;
    public float maxHealth;
    public bool isGameRunning;
    private float shootCooldown;
    private float verticalInput;
    private float horizontalInput;
    private Vector3 velocityOfPlayer;
    // end of variables


    // components
    private Rigidbody playerRb;
    public Transform bulletSpanwPoint;
    // end of components

    // game objects
    private Camera gameCam;
    public GameObject bulletPrefab;
    // end of game objects

    // claseses

    // end of classes


    // Start is called before the first frame update
    void Start()
    {


        playerRb = GetComponent<Rigidbody>();

        gameCam = Camera.main;

        currentHealth = maxHealth;

        isGameRunning = true;



    }

    // Update is called once per frame
    void Update()
    {

        if (isGameRunning)
        {

            LookTowardMouse();


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
        }
        else
        {
            playerRb.velocity = Vector3.zero;
            playerRb.angularVelocity = Vector3.zero;
        }

    }

    private void Shoot()
    {
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

    public void GetDamage(float damage)
    {

        currentHealth -= damage;
        Debug.Log("Health : " + currentHealth);

        if(currentHealth <= 0)
        {
            isGameRunning = false;
            playerRb.velocity = Vector3.zero;
            playerRb.angularVelocity = Vector3.zero;

            Debug.Log("Game Over");
        }

    }

}

