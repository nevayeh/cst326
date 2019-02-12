using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class YouWin : MonoBehaviour {
    public Text youWinText;
    public Camera mainCamera;

	// Use this for initialization
	void Start () {
        youWinText.text = "";
	}

    private void OnTriggerEnter(Collider other)
    {
        youWinText.text = "You Win";
    }

    private void Update()
    {
        RaycastHit hit;
        Vector3 origin = new Vector3(-10, -10, 0);
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(origin, mainCamera.transform.right,  out hit))
        {
            youWinText.text = "You Lose";
        }
    }
}
