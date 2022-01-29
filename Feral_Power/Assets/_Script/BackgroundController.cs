using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This code is for Debug only.
// Don't use this code when putting the game together
public class BackgroundController : MonoBehaviour
{
    private bool IsNight = false;
    private SpriteRenderer SR;
    // Start is called before the first frame update
    void Start()
    {
        SR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            IsNight = !IsNight;
        }

        if(IsNight)
        {
            // Night
            SR.color = Color.grey;
        }
        else
        {
            // Day
            SR.color = Color.white;
        }
    }
}
