using Unity.Robotics.UrdfImporter.Control;
using UnityEngine;

public class OldJointControl : MonoBehaviour {
    // public RotationDirection direction;
    // public float speed;
    // public ArticulationBody joint;
    // private CustomController controller;
    //
    //
    // private void Start() {
    //     direction = 0;
    //     controller = (CustomController)GetComponentInParent(typeof(CustomController));
    //     joint = GetComponent<ArticulationBody>();
    //     speed = controller.speed;
    //     var drive = joint.xDrive;
    //     drive.stiffness = controller.stiffness;
    //     drive.damping = controller.damping;
    //     drive.forceLimit = controller.forceLimit;
    //     joint.xDrive = drive;
    // }
    //
    // private void FixedUpdate() {
    //     speed = controller.speed;
    //     
    //     if (joint.jointType != ArticulationJointType.FixedJoint) {
    //         var currentDrive = joint.xDrive;
    //         var newTargetDelta = (int)direction * Time.fixedDeltaTime * speed;
    //
    //         if (joint.jointType == ArticulationJointType.RevoluteJoint) {
    //             if (joint.twistLock == ArticulationDofLock.LimitedMotion) {
    //                 if (newTargetDelta + currentDrive.target > currentDrive.upperLimit)
    //                     currentDrive.target = currentDrive.upperLimit;
    //                 else if (newTargetDelta + currentDrive.target < currentDrive.lowerLimit)
    //                     currentDrive.target = currentDrive.lowerLimit;
    //                 else
    //                     currentDrive.target += newTargetDelta;
    //             }
    //             else {
    //                 currentDrive.target += newTargetDelta;
    //             }
    //         }
    //
    //         else if (joint.jointType == ArticulationJointType.PrismaticJoint) {
    //             if (joint.linearLockX == ArticulationDofLock.LimitedMotion) {
    //                 if (newTargetDelta + currentDrive.target > currentDrive.upperLimit)
    //                     currentDrive.target = currentDrive.upperLimit;
    //                 else if (newTargetDelta + currentDrive.target < currentDrive.lowerLimit)
    //                     currentDrive.target = currentDrive.lowerLimit;
    //                 else
    //                     currentDrive.target += newTargetDelta;
    //             }
    //             else {
    //                 currentDrive.target += newTargetDelta;
    //             }
    //         }
    //
    //         joint.xDrive = currentDrive;
    //     }
    // }
}