using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SplineSystem;
using System.Linq;

public class testSpline : MonoBehaviour
{
    public Transform[] array;
    SplineBrain brain;
    void Start()
    {
        brain = new SplineBrain();
    }
    void Update()
    {

    }

    private void OnDrawGizmos()
    {
        if (brain == null)
            brain = new SplineBrain();

        brain.Positions = getPos();
        brain.SubDivisionType = DivisionType.DistanceBased;
        //brain.IsLoop = true;
        brain.Resolution = 0.1f;
        var points = brain.Compute();
        
        for (int i = 0; i < points.Length; i++)
        {
            Gizmos.color = Color.red;
            //if (i == points.Length - 1)
            //    Gizmos.DrawLine(points[i].Position, points[0].Position);
            if(i!=points.Length-1)
                Gizmos.DrawLine(points[i].Position, points[i + 1].Position);
            Gizmos.color = Color.green;
            Vector3 vec = Vector3.Cross( points[i].Position+points[i].Tangent.normalized, points[i].Position);
            //vec = vec.normalized;
            Gizmos.DrawLine(vec, points[i].Position);
        }
    }
    Vector3[] getPos()
    {
        List<Vector3> pt = new List<Vector3>();
        foreach (var item in array)
        {
            Gizmos.DrawSphere(item.position, 5);
            pt.Add(item.position);
        }

        pt.Insert(0, array[0].position);
        pt.Add(array.Last().position);
        return pt.ToArray();
    }
}
