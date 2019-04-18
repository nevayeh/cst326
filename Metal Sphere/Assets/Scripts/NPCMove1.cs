using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMove1 : MonoBehaviour
{
    public GameObject alert;

    public Transform destination;
    public Transform patrolPoint1;
    public Transform patrolPoint2;
    public Transform agent;

    public NavMeshAgent navMeshAgent;

    public int spottingRange;
    //public int minDistance;

    public AudioSource source;
    public AudioClip clip1;
    public AudioClip clip2;
    public AudioClip clip3;

    private float TargetDistance;
    public float fieldOfViewDegrees = 120f;
    private double count;

    private bool notChase;
    private bool alerted;
    private bool chase;
    private bool once;
    private bool once2;
    private bool pursuit;

    public float spreadFactor = 0.02f;

    //Vector3 direction = transform.forward;

    //direction.x += Random.Range(-spreadFactor, spreadFactor);
    //direction.y += Random.Range(-spreadFactor, spreadFactor);
    //direction.z += Random.Range(-spreadFactor, spreadFactor);


    // Start is called before the first frame update
    void awake()
    {
        SetPatrol();
        once = false;
    }

    void Start()
    {
        alert.gameObject.SetActive(false);

        navMeshAgent = this.GetComponent<NavMeshAgent>();
        notChase = true;
        alerted = false;
        chase = false;
        once = false;
        once2 = false;
        pursuit = false;
        count = 0;

        //Sound/clips
        AudioSource[] audioSources = GetComponents<AudioSource>();
        source = audioSources[0];
        clip1 = audioSources[1].clip;
        clip2 = audioSources[0].clip;
        clip3 = audioSources[2].clip;

    }

    void Update()
    {
        /*if (Vector3.Distance(agent.position, destination.transform.position) > spottingRange + count)
        {
            //Debug.Log("Patrolling");
            SetPatrol();
            once = false;
        }
        else
        {
            //Debug.Log("Chasing");
            SetDestination();



        function CanSeePlayer() : boolean{*/
 
        RaycastHit hit;
        Vector3 rayDirection = destination.transform.position - transform.position;
 
        if ((Vector3.Angle(rayDirection, transform.forward)) <= fieldOfViewDegrees * 0.5f)
        {
          
            // Detect if player is within the field of view
            if (Physics.Raycast(transform.position, rayDirection, out hit))
            {

                TargetDistance = hit.distance;

                if ((TargetDistance <= spottingRange) && (hit.transform.CompareTag("Player")))
                    {
                        //if (pursuit == false)
                        //{
                            print("Chasing");
                            StartCoroutine(SetDestination());
                            //pursuit = true;
                        //}

                    }
            //Debug.Log(hit.transform.tag);
            //print(hit.transform.CompareTag("Player"));
            }
        }

        RaycastHit TheHit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out TheHit))
        {
            TargetDistance = TheHit.distance;

            if ((TargetDistance > spottingRange) || (TheHit.transform.tag != "Player"))
            {
                print("Patrolling");
                SetPatrol();
                once = false;

            }
        }

        /*RaycastHit TheHit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out TheHit))
        {
            TargetDistance = TheHit.distance;

            if ((TargetDistance <= spottingRange) && (TheHit.transform.tag == "Player"))
            {
                //Debug.Log(TheHit.transform.tag);
                StartCoroutine(SetDestination());
                pursuit = true;

            }
            else
            {
                //Debug.Log(TheHit.transform.tag);
                SetPatrol();
                once = false;

            }
        }*/
    }

    private IEnumerator SetDestination()
    {
        
        if (once == false)
        {
            alert.gameObject.SetActive(true);
            GetComponent<NavMeshAgent>().speed = 8F;
            fieldOfViewDegrees = 100f;
            spottingRange = 16;

            if (pursuit == false)
            {
                source.PlayOneShot(clip1);
                pursuit = true;
            }

            print("Target Spotted!");
            once = true;
            once2 = false;
        }

        Vector3 targetVector = destination.transform.position;
        navMeshAgent.SetDestination(targetVector);

        /*RaycastHit spot;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out spot))
        {
            if ((TargetDistance > spottingRange) && (spot.transform.tag != "Player"))
            {
                pursuit = false;
                spottingRange = 8;
                notChase = true;
            }
        }*/
        RaycastHit spot;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out spot))
        {
            if ((TargetDistance < spottingRange) && (spot.transform.tag == "Player"))
            {
                print("Target in sight!");
                targetVector = destination.transform.position;
                navMeshAgent.SetDestination(targetVector);
            }
            else
            {
                print("Lost target heading to their last known location...");

                if (once2 == false)
                {
                    GetComponent<NavMeshAgent>().speed = 12F;
                    spottingRange = 10;
                    targetVector = destination.transform.position;
                    navMeshAgent.SetDestination(targetVector);
                }

                yield return new WaitForSeconds(10);
                once2 = true;
                pursuit = false;
                spottingRange = 8;
                notChase = true;
                print("Target lost, heading back...");
                fieldOfViewDegrees = 120f;
                SetPatrol();
            }
        }

        yield return new WaitForSeconds(2);
        alert.gameObject.SetActive(false);
        pursuit = false;
    }

private void SetPatrol()
{
    GetComponent<NavMeshAgent>().speed = 3.5F;

    if (notChase == false)
    {
            //code to patrol
            if ((Vector3.Distance(agent.position, patrolPoint1.transform.position) < 0.25) && (Vector3.Distance(agent.position, patrolPoint2.transform.position) > 1))
            {

                navMeshAgent.SetDestination(patrolPoint2.transform.position);

            }
            else if ((Vector3.Distance(agent.position, patrolPoint2.transform.position) < 0.25) && (Vector3.Distance(agent.position, patrolPoint1.transform.position) > 1))
            {

                navMeshAgent.SetDestination(patrolPoint1.transform.position);

            }
        }
        else
        {
            //code to return back to patrolling after chasing
            if (Vector3.Distance(agent.position, patrolPoint1.transform.position) < Vector3.Distance(agent.position, patrolPoint2.transform.position))
            {

                Vector3 patrolVector = patrolPoint1.transform.position;
                navMeshAgent.SetDestination(patrolVector);
                notChase = false;

            }
            else
            {

                Vector3 patrolVector = patrolPoint2.transform.position;
                navMeshAgent.SetDestination(patrolVector);
                notChase = false;

            }
        }
    }
}
