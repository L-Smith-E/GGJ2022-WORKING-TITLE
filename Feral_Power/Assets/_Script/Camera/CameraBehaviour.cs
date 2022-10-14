using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraBehaviour : MonoBehaviour
{
    public GameObject FllowObject;
    public List<GameObject> DockingPoints;
    public float MaxCameraSpeed = 10.0f;
    private Camera Cam;
    private Vector3 TargetPos;

    //private bool IsNight = false;
    //private bool ChangingTime = false;
    //public float TransitionTime = 0.5f;

    private void Start()
    {
        Cam = GetComponent<Camera>();
        TargetPos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float ClosestDistance = float.MaxValue;
        for (int i = 0; i < DockingPoints.Count; i++)
        {
            if (DockingPoints[i] != null)
            {
                // Distance from the player to the camera
                Vector3 ObjectPos = DockingPoints[i].transform.position;
                Vector2 Heading = ObjectPos - FllowObject.transform.position;
                float Distance = Heading.magnitude;

                if (Distance < ClosestDistance)
                {
                    TargetPos = DockingPoints[i].transform.position;
                    ClosestDistance = Distance;
                }
            }

        }
        DockingPoints.Clear();

        
        Vector3 TargetHeading = TargetPos - transform.position;
        Vector3 TargetDirection = TargetHeading.normalized;
        // Could use the ClosestDistance but by default the ClosestDistance is the max value of float
        // And that could mess stuff up

        float TargetDistanceClamp = Mathf.Clamp(TargetHeading.magnitude, 0, MaxCameraSpeed);
        transform.position += TargetDirection * TargetDistanceClamp;




        // Change the background colour
        // Yes, I spell colour with a u
        // It's the right way of spelling it
        if (GameManager.IsNight())
        {
            // Night
            Cam.backgroundColor = Color.black;
        }
        else
        {
            // Day
            Cam.backgroundColor = Color.grey;
        }

    }

    void ChangeFollowObject(GameObject o)
    {
        DockingPoints.Add(o);
    }
}
