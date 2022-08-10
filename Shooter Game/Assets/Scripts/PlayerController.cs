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
        private float shootCooldown;
        private float verticalInput;
        private float horizontalInput;
        private Vector3 velocityOfPlayer;
        private Vector3 bulletSpawnOffset;
        // end of variables


        // components
        private Rigidbody playerRb;
        public Transform bulletSpanwPoint;
        // end of components

        // game objects
        private Camera gameCam;
        public GameObject bulletPrefab;
        // end of game objects


        // Start is called before the first frame update
        void Start()
        {

            playerRb = GetComponent<Rigidbody>();

            gameCam = Camera.main;

            bulletSpawnOffset = new Vector3(0, 2, 1.5f);

            currentHealth = maxHealth;



        }

        // Update is called once per frame
        void Update()
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

        }

        private void FixedUpdate()
        {

            velocityOfPlayer = new Vector3(horizontalInput * speed * Time.fixedDeltaTime, 0f, verticalInput * speed * Time.fixedDeltaTime);
            playerRb.velocity = velocityOfPlayer;

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

        }

    }

