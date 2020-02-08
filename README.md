# SplineSystem-Unity

## Implemented
1. Implemented Density based Spline
2. Implemented Distanced based Spline
3. Tangent Vector
4. Left Right Normal Excludable Vector

## WIP Screenshots

![progress](https://user-images.githubusercontent.com/45932883/73134108-950a4780-4058-11ea-85e9-08d08ca40338.PNG)


### Usage

```C#
        SplineCore spline;
        spline = new SplineCore();

        spline.Positions = getPos(); ///put your vector 3 positions here 
        spline.SubDivisionType = DivisionType.DistanceBased;
        //spline.IsLoop = true;
        spline.DistanceOffset = 20;
        spline.Resolution = 0.1f;
        var points = spline.Compute();
        
        for (int i = 0; i < points.Length; i++)
        {
        //Vector3 normal=points[i].Normal;
        //Vector3 tan=points[i].Tangent;
        /Vector3 p=points[i].Position;

        }
```
