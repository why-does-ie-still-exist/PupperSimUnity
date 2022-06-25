using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BetterController : MonoBehaviour {
    [Tooltip("Kp parameter. Units: Nm/deg")]
    public float stiffness = 1f;

    [Tooltip("Kd parameter. Units: Nm/(deg/s)")]
    public float damping;

    [Tooltip("Torque limit for actuators. Units: Nm")]
    public float torqueLimit = 1f;

    private readonly Dictionary<string, BetterJointControl> jointdict = new();
    private readonly List<BetterJointControl> joints = new();
    private readonly List<string> relevantjointnames = new();

    private Vector3 baseupvec;
    private bool basevecinit;

    private bool initialized;

    /// <summary>
    ///     This is called when everything is initialized in the scene, before the first frame
    ///     Here we add BetterJointControl to all the joint actuators provided to us by the URDF loader
    ///     We then store these BetterJointControl objects in easy-to-access data structures
    /// </summary>
    private void Start() {
        //Generating all the joint names because I didn't want to write them all out
        foreach (var direction in new[] { "right", "left" })
        foreach (var frontorback in new[] { "Rear", "Front" })
        foreach (var height in new[] { "", "Upper", "Lower" }) {
            relevantjointnames.Add(direction + frontorback + height + "Leg");
        }

        //This gets all the articulations in the robot. Articulations are an abstraction on joints and actuators.
        var articulationChain = GetComponentsInChildren<ArticulationBody>();

        foreach (var joint in articulationChain) {
            //All our robot joints are articulations. But not all articulations are ones we can control or care about.
            if (relevantjointnames.Contains(joint.name)) {
                //If we find an articulation we care about, we add our wrapper class to that articulation
                var betterJointControl = joint.gameObject.AddComponent<BetterJointControl>();
                joints.Add(betterJointControl);
                jointdict.Add(joint.name, betterJointControl);
            }
        }
    }

    public void Update() {
        if (!basevecinit) {
            //In our robot model, a vector pointing up is in the negative z/blue direction
            baseupvec = -GameObject.FindGameObjectWithTag("robotbody").GetComponent<Transform>().forward;
            basevecinit = true;
        }
    }

    /// <summary>
    ///     This function collects the positions of the joints.
    /// </summary>
    /// <param name="degrees">Whether to return positions in degrees or in normalized 0-1 magnitudes</param>
    /// <returns>Joint positions from 0-1 in an array in the same order as relevantjointnames</returns>
    public float[] CollectJointPositions(bool degrees) {
        var positions = new List<float>();
        foreach (var jointname in relevantjointnames) {
            positions.Add(jointdict[jointname].GetPosition(degrees));
        }

        return positions.ToArray();
    }

    /// <summary>
    ///     Writes out joint target positions to the robot
    /// </summary>
    /// <param name="positions">Joint positions in degrees in an array in the same order as relevantjointnames</param>
    /// <param name="degrees">Whether to use degrees or 0-1 to indicate positions</param>
    public void WriteJointPositions(float[] positions, bool degrees) {
        for (var i = 0; i < relevantjointnames.Count; i++) {
            var jointname = relevantjointnames[i];
            var joint = jointdict[jointname];
            joint.SetTarget(positions[i], degrees);
        }
    }

    /// <summary>
    ///     Updates all joint PD parameters to match the controller. This will be useful for learning the parameters
    /// </summary>
    /// <param name="Kp">"Stiffness" of joint</param>
    /// <param name="Kd">"Damping" of joint</param>
    public void UpdatePdParams(float Kp, float Kd) {
        stiffness = Kp;
        damping = Kd;
        foreach (var jointControl in joints) {
            jointControl.UpdatePdParams();
        }
    }

    public void DeltaAllJoints(float delta, bool degrees) {
        foreach (var betterJointControl in joints) {
            betterJointControl.SetTarget(betterJointControl.GetTarget(degrees) + delta, degrees);
        }
    }

    public bool CheckIfFed() {
        if (!basevecinit) return false;
        var currentvec = -GameObject.FindGameObjectWithTag("robotbody").GetComponent<Transform>().forward;
        //This is the only sane way to do this without using quaternions.
        var randomvec = Random.insideUnitSphere.normalized;
        var someorthvec = Vector3.Cross(currentvec, randomvec);
        var anotherorthvec = Vector3.Cross(currentvec, someorthvec);
        //We now have a basis for R3, our rotation vector and two more random, mutually orthogonal vectors.
        //These other vectors can form plane normals, which we can project our base orientation vector and our current vector onto, and compare the angle displacements

        //Plane 1:
        Vector2 basein1 = Vector3.ProjectOnPlane(baseupvec, someorthvec).normalized;
        Vector2 currentin1 = Vector3.ProjectOnPlane(currentvec, someorthvec).normalized;
        var plane1Angle = Math.Acos(Vector2.Dot(basein1, currentin1)) * Mathf.Rad2Deg;
        if (plane1Angle > 80) return true;
        //Plane 2:
        Vector2 basein2 = Vector3.ProjectOnPlane(baseupvec, anotherorthvec).normalized;
        Vector2 currentin2 = Vector3.ProjectOnPlane(currentvec, anotherorthvec).normalized;
        var plane2Angle = Math.Acos(Vector2.Dot(basein2, currentin2)) * Mathf.Rad2Deg;
        return plane2Angle > 80;
    }
}