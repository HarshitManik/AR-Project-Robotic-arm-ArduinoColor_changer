using UnityEngine;
using UnityEngine.UI;

public class controller : MonoBehaviour
{
    public Transform[] joints; // Array to hold references to the robot's joints
    public float[] targetAngles; // Target angles for each joint
    public float speed = 5f; // Speed of movement

    public Slider[] jointSliders; // UI sliders for controlling each joint

    void Start()
    {
        // Initialize sliders based on initial joint angles
        for (int i = 0; i < jointSliders.Length; i++)
        {
            int index = i; // Capture index for use in lambda
            jointSliders[i].value = targetAngles[i];
            jointSliders[i].onValueChanged.AddListener((value) => OnSliderValueChanged(index, value));
        }
    }

    void Update()
    {
        for (int i = 0; i < joints.Length; i++)
        {
            if (i < targetAngles.Length)
            {
                // Smoothly rotate joints towards target angles
                float angle = Mathf.LerpAngle(joints[i].localEulerAngles.y, targetAngles[i], Time.deltaTime * speed);
                joints[i].localEulerAngles = new Vector3(joints[i].localEulerAngles.x, angle, joints[i].localEulerAngles.z);
            }
        }
    }

    void OnSliderValueChanged(int jointIndex, float value)
    {
        targetAngles[jointIndex] = value;
    }

    public void SetTargetAngles(float[] angles)
    {
        if (angles.Length == targetAngles.Length)
        {
            targetAngles = angles;
        }
    }
}
