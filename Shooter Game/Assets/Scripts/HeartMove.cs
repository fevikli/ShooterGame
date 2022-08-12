using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartMove : MonoBehaviour
{

    // veriables
    public float rotationalSpeed;
    private int currentHealth;
    // end of variables

    // components
    public Rigidbody heartRb;
    // end of components


    // clasesses
    public PlayerController playerControllerScript;
    public UIManager UIManagerScript;
    //end of classes

    // Start is called before the first frame update
    void Start()
    {

        heartRb = GetComponent<Rigidbody>();

        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        UIManagerScript = GameObject.Find("UI Manager").GetComponent<UIManager>();

    }


    private void Update()
    {

        currentHealth = playerControllerScript.currentHealth;

    }

    private void FixedUpdate()
    {

        heartRb.angularVelocity = Vector3.forward * rotationalSpeed * Time.fixedDeltaTime;

    }

    private void OnTriggerEnter(Collider other)
    {

       
        if (other.gameObject.CompareTag("Player"))
        {

            if (currentHealth < 100)
            {

                playerControllerScript.AddHealth(10);

            }

            Destroy(gameObject);
        }

    }
}
