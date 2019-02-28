using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    private bool ballIsActive;
    private Vector3 ballPosition;
    private Vector2 ballInitialForce;
    private Vector2 ballResetForce;

    // GameObject
    public GameObject playerObject;

    // Use this for initialization
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();

        // create the force
        ballInitialForce = new Vector2(100.0f, 200.0f);

        ballResetForce = new Vector2(0.0f, 0.0f);

        // set to inactive
        ballIsActive = false;

        // ball position
        ballPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // check for user input
        if (Input.GetButtonDown("Jump") == true)
        {
            // check if is the first play
            if (!ballIsActive)
            {
                // set ball active
                ballIsActive = !ballIsActive;

                // add a force
                rigidbody2D.AddForce(ballInitialForce);
            }
        }

        if (!ballIsActive && playerObject != null)
        {
            // get and use the player position
            ballPosition.x = playerObject.transform.position.x;

            // apply player X position to the ball
            transform.position = ballPosition;
        }

        // Check if ball falls
        if (ballIsActive && transform.position.y < -6)
        {
            ballIsActive = !ballIsActive;
            ballPosition.x = playerObject.transform.position.x;
            ballPosition.y = -3.2f;
            transform.position = ballPosition;

            rigidbody2D.velocity = ballResetForce;

            playerObject.SendMessage("TakeLife");
        }

    }
}
