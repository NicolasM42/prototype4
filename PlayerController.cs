using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float powerUpStr = 15.0f;
    public bool hasPowerup;
    public bool hasFirePower;
    public bool hasSmashPower;
    public float rocketCooldown = 0;

    private Rigidbody playerRb;
    private GameObject focalPoint;
    public GameObject platform;
    public GameObject powerUpIndicator;
    public GameObject rocketPrefab;

    private Vector3 indOffset = new Vector3(0, 0.1f, 0);

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("FocalPoint");

    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * forwardInput * speed);
        powerUpIndicator.transform.position = transform.position + indOffset;

        if (hasFirePower && rocketCooldown <= 0)
        {
            rocketCooldown = 0.5f;
            FireRocket();
        }
        else if (hasFirePower)
        {
            rocketCooldown -= Time.deltaTime;
        }

        if (hasSmashPower && Input.GetKeyDown(KeyCode.Space))
        {
            playerRb.AddForce( new Vector3(0, 10, 0), ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("powerup"))
        {
            hasPowerup = true;
            powerUpIndicator.gameObject.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerUpCountdownRoutine());
        }
        else if (other.CompareTag("Rocket"))
        {
            hasFirePower = true;
            powerUpIndicator.gameObject.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerUpCountdownRoutine());
        }
        else if (other.CompareTag("Smash"))
        {
            hasSmashPower = true;
            powerUpIndicator.gameObject.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerUpCountdownRoutine());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.CompareTag("enemy") || collision.gameObject.CompareTag("Alpha")) && hasPowerup)
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;

            enemyRb.AddForce(awayFromPlayer * powerUpStr, ForceMode.Impulse);
        }
    }

    private void FireRocket()
    {
        GameObject r = Instantiate(rocketPrefab, transform.position + focalPoint.transform.forward, rocketPrefab.transform.rotation);
        Rigidbody rb = r.GetComponent<Rigidbody>();
        rb.AddForce(focalPoint.transform.forward * 50, ForceMode.Impulse);
        r.transform.Rotate(90, 0, 0);
    }

    IEnumerator PowerUpCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        hasFirePower = false;
        hasSmashPower = false;
        powerUpIndicator.gameObject.SetActive(false);
    }
}
