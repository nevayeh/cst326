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
    private double count;

    private bool notChase;
    private bool alerted;
    private bool chase;
    private bool once;
    private bool once2;
    private bool pursuit;

    // Start is called before the first frame update
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
        }*/
        RaycastHit TheHit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out TheHit))
        {
            TargetDistance = TheHit.distance;

            if ((TargetDistance <= spottingRange) && (TheHit.transform.tag == "Player") || pursuit == true)
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
        }
    }

    private IEnumerator SetDestination()
    {
        
        if (once == false)
        {
            alert.gameObject.SetActive(true);
            GetComponent<NavMeshAgent>().speed = 8F;
            spottingRange = spottingRange * 2;
            print("Spotting Range OC" + spottingRange);
            source.PlayOneShot(clip1);
            print("Target Spotted!");
            once = true;
            once2 = false;
        }

        Vector3 targetVector = destination.transform.position;
        navMeshAgent.SetDestination(targetVector);
        yield return new WaitForSeconds(2);
        alert.gameObject.SetActive(false);

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

        if (Vector3.Distance(agent.position, destination.transform.position) < spottingRange * 2)
        {
            print("Maintaining sight on target!");
            print("Spotting Range VD" + spottingRange);
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

            yield return new WaitForSeconds(15);
            once2 = true;
            pursuit = false;
            spottingRange = 8;
            notChase = true;
        }

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
