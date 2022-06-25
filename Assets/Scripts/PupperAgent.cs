using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class PupperAgent : Agent {
    public bool trainingmode;
    private Vector3 acceleration = Vector3.zero;
    private Vector3 baseEulerAngles = new(90f, 180f, 270f);
    private Rigidbody body;

    private BetterController controller;
    private Vector3 middlepos = Vector3.zero;
    private Vector3 newestpos = Vector3.zero;
    private Vector3 oldestpos = Vector3.zero;
    private int state;

    private void Start() {
        body = GameObject.FindGameObjectWithTag("robotbody").GetComponent<MeshCollider>()
            .attachedRigidbody;
        controller = gameObject.GetComponent<BetterController>();
    }

    /// <summary>
    ///     Called every frame
    /// </summary>
    private void Update() {
        //TODO: Add reward and penalty
        //EndEpisode();
        if (!trainingmode) {
            var selectionInput1 = Input.GetKey("right");
            var selectionInput2 = Input.GetKey("left");
            if (selectionInput1) //.2 degrees per frame = 12 deg/s
                controller.DeltaAllJoints(+.2f, true);
            else if (selectionInput2) controller.DeltaAllJoints(-.2f, true);
        }

        Debug.Log(controller.CheckIfFed());
    }

    /// <summary>
    ///     Called every .02 seconds
    /// </summary>
    private void FixedUpdate() {
        UpdateAcceleration();
    }

    private void UpdateAcceleration() {
        var currentpos = GameObject.FindGameObjectWithTag("robotbody").GetComponent<Transform>().position;
        switch (state) {
            case 0:
                oldestpos = currentpos;
                state = 1;
                break;
            case 1:
                middlepos = currentpos;
                state = 2;
                break;
            case 2:
                newestpos = currentpos;
                state = 0;
                var earliervel = (middlepos - oldestpos) / Time.fixedDeltaTime;
                var latervel = (newestpos - middlepos) / Time.fixedDeltaTime;
                var accel = (latervel - earliervel) / Time.fixedDeltaTime;
                acceleration = accel;
                break;
        }
    }

    /// <summary>
    ///     Initialize the agent
    /// </summary>
    public override void Initialize() { }

    /// <summary>
    ///     Reset the agent when an episode begins
    /// </summary>
    public override void OnEpisodeBegin() {
        transform.position = new Vector3(0, 0.3f, 0);
        transform.rotation = Quaternion.Euler(Vector3.zero);
    }

    public override void OnActionReceived(ActionBuffers actions) {
        if (trainingmode) {
            var vectorAction = actions.ContinuousActions.Array;
            controller.WriteJointPositions(vectorAction, false);
        }
    }

    /// <summary>
    ///     Collect vector observations from the environment
    /// </summary>
    /// <param name="sensor">The vector sensor</param>
    public override void CollectObservations(VectorSensor sensor) {
        sensor.AddObservation(controller.CollectJointPositions(false));
        sensor.AddObservation(acceleration);
        sensor.AddObservation(GameObject.FindGameObjectWithTag("robotbody").GetComponent<Transform>().rotation
            .eulerAngles);
    }


    public override void Heuristic(in ActionBuffers actionsOut) {
        trainingmode = false;
    }
}