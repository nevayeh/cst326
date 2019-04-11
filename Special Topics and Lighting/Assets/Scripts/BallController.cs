using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private PlayerController playerController;

    // ------------------------------------
    //              PARTICLES
    // ------------------------------------

    public GameObject particles;
    private Vector3 particlesDifference = new Vector3(0, -1, 0);
    private Quaternion particleRotation = Quaternion.Euler(new Vector3(-90, 0, 0));

    void Start()
    {
        GameObject playerObject = GameObject.Find("Player");        
        if(playerObject != null)
        {
            playerController = playerObject.GetComponent<PlayerController>();
        }
        if(playerController == null)
        {
            Debug.Log("Cannot find 'PlayerController' script");
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Finish")
        {
            Destroy(gameObject);
            spawnParticles();
            playerController.spawnBall();            
        }
    }

    private void spawnParticles()
    {
        Instantiate(particles, transform.position + particlesDifference, particleRotation);
    }
}
