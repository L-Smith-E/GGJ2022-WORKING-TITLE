using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraBehaviour : MonoBehaviour
{
    public GameObject FllowObject;

    // Update is called once per frame
    void Update()
    {
        if (FllowObject != null)
        {
            Vector3 NewPos = FllowObject.transform.position;
            NewPos.z = transform.position.z;
            transform.position = NewPos;
        }
    }
}
