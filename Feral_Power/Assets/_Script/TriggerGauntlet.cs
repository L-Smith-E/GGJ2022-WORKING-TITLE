using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerGauntlet : MonoBehaviour
{
    public static bool startGauntlet = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            startGauntlet = true;
            Debug.Log("Start the battle");
        }
        
    }
}
