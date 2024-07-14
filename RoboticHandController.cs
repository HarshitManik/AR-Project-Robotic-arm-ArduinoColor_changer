using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboticHandController : MonoBehaviour
{
    public Transform pickPosition;  // Position to pick up the object
    public Transform placePosition; // Position to place the object
    public GameObject Panel;

    public RobotController robotController;
    public PincherController pincherController;
    public GameObject objectToPick;

    public float moveSpeed = 1.0f;
    public float gripSpeed = 3.0f;

    private enum State { Idle, MovingToPick, Gripping, MovingToPlace, Releasing }
    private State currentState = State.Idle;

    private void Start()
    {
        // Ensure all joints and the pincher are in their initial state
        robotController.StopAllJointRotations();
        pincherController.ResetGripToOpen();
    }

    private void Update()
    {
        switch (currentState)
        {
            case State.Idle:
                // Transition to the next state when needed
                currentState = State.MovingToPick;
                break;

            case State.MovingToPick:
                MoveToPosition(pickPosition.position);
                break;

            case State.Gripping:
                GripObject();
                break;

            case State.MovingToPlace:
                MoveToPosition(placePosition.position);
                break;

            case State.Releasing:
                ReleaseObject();
                break;
        }
    }

    private void MoveToPosition(Vector3 targetPosition)
    {
        Debug.Log("Moving to position: " + targetPosition);
        
        // For debugging, we simply move the whole arm GameObject
        // Adjust this logic to move specific joints using the RobotController and ArticulationJointController
        if (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
        else
        {
            Debug.Log("Reached position: " + targetPosition);
            currentState = currentState == State.MovingToPick ? State.Gripping : State.Releasing;
        }
    }

    private void GripObject()
    {
        Debug.Log("Gripping object...");
        
        // Implement the logic to close the pincher and grip the object
        pincherController.gripState = GripState.Closing;

        if (pincherController.CurrentGrip() >= 1.0f)
        {
            pincherController.gripState = GripState.Fixed;
            currentState = State.MovingToPlace;

            // Attach the object to the pincher
            objectToPick.transform.SetParent(pincherController.transform);
            Debug.Log("Object gripped.");
        }
    }

    private void ReleaseObject()
    {
        Debug.Log("Releasing object...");
        
        // Implement the logic to open the pincher and release the object
        pincherController.gripState = GripState.Opening;

        if (pincherController.CurrentGrip() <= 0.0f)
        {
            pincherController.gripState = GripState.Fixed;
            currentState = State.Idle;

            // Detach the object from the pincher
            objectToPick.transform.SetParent(null);
            Debug.Log("Object released.");
        }
    }
}
