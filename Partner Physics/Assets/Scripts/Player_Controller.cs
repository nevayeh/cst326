using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour {
    private Rigidbody rb;
    public Camera cameraRefrence;

    public float speed;
    public float jumpHeight;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate () {
		float moveHorizontal = Input.GetAxis("Horizontal");

        Vector3 movement = new Vector3(moveHorizontal, 0, 0);

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(cameraRefrence.transform.up * jumpHeight, ForceMode.Impulse);
        }

        rb.AddForce(movement * speed);
    }
}
