using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SplineSystem
{
	public class SplineBrain
	{
		public Vector3[] Positions { get; set; }
		public bool IsLoop { get; set; }
		public DivisionType SubDivisionType { get; set; } = DivisionType.DensityBased;
		public float Resolution { get; set; } = 0.2f;
		public float DistanceOffset { get; set; } = 4;

		private List<SonicPoint> Points { get; set; } = new List<SonicPoint>();
		private float OffsetTrack { get; set; } = 0;
		public SonicPoint[] Compute()
		{
			Points.Clear();
			OffsetTrack = 0;


			ComputeSpline();


			return Points.ToArray();
		}
		private void ComputeSpline()
		{
			for (int i = 0; i < Positions.Length; i++)
			{
				if ((i == 0 || i == Positions.Length - 2 || i == Positions.Length - 1) && !IsLoop)
				{
					continue;
				}

				if (SubDivisionType == DivisionType.DensityBased)
					ComputeDensity(i);
				else
					ComputeDistance(i);
			}
		}
		private void ComputeDistance(int ind)
		{
			Vector3 p0 = Positions[ClampIndex(ind - 1)];
			Vector3 p1 = Positions[ind];
			Vector3 p2 = Positions[ClampIndex(ind + 1)];
			Vector3 p3 = Positions[ClampIndex(ind + 2)];

			float distance = Vector3.Distance(p1, p2);
			distance += OffsetTrack;

			//total division needed
			int tis=(int)(distance/DistanceOffset);

			for (int i =0; i <= tis; i++)
			{
				//Which t position are we at?
                float t=(i*DistanceOffset-OffsetTrack)/distance;

				if(t<0)
                    continue;

				if (i == tis)
					OffsetTrack = distance - i * DistanceOffset;

				//Find the coordinate between the end points with a Catmull-Rom spline
				SonicPoint point = GetCatmullPoint(t, p0, p1, p2, p3);
				Points.Add(point);
			}

		}
		private void ComputeDensity(int ind)
		{

			Vector3 p0 = Positions[ClampIndex(ind - 1)];
			Vector3 p1 = Positions[ind];
			Vector3 p2 = Positions[ClampIndex(ind + 1)];
			Vector3 p3 = Positions[ClampIndex(ind + 2)];

			//The spline's resolution

			//How many times should we loop?
			int loops = Mathf.FloorToInt(1f/Resolution);

			for (int i = 1; i <= loops; i++)
			{
				//Which t position are we at?
				float t = i * Resolution;

				//Find the coordinate between the end points with a Catmull-Rom spline
				SonicPoint point = GetCatmullPoint(t, p0, p1, p2, p3);
				Points.Add(point);
			}
		}

		private int ClampIndex(int pos)
		{
			if (pos < 0)
			{
				pos = Positions.Length - 1;
			}

			if (pos > Positions.Length)
			{
				pos = 1;
			}
			else if (pos > Positions.Length - 1)
			{
				pos = 0;
			}

			return pos;
		}

		SonicPoint GetCatmullPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
		{
			//The coefficients of the cubic polynomial (except the 0.5f * which I added later for performance)
			Vector3 a = 2f * p1;
			Vector3 b = p2 - p0;
			Vector3 c = 2f * p0 - 5f * p1 + 4f * p2 - p3;
			Vector3 d = -p0 + 3f * p1 - 3f * p2 + p3;

			//The cubic polynomial: a + b * t + c * t^2 + d * t^3
			Vector3 pos = 0.5f * (a + (b * t) + (c * t * t) + (d * t * t * t));
			Vector3 tan = 0.5f * (a + (b) + 2*(c * t) + 3*(d * t * t));

			SonicPoint point = new SonicPoint();
			point.Position = pos;
			point.Tangent = tan;
			return point;
		}

	}

}
