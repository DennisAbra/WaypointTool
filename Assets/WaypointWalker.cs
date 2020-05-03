using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointWalker : MonoBehaviour
{
    [SerializeField] WaypointManager manager;
    [SerializeField] float speed;
    [SerializeField] float timeBetweenPoints;

    float t = 0;
    int index = 0;

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime * speed;
        if (t > timeBetweenPoints)
        {
            t = 0;
            if (index == manager.points.Count - 1)
            {
                index = 0;
            }
            else
            index++;
        }
        else
        {
            if (index < manager.points.Count - 1)
            {
                transform.position = Vector3.Lerp(manager.points[index], manager.points[index + 1], t);

            }
            else if(manager.looping && index == manager.points.Count-1)
            {
                transform.position = Vector3.Lerp(manager.points[index], manager.points[0], t);
            }
        }
    }
}
