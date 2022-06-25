using UnityEngine;

public class BetterJointControl : MonoBehaviour {
    public ArticulationBody joint;
    private BetterController controller;


    private void Start() {
        controller = (BetterController)GetComponentInParent(typeof(BetterController));
        joint = GetComponent<ArticulationBody>();
        var drive = joint.xDrive;
        drive.stiffness = controller.stiffness;
        drive.damping = controller.damping;
        drive.forceLimit = controller.torqueLimit;
        joint.xDrive = drive;
    }

    public float GetPosition(bool degrees) {
        if (joint == null) {
            Debug.LogWarning("Agent attempting to access uninitialized joint");
            return 0f;
        }

        var degreepos = joint.jointPosition[0] * Mathf.Rad2Deg % 360f;
        if (degrees)
            return degreepos;
        return degreepos / 360;
    }

    public void SetTarget(float newPos, bool degrees) {
        if (joint == null) {
            Debug.LogWarning("Agent attempting to access uninitialized joint");
            return;
        }
        if (!degrees) newPos *= 360f;
        newPos %= 360f;
        var currentDrive = joint.xDrive;
        if (newPos > currentDrive.upperLimit)
            currentDrive.target = currentDrive.upperLimit;
        else if (newPos < currentDrive.lowerLimit)
            currentDrive.target = currentDrive.lowerLimit;
        else
            currentDrive.target = newPos;
        joint.xDrive = currentDrive;
    }

    public float GetTarget(bool degrees) {
        if (degrees)
            return joint.xDrive.target;
        return joint.xDrive.target / 360;
    }

    public void UpdatePdParams() {
        joint = GetComponent<ArticulationBody>();
        var drive = joint.xDrive;
        drive.stiffness = controller.stiffness;
        drive.damping = controller.damping;
        joint.xDrive = drive;
    }
}