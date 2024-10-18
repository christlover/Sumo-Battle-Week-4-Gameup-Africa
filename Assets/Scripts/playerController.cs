using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float speed = 5.0f;
    private Rigidbody playerRb;

    private GameObject focalPoint;
    public bool hasPowerup;

    private float powerUpStrength = 15f;

    public GameObject powerupIndicator;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        //Move Player with "W" and "A" keys
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);

        //Make Power up Indicator Follow Player
        powerupIndicator.transform.position = transform.position + new Vector3 (0, -0.5f, 0);
    }

    //If Player Picks Up a Powerup Then Change Boolean to True and Destroy Power up 
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Power Up")) {

            hasPowerup = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerUpCountdownRoutine());

            //Show Powerup Indicator When Gotten
            powerupIndicator.gameObject.SetActive(true);
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup) {

            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayfromPlayer = (collision.gameObject.transform.position - playerRb.transform.position);

            //Tell us in Console That We Hit Enemy After We Got Powerup
            Debug.Log("Collided With " + collision.gameObject.name + " with Powerup set to " + hasPowerup);

            //Knock enemy away with more Force
            enemyRigidbody.AddForce(awayfromPlayer * powerUpStrength, ForceMode.Impulse);
        }
    }
    //Countdown 7 Seconds after Getting Powerup and Then Deactivate Powerup
    IEnumerator PowerUpCountdownRoutine() { 
        yield return new WaitForSeconds(7);
        hasPowerup = false;

        //Hide Powerup Indicator When Expired
        powerupIndicator.gameObject.SetActive(false);
    }
}
