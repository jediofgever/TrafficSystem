using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleSpawner : MonoBehaviour
{

    public GameObject vehiclePrefab;
    public int vehicleCount;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnVehicles());
    }

    IEnumerator SpawnVehicles()
    {
        int count = 0;
        while (count < vehicleCount)
        {
            GameObject vehicle = Instantiate(vehiclePrefab);
            Transform child = transform.GetChild(Random.Range(0, transform.childCount - 1));
            vehicle.GetComponent<VehicleWaypointNavigator>().currentWaypoint = child.GetComponent<Waypoint>();
            vehicle.transform.position = child.position;
            vehicle.transform.parent = transform;
            yield return new WaitForEndOfFrame();
            count++;
        }
    }

}
