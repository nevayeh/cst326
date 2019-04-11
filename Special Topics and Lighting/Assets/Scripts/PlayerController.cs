using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    // ------------------------------------
    //              PLAYER 
    // ------------------------------------

    public float speed;
    private Rigidbody rb;

    // ------------------------------------
    //          BALL SPAWNING
    // ------------------------------------

    public GameObject ball;
    public GameObject ballLight;
    public GameObject ballSpawnPoint;
    public GameObject lightSpawnPoint;

    private Vector3 lightDifference = new Vector3(0, 4, 0);
    private Quaternion lightRotation = Quaternion.Euler(new Vector3(90, 0, 0));


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        spawnBall();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
    }

    public void spawnBall()
    {
        Instantiate(ball, ballSpawnPoint.transform.position, Quaternion.identity);
        Instantiate(ballLight, lightSpawnPoint.transform.position, lightRotation);
    }

}