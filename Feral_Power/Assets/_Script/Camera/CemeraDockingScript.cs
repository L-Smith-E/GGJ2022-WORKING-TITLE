using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]
public class CemeraDockingScript : MonoBehaviour
{
    public Camera Cam;
    public CameraBehaviour CameraScript;
    public BoxCollider2D Collider;

    public bool AutoAdjustColliderSize = true;

    public float ColliderHeight;
    public float ColliderWidth;
    // Start is called before the first frame update
    void Start()
    {
        if (Cam == null)
        {
            Cam = Camera.main;
            CameraScript = Camera.main.GetComponent<CameraBehaviour>();
        }

        if (Collider == null)
            Collider = GetComponent<BoxCollider2D>();

        if(AutoAdjustColliderSize)
        {
            ColliderHeight = 2f * Cam.orthographicSize;
            ColliderWidth = ColliderHeight * Cam.aspect;
            Collider.size = new Vector2(ColliderWidth, ColliderHeight);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        // Draw a semitransparent blue cube at the transforms position
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(transform.position, new Vector3(1, 1, 1));
    }

    void OnTriggerStay2D(Collider2D Collider)
    {
        if (CameraScript != null)
        {
            if (Collider.gameObject == CameraScript.FllowObject)
            {
                Cam.SendMessage("ChangeFollowObject", this.transform.gameObject);
            }
        }

    }
}
