using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{

    public Waypoint nextWaypoint;
    public Waypoint previousWaypoint;

    [Range(0.0f, 5.0f)]
    public float width = 3.2f;

    public List<Waypoint> branches;

    [Range(0.0f, 1.0f)]
    public float branchRatio = 0.5f;

    public Vector3 GetPosition()
    {
        Vector3 minBound = transform.position + transform.right * width / 2f;
        Vector3 maxBound = transform.position - transform.right * width / 2f;

        return Vector3.Lerp(minBound, maxBound, Random.Range(0.0f, 1.0f));

    }
}
