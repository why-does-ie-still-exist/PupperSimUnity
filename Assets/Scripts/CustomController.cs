using Unity.Robotics.UrdfImporter.Control;
using UnityEngine;

public class CustomController : MonoBehaviour {
    // [InspectorReadOnly(true)] public string selectedJoint;
    //
    // [HideInInspector] public int selectedIndex;
    //
    // [Tooltip("Kp parameter. Units: Nm/deg")]
    // public float stiffness = 1f;
    //
    // [Tooltip("Kd parameter. Units: Nm/(deg/s)")]
    // public float damping;
    //
    // // [HideInInspector]
    // public float forceLimit = 10f;
    //
    // [Tooltip("How fast keyboard inputs change target")]
    // public float speed = 10f; // Units: degree/s
    //
    // private ArticulationBody[] articulationChain;
    //
    // private int previousIndex;
    //
    // private void Start() {
    //     previousIndex = selectedIndex = 1;
    //     articulationChain = GetComponentsInChildren<ArticulationBody>();
    //     foreach (var joint in articulationChain) {
    //         joint.gameObject.AddComponent<OldJointControl>();
    //     }
    //     DisplaySelectedJoint(selectedIndex);
    // }
    //
    // private void Update() {
    //     var selectionInput1 = Input.GetKeyDown("right");
    //     var selectionInput2 = Input.GetKeyDown("left");
    //
    //     SetSelectedJointIndex(selectedIndex); // to make sure it is in the valid range
    //     UpdateDirection(selectedIndex);
    //
    //     if (selectionInput2)
    //         SetSelectedJointIndex(selectedIndex - 1);
    //     else if (selectionInput1) SetSelectedJointIndex(selectedIndex + 1);
    //
    //     DisplaySelectedJoint(selectedIndex);
    //     UpdateDirection(selectedIndex);
    // }
    //
    // public void OnGUI() {
    //     var centeredStyle = GUI.skin.GetStyle("Label");
    //     centeredStyle.alignment = TextAnchor.UpperCenter;
    //     GUI.Label(new Rect(Screen.width / 2 - 200, 10, 400, 20), "Press left/right arrow keys to select a robot joint.",
    //         centeredStyle);
    //     GUI.Label(new Rect(Screen.width / 2 - 200, 30, 400, 20),
    //         "Press up/down arrow keys to move " + selectedJoint + ".", centeredStyle);
    // }
    //
    // /// <summary>
    // ///     Sets the direction of movement of the joint on every update
    // /// </summary>
    // /// <param name="jointIndex">Index of the link selected in the Articulation Chain</param>
    // private void UpdateDirection(int jointIndex) {
    //     if (jointIndex < 0 || jointIndex >= articulationChain.Length) return;
    //
    //     var moveDirection = Input.GetAxis("Vertical");
    //     var current = articulationChain[jointIndex].GetComponent<OldJointControl>();
    //     if (previousIndex != jointIndex) {
    //         var previous = articulationChain[previousIndex].GetComponent<OldJointControl>();
    //         previous.direction = RotationDirection.None;
    //         previousIndex = jointIndex;
    //     }
    //
    //     if (moveDirection > 0)
    //         current.direction = RotationDirection.Positive;
    //     else if (moveDirection < 0)
    //         current.direction = RotationDirection.Negative;
    //     else
    //         current.direction = RotationDirection.None;
    // }

    // private void SetSelectedJointIndex(int index) {
    //     if (articulationChain.Length > 0) selectedIndex = (index + articulationChain.Length) % articulationChain.Length;
    // }
    //
    // private void DisplaySelectedJoint(int selectedIndexArg) {
    //     if (selectedIndexArg < 0 || selectedIndexArg >= articulationChain.Length) return;
    //
    //     selectedJoint = articulationChain[selectedIndexArg].name + " (" + selectedIndexArg + ")";
    // }
}