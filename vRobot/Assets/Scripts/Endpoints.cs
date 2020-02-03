using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Endpoints : MonoBehaviour
{
    public GameObject prefab;
    public float leverLength;

    public int xRange;
    public int yRange;

    public int spacingBetweenEndpoints;

    public EndpointPositionData positionData;

    void Start()
    {
        for(int i = 0; i < positionData.coordinates.Count; i++) {
            //var position = new Vector3(Random.Range(-xRange / 2, xRange / 2), 0, (yRange*i/(count-1))-(yRange/2));
            Vector2 coordinates = positionData.coordinates[i] * spacingBetweenEndpoints;
            GameObject pendulum = Instantiate(prefab, new Vector3(coordinates.x, 0, coordinates.y), Quaternion.identity, transform);
            pendulum.GetComponent<Pendulum>().leverLength = leverLength;
        }
    }
}
