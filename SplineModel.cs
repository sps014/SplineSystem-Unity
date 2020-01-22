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
    public struct SonicPoint
    {
        public Vector3 Position { get; set; }
        public Vector3 Tangent { get; set; }
    }
}
