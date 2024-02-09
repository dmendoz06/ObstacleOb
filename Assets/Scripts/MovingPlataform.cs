using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlataform : MonoBehaviour
{
    [SerializeField]
    private Waypoint waypointPath;

    [SerializeField]
    private float speed;

    private int TargetWaypointIndex;

    private Transform PreviousWaypoint;
    private Transform TargetWaypoint;

    private float TimeToWaypoint;
    private float ElapseTime;

    void Start()
    {
        TargetNextWaypoint();
    }

    private void FixedUpdate()
    {
        ElapseTime += Time.deltaTime;

        float elapsedPercentage = ElapseTime / TimeToWaypoint;
        elapsedPercentage = Mathf.SmoothStep(0, 1, elapsedPercentage);
        transform.position = Vector3.Lerp(PreviousWaypoint.position, TargetWaypoint.position, elapsedPercentage);

        if (elapsedPercentage >=1 ) 
        {
            TargetNextWaypoint();
        }


    }

    private void TargetNextWaypoint()
    {
        PreviousWaypoint = waypointPath.Getwaypoint(TargetWaypointIndex);
        TargetWaypointIndex = waypointPath.GetNextWaypoint(TargetWaypointIndex);
        TargetWaypoint = waypointPath.Getwaypoint(TargetWaypointIndex);

        ElapseTime = 0;

        float distanceToWaypoint = Vector3.Distance(PreviousWaypoint.position, TargetWaypoint.position);
        TimeToWaypoint = distanceToWaypoint / speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.transform.parent.SetParent(transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        other.transform.SetParent(null);
    }

}
