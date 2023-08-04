using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace Assets.Scripts.mainScriptContainer
{
    /* a bit of a short history.
    
    I initally used collider.2d to 
    do my correction of position. https://docs.unity3d.com/ScriptReference/Collider2D.Distance.html
    However, it did not work as expected so I had to come up with my own collision detection. this is where
    the circlecalculator comes in.
    It takes two circles and does calculation to find out a vector2 to seperate them apart.
    */
    public class CircleCalculator : MonoBehaviour
    {
        //used to calculate the vector 2 position needed to seperate two circluar object.
        public static Vector2 DistanceToMoveBetweenDriverAndDrivenGear(
            Gear driverGear,
            Gear drivenGear)
        {
            Circle driverCircle = new Circle(driverGear.transform.position, driverGear.InnerGearRadius);
            Circle drivenCircle = new Circle(drivenGear.transform.position, drivenGear.GearRadius);
            Line lineConnectingBothGear = new Line(driverGear.transform.position, drivenGear.transform.position);

            Vector2 pointA = lineConnectingBothGear.GetPointThatIsIntersectCircle(driverCircle, drivenCircle);
            Vector2 pointB = lineConnectingBothGear.GetPointThatIsIntersectCircle(drivenCircle, driverCircle);

            //point A and point B are the shortest point to seperate the two circles

            return pointA - pointB; 
        }
    }

    public struct Line
    {

        //line is just a struct that calculate the line equation y = kx + c
        public float K { get; private set; }
        public float C { get; private set; }


        //formula for line : y = kx + C 
        /*
             k: gradient of line, also the normalise vector 
             c: Constant
        */
    public Line(Vector3 driverGearOrigin, Vector3 drivenGearOrigin)
        {
            float yDifference = driverGearOrigin.y - drivenGearOrigin.y;
            float xDifference = driverGearOrigin.x - drivenGearOrigin.x;
            K = yDifference / xDifference;
            C = driverGearOrigin.y - (K * driverGearOrigin.x);
        }


        public Vector2 GetPointThatIsIntersectCircle(Circle circleToCheck, Circle circleToIntersect)
        {
            Vector2[] twopoints = GetTwoPointsIntersectingTheCircle(circleToCheck);
            for (int i = 0; i < twopoints.Length; i++)
            {
                if (circleToIntersect.IsPointInCircle(twopoints[i]))
                {
                    return twopoints[i];
                }
            }

            //problem
            /*
            1. if the circle is bigger, then it return vector 2 zero. this is bad and 
            it can send the gear flying.
            */
            return twopoints[1]; // return the furthest point.
        }
        private Vector2[] GetTwoPointsIntersectingTheCircle(Circle circle)
        {
            // whenever a line intersect a circle, it will depends on have one or two points.
            //in this case,it is usually two points as no way a a circle that overlap forms a tangents in one of the circles.
            float[] xValues = GetXValuesFromCircle(circle); 
            Vector2[] points = new Vector2[2];
            for (int i = 0; i < xValues.Length; i++)
            {
                points[i] = new Vector2(xValues[i], GetYFromXValue(xValues[i]));
            }
            return points;
        }
        private float[] GetXValuesFromCircle(Circle circle)
        {
            // y = kx + c
            // R^2 = (x - h)^2 + (y - g)^2 
            // R^2 = x^2 - 2hx + h^2 + k^2x^2 + 2ckx + c^2 - 2gkx - 2gc + g^2
            //have to do substitute to get the x value
            // after doing abit of math
            // r^2 - h^2 - c^2 +2gc -g^2 = x^2 (1 + k^2) + x(2ck - 2h - 2gk)
            // a = (1 + k^2)
            // b = (2ck - 2h - 2gk )
            // c = r^2 - h^2 - c^2 +2gc -g^2
            // c = ax^2 + bx
            // 0 = ax^2 + bx - c
            // quadratic equation: x = (-b + sqrt(b^2 - 4ac)) / 2a
            float a = 1 + (K * K);
            float b = (2 * C * K) - (2 * circle.XOrigin) - (2 * circle.YOrigin * K);
            float c = (circle.Radius * circle.Radius) - 
                (circle.XOrigin * circle.XOrigin) - 
                (C * C) + 
                (2 * circle.YOrigin * C) - 
                (circle.YOrigin * circle.YOrigin);
            return new float[] { QuadraticEquation(a, b, -c, false) , QuadraticEquation(a, b, -c, true) };
        }

        private float QuadraticEquation(float a, float b, float c , bool isNegative)
        {
            if (isNegative)
            {
                return ((-b - math.sqrt((b * b) - (4 * a * c))) / (2 * a));
            }
            else
            {
                return ((-b + math.sqrt((b * b) - (4 * a * c))) / (2 * a));
            }
        }

        public float GetXFromYValue(float y)
        {
            // x = (y - c) / k
            return (y - C) / K;
        }

        public float GetYFromXValue(float x)
        {
            //y = kx + C;
            return (x * K) + C;
        }
    }

    public struct Circle
    {
        //formula for a cirlce: r^2 = (x - h)^2 + (y - g)^2
        // h is the x origin of the circle
        // g is the y origin of the circle
        // r is the radius of the circle
        public float XOrigin { get; private set; }
        public float YOrigin { get; private set; }
        public float Radius { get; private set; }

        public Circle(Vector3 origin, float radius)
        {
            Radius = radius;
            XOrigin = origin.x;
            YOrigin = origin.y;
        }

        public bool IsPointInCircle(Vector3 point)
        {
            float distanceFromTwoPoints = PowerOfTwo((point.x - XOrigin)) + PowerOfTwo((point.y - YOrigin));
            float sqrt = (float) Math.Sqrt((double)distanceFromTwoPoints);
            return Radius >= sqrt;
        }
        private float PowerOfTwo( float value) { return value * value; }
    }
}