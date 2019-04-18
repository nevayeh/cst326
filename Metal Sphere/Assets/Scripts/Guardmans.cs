using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guardmans : MonoBehaviour
{
    //facing neg x direction 
    private float TargetDistance;
    public int spottingRange;
    public GameObject target;
    private  bool alerted;
    private int count;

    // Start is called before the first frame update
    void Start()
    {
        alerted = false;
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit TheHit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out TheHit))
        {
            TargetDistance = TheHit.distance;
            //Debug.Log(TheHit.distance);
            //Debug.Log(TheHit.transform.tag);
            if ((alerted == false) && (TargetDistance <= spottingRange) && (TheHit.transform.tag == "Player"))
            {
                //Destroy(target.gameObject);
                //Debug.Log(TheHit.transform.tag);
                GetComponent<AudioSource>().Play();
                count += 1;
                alerted = true;
            }

            if((alerted == true) && (TargetDistance <= spottingRange + count) && (TheHit.transform.tag == "Player"))
            {
                //Debug.Log(spottingRange + count);
                //Debug.Log(TheHit.transform.tag);
                GetComponent<AudioSource>().Play();
                count += 1;
            }
        }
    }
}
