using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotJoint : MonoBehaviour
{
    public Vector3 Axis;
    public Vector3 StartOffset;
    
    public float MinAngle = float.NegativeInfinity;
    public float MaxAngle = float.PositiveInfinity;
    
    void Awake ()
    {
        // Position of the transform relative to the parent transform.
        StartOffset = transform.localPosition;
    }
}