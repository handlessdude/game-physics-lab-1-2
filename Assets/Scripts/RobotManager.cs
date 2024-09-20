using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RobotManager : MonoBehaviour
{
    public GameObject endEffector;
    RobotJoint[] joints;
    public bool useForwardKinematics = true;
    
    private void Awake()
    {
        joints = GetComponentsInChildren<RobotJoint>();
    }
    
    private void FixedUpdate()
    {
        // angles[i] === projection of the joint's local rotation vector onto its rotation axis
        // each joint.Axis is already normalized
        float[] angles = joints.Select(joint => Vector3.Dot(joint.Axis, joint.transform.localRotation.eulerAngles)).ToArray();

        if (useForwardKinematics)
        {
             endEffector.transform.position = ForwardKinematics(angles);
        }
        else
        {
            // TODO: implement inverse kinematics
        }
    }
    
    // returns the position of the end effector, in global coordinates.
    // For each joint, method calculates the next position by rotating the offset (distance from one joint to the next) using the cumulative rotation up to that joint
    private Vector3 ForwardKinematics(float[] angles)
    {
        Vector3 prevPoint = joints[0].transform.position;
        Quaternion rotation = Quaternion.identity; // indicates "no rotation"
        for (int i = 1; i < joints.Length; i++)
        {
            // Rotates around a new axis
            rotation *= Quaternion.AngleAxis(angles[i - 1], joints[i - 1].Axis);
            Vector3 nextPoint = prevPoint + rotation * joints[i].StartOffset;
    
            prevPoint = nextPoint;
        }
        return prevPoint;
    }
}
