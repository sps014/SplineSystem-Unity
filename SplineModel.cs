using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SplineSystem
{
    public enum DivisionType
    {
        DistanceBased,
        DensityBased
    }
    [System.Serializable]
    public struct SonicPoint
    {
        public Vector3 Position { get; set; }
        public Vector3 Tangent { get; set; }
        public Vector3 Normal => Vector3.Cross(Tangent, Vector3.up).normalized / 2;

        //public Vector3 Normal { get; set; }
        
        public Vector3 TangentPosition => Position + Tangent.normalized;

        public Vector3 Left => Vector3.Cross(Tangent.normalized, Vector3.up).normalized / 2;
        public Vector3 Right => Vector3.Cross(Tangent.normalized, Vector3.up).normalized / -2;
        public Vector3 GetTangent(float mag = 10)
        {
            return Position + Tangent.normalized * mag;
        }
        public Vector3 GetLeft(float mag=10)
        {
            return (Vector3.Cross(Tangent.normalized, Vector3.up).normalized / 2)*mag+Position;
        }
        public Vector3 GetRight(float mag = 10)
        {
            return (Vector3.Cross(Tangent.normalized, Vector3.up).normalized / 2) * -mag + Position;
        }    

    }
}
