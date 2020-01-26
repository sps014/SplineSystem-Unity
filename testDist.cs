using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class testDist : MonoBehaviour
{
    public Transform[] array;
    public float offset = 2;
    private float calcoffset=0;
    void Start()
    {
        
    }
    private void OnDrawGizmos()
    {
        calcoffset=0;
        if (array == null)
            return;
        for (int i = 0; i < array.Length; i++)
        {
            Gizmos.color=Color.green;
            //Gizmos.DrawSphere(array[i].position, 5);
           // Gizmos.DrawSphere(array[i].position, 5);
            Gizmos.color = Color.red;
            
            if(array[i]!=array.Last())
            {
                Gizmos.DrawLine(array[i].position, array[i + 1].position);
                var dist=Vector3.Distance(array[i].position,array[i+1].position);
                int tis=(int)(dist/offset);
                for (float j = calcoffset; j <= dist; j+=offset)
                {
                    float t=(j)/(dist);
                    if(t<0||t>1)
                        continue;
                    
                    if(j>dist-offset)
                    {
                        calcoffset=offset-(dist-(j));
                    }
                    Gizmos.DrawSphere(Vector3.Lerp(array[i].position,array[i+1].position,t),3);
                    
                }

            }
        }
    }
    void Update()
    {
       
    }
    Vector3[] getPos()
    {
        List<Vector3> pt = new List<Vector3>();
        foreach (var item in array)
        {
            Gizmos.color=Color.green;
            Gizmos.DrawSphere(item.position, 5);
            pt.Add(item.position);
        }

        pt.Insert(0, array[0].position);
        pt.Add(array.Last().position);
        return pt.ToArray();
    }
}
