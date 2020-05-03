using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class WaypointManager : MonoBehaviour
{
    public List<Vector3> points = new List<Vector3>();
    public bool looping = false;

    private void OnEnable()
    {
        if (points.Count == 0)
        {
            points.Add(transform.position);
        }
    }

    public void Add()
    {
        if (points.Count >= 1)
        {
            points.Add(new Vector3(points[points.Count - 1].x * 1.2f, points[points.Count - 1].y * 1.2f, points[points.Count - 1].y * 1.2f));
        }
    }

    public void Remove()
    {
        if(points.Count > 1)
        points.RemoveAt(points.Count - 1);
    }

    public void Insert(int i)
    {
        Vector3 insertPos = CalculateMidPoint(i);
        points.Insert(i + 1, insertPos);
    }

    public void RemoveAt()
    {

    }

    public Vector3 CalculateMidPoint(int index)
    {
        index = Mathf.Clamp(index, 0, points.Count - 1);
        if(index == points.Count-1)
        {
            if(looping)
            {
                return points[0] + points[index] / 2;
            }
            else
            {
                return new Vector3(points[points.Count - 1].x /*+ 0.5f*/, points[points.Count - 1].y, points[points.Count - 1].z);
            }
        }
        else
        {
            return ((points[index] + points[index + 1]) / 2);
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(points[0], 0.5f);
        for (int i = 1; i < points.Count; i++)
        {
            Gizmos.DrawLine(points[i - 1], points[i]);
            Gizmos.DrawSphere(points[i], 0.5f);
        }
        if (looping) Gizmos.DrawLine(points[points.Count - 1], points[0]);
    }
}
;