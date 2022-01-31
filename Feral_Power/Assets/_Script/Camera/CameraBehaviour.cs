using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraBehaviour : MonoBehaviour
{
    public GameObject FllowObject;
    public List<GameObject> DockingPoints;
    private Camera Cam;

    //private bool IsNight = false;
    //private bool ChangingTime = false;
    //public float TransitionTime = 0.5f;

    private void Start()
    {
        Cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float ClosestDistance = 100000000;
        for (int i = 0; i < DockingPoints.Count; i++)
        {
            //Vector3 NewPos = DockingPoints[i].transform.position;
            if (DockingPoints[i] != null)
            {
                Vector3 NewPos = DockingPoints[i].transform.position;
                Vector2 Heading = NewPos - FllowObject.transform.position;
                float Distance = Heading.magnitude;

                if (Distance < ClosestDistance)
                {
                    NewPos.z = transform.position.z;
                    transform.position = NewPos;
                    ClosestDistance = Distance;
                }
            }

        }
        DockingPoints.Clear();

        
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
