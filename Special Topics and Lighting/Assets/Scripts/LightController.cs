using UnityEngine;
using System.Collections;

public class LightController : MonoBehaviour
{
    private GameObject ball;
    private Vector3 offset;

    void Start()
    {
        ball = GameObject.Find("Soccer Ball(Clone)");
        offset = transform.position - ball.transform.position;
    }

    void LateUpdate()
    {
        if (ball != null)
        {
            transform.position = ball.transform.position + offset;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Finish")
        {
            Destroy(gameObject);
        }
    }
}