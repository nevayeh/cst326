using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.AI;

public class NPCMove : MonoBehaviour
{
    public Transform target;
    public Transform point1;
    public Transform point2;
    private bool chase;
    public int minDistance;
    UnityEngine.AI.NavMeshAgent agent;
    //NavMeshAgent somethingElse;

    // Start is called before the first frame update
    void Start()
    {
        //somethingElse = this.GetComponent<NavMeshAgent>();
        agent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
        chase = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, target.transform.position) < minDistance)
        {
            SetDestination();
        }
        else
        {
            SetPatrol();
        }
        
    }

    private void SetDestination()
    {
        if(target != null)
        {
            Vector3 targetVector = target.transform.position;
            //somethingElse.SetDestination(targetVector);
            agent.SetDestination(targetVector);
            chase = true;
        }
    }

    private void SetPatrol()
    {
        if (chase == false)
        {
            if (Vector3.Distance(transform.position, point1.transform.position) < 0.5 && Vector3.Distance(transform.position, point2.transform.position) > 1.0)
            {
                Vector3 patrolVector = point2.transform.position;
                agent.SetDestination(patrolVector);
            }
            else if (Vector3.Distance(transform.position, point1.transform.position) > 1 && Vector3.Distance(transform.position, point2.transform.position) < 0.5)
            {
                Vector3 patrolVector = point1.transform.position;
                agent.SetDestination(patrolVector);
            }
        }
        else
        {
            if(Vector3.Distance(transform.position, point1.transform.position) < Vector3.Distance(transform.position, point2.transform.position))
            {
                Vector3 patrolVector = point2.transform.position;
                agent.SetDestination(patrolVector);
                chase = false;
            }
            else
            {
                Vector3 patrolVector = point1.transform.position;
                agent.SetDestination(patrolVector);
                chase = false;
            }
        }
    }
}
