using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleWaypointNavigator : MonoBehaviour
{

    VehicleController controller;
    public Waypoint currentWaypoint;

    int direction;

    private void Awake()
    {
        controller = GetComponent<VehicleController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        controller.setDestination(currentWaypoint.transform.localPosition);
    }

    // Update is called once per frame
    void Update()
    {

        if (controller.reachedDestination)
        {

            if (currentWaypoint == null)
            {
                currentWaypoint = GameObject.Find("LaneWaypoint0").GetComponent<Waypoint>();
            }

            bool shouldBranch = false;


            if (currentWaypoint.branches != null && currentWaypoint.branches.Count > 0)
            {
                shouldBranch = Random.Range(0f, 1f) <= currentWaypoint.branchRatio ? true : false;
            }


            if (shouldBranch)
            {
                currentWaypoint = currentWaypoint.branches[Mathf.RoundToInt(Random.Range(0f, currentWaypoint.branches.Count - 1))];
            }
            else
            {
                if (currentWaypoint.nextWaypoint != null)
                {
                    currentWaypoint = currentWaypoint.nextWaypoint;

                }
                else
                {
                    // reached the end , respawn vehiclke to another random Waypoint
                    currentWaypoint = GameObject.Find("LaneWaypoint" + Random.Range(0, 10)).GetComponent<Waypoint>();
                    transform.position = currentWaypoint.transform.position;

                    // Let the vehicle to face next Waypoint
                    transform.LookAt(currentWaypoint.nextWaypoint.transform);
                }
            }
        }

        controller.setDestination(currentWaypoint.transform.localPosition);

    }

}

