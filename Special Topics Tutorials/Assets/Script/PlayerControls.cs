using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public float speed;
    public GameObject camera;
    private Rigidbody rb;

    public Transform player;
    public float turnSpeed = 4.0f;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        offset = new Vector3(player.position.x, player.position.y + 3.0f, player.position.z - 8.0f);
    }
        
    void FixedUpdate()
    {
        Vector3 cameraPlayerDifference = transform.position - camera.transform.position;
        cameraPlayerDifference.y = 0;
        cameraPlayerDifference.Normalize();

        float moveHorizontal = Input.GetAxis("Horizontal");
        float jump = Input.GetAxis("Jump");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = (cameraPlayerDifference * moveVertical + camera.transform.right * moveHorizontal) * speed;
        Vector3 up = new Vector3(0.0f, jump, 0.0f);

        rb.AddForce(movement * speed);
        if(Input.GetKeyDown("space"))
        {
            rb.AddForce(up * 600);
        }
    }
}
