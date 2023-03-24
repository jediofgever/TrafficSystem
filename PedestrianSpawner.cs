using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestrianSpawner : MonoBehaviour
{

    public GameObject pedestrianPrefab;
    public int pedestrianCount;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnPedestrians());
    }

    IEnumerator SpawnPedestrians()
    {
        int count = 0;
        while (count < pedestrianCount)
        {
            GameObject pedestrian = Instantiate(pedestrianPrefab);
            Transform child = transform.GetChild(Random.Range(0, transform.childCount - 1));
            pedestrian.GetComponent<WaypointNavigator>().currentWaypoint = child.GetComponent<Waypoint>();
            pedestrian.transform.position = child.position;
            pedestrian.transform.parent = transform;
            yield return new WaitForEndOfFrame();
            count++;
        }
    }

}
