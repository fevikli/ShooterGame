using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // variables
    public float speed;
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

    }

    // Update is called once per frame
    void Update()
    {

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");


        // look toward mouse position
        Vector3 mouseWorldPosition = gameCam.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * 15f);
        float angleBetween = 270 - Mathf.Atan2(transform.position.z - mouseWorldPosition.z, transform.position.x - mouseWorldPosition.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0f, angleBetween, 0f));



        Shoot();

    }

    private void FixedUpdate()
    {

        velocityOfPlayer = new Vector3(horizontalInput * speed * Time.fixedDeltaTime, 0f, verticalInput * speed * Time.fixedDeltaTime);
        playerRb.velocity = velocityOfPlayer;

    }

    private void Shoot()
    {
        
            if (Input.GetMouseButton(0))
            {

                Instantiate(bulletPrefab, bulletSpanwPoint.position, transform.rotation);
                
            }

    }
}
