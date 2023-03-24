using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointNavigator : MonoBehaviour
{

    CharacterController controller;
    public Waypoint currentWaypoint;

    int direction;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        //direction = Mathf.RoundToInt(Random.Range(0f, 1f));
        controller.setDestination(currentWaypoint.GetPosition());
    }

    // Update is called once per frame
    void Update()
    {

        if (controller.reachedDestination)
        {

            if (currentWaypoint == null)
            {
                currentWaypoint = GameObject.Find("Waypoint0").GetComponent<Waypoint>();
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

                if (direction == 0)
                {
                    if (currentWaypoint.nextWaypoint != null)
                    {
                        currentWaypoint = currentWaypoint.nextWaypoint;

                    }
                    else
                    {
                        currentWaypoint = currentWaypoint.previousWaypoint;
                        direction = 1;
                    }


                }
                else if (direction == 1)
                {
                    if (currentWaypoint.previousWaypoint != null)
                    {
                        currentWaypoint = currentWaypoint.previousWaypoint;

                    }
                    else
                    {
                        currentWaypoint = currentWaypoint.nextWaypoint;
                        direction = 0;
                    }
                }

            }

            controller.setDestination(currentWaypoint.GetPosition());

        }
    }
}
